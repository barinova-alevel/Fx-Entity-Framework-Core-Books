using Books.DataAccessLayer;
using Books.PresentationLayer;
using Microsoft.Extensions.Configuration;
using Serilog;

class Program
{
    static void Main()
    {
        var applicationContext = new ApplicationContext();
        applicationContext.SaveChanges();

        IConfigurationRoot builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder)
            .CreateLogger();
        Log.Logger.Information("start");

        InputOutput inputOutput = new InputOutput();
        inputOutput.Run();
        Console.ReadKey();
    }
}
