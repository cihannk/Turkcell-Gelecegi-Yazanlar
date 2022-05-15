using MarketApp.API.Middlewares;
using MarketApp.Business.Abstract;
using MarketApp.Business.Concrete;
using MarketApp.Business.MapperProfile;
using MarketApp.DataAccess.Contexts;
using MarketApp.DataAccess.Repositories;
using MarketApp.Web.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("DbConnection");

builder.Services.AddDbContext<EfDbContext>(optionsBuilder => optionsBuilder.UseSqlServer(connectionString));

builder.Services.AddControllers().AddNewtonsoftJson(options => {
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddScoped<IProductRepository, EFProductRepository>();
builder.Services.AddScoped<IProductService, MarketApp.Business.Concrete.ProductService>();

builder.Services.AddScoped<ICategoryRepository, EFCategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, EFUserRepository>();

builder.Services.AddScoped<IOrderService, MarketApp.Business.Concrete.OrderService>();
builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();

builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IAddressRepository, EFAddressRepository>();

builder.Services.AddScoped<ICartItemRepository, EFCartItemRepository>();

builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRoleRepository, EFRoleRepository>();

builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddSingleton<IPaymentService, StripePaymentService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddCors(opt => opt.AddPolicy("Allow", builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
}));

var keyBytes = Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JWTSecretKey"));
var key = new SymmetricSecurityKey(keyBytes);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.SaveToken = true;
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateActor = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = "MarketApp",
        ValidAudience = "MarketApp",
        ValidateIssuerSigningKey = true,
        IssuerSigningKey= key
    };
});

//stripe
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe").GetValue<string>("SecretKey");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware(typeof(ErrorHandlingMiddleware));
app.UseHttpsRedirection();

app.UseCors("Allow");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
