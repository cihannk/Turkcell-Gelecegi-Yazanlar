using IntroEF.Data;

namespace IntroEF;
public class Program
{
    static void Main(string[] args)
    {
        BookstoreDbContext ctx = new BookstoreDbContext();
        Console.WriteLine(ctx.Books.ToList());
    }
}