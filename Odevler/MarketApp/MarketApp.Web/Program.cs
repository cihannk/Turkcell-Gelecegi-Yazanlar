using MarketApp.Business.Abstract;
using MarketApp.Business.Concrete;
using MarketApp.Business.MapperProfile;
using MarketApp.DataAccess.Contexts;
using MarketApp.DataAccess.Repositories;
using MarketApp.Web.Data;
using MarketApp.Web.Middlewares;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<EfDbContext>(optionsBuilder => optionsBuilder.UseSqlServer(connectionString));

builder.Services.AddScoped<IProductRepository, EFProductRepository>();
builder.Services.AddScoped<IProductService, MarketApp.Business.Concrete.ProductService>();

builder.Services.AddScoped<ICategoryRepository, EFCategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, EFUserRepository>();
builder.Services.AddScoped<IHashingService, HashingService>();

builder.Services.AddScoped<IOrderService, MarketApp.Business.Concrete.OrderService>();
builder.Services.AddScoped<IUserRepository, EFUserRepository>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IAddressRepository, EFAddressRepository>();
builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();
builder.Services.AddScoped<ICartItemRepository, EFCartItemRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddSession();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
{
    opt.LoginPath = "/User/Login";
    opt.AccessDeniedPath = "/User/AccessDenied";
});

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe").GetValue<string>("SecretKey");


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseMiddleware(typeof(ErrorHandlingMiddleware));
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "",
        pattern: "{categoryName}/Sayfa{page}",
        defaults: new { controller = "Home", action = "Index", page = 1 });

    endpoints.MapControllerRoute(
        name: "",
        pattern: "Sayfa{page}",
        defaults: new { controller = "Home", action = "Index", page = 1 });

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
