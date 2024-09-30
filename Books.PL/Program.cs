using Books.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

class Program
{
    static void Main()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .Build();

        var dbOptionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        dbOptionsBuilder.UseSqlServer(connectionString, i => i.CommandTimeout(20));
        dbOptionsBuilder.LogTo(Console.Write);
        //add logger

        var applicationContext = new ApplicationContext(dbOptionsBuilder.Options);
        applicationContext.Database.Migrate();


    }
}
