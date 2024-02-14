using MyIoTService;
public class Program
{
    public static async Task Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        Startup.ConfigureServices(builder.Services, builder.Configuration);

        WebApplication app = Startup.ConfigureApplication(builder);
        await app.RunAsync();
    }
}