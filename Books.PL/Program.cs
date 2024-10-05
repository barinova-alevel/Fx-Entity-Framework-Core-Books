using Books.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

class Program
{
    static void Main()
    {
        var applicationContext = new ApplicationContext();
        var book = applicationContext.Books.First();
        if (book != null)
        {
            applicationContext.Books.Remove(book);
            applicationContext.SaveChanges();
        }
        //IConfiguration configuration = new ConfigurationBuilder()
        //    .AddJsonFile("appsettings.json", true, true)
        //    .Build();

        //var connectionString = configuration.GetConnectionString("DefaultConnection"); //?why null here?
        //var dbOptionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        //dbOptionsBuilder.UseSqlServer(connectionString, i => i.CommandTimeout(20));
        //dbOptionsBuilder.LogTo(Console.Write);
        ////add logger

        //var applicationContext = new ApplicationContext(dbOptionsBuilder.Options);
        //applicationContext.Database.Migrate();
    }
}
