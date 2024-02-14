using DeviceEmulator;
using DeviceEmulator.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static async Task Main(string[] args)
    {
        using IHost host = CreateHostBuilder(args).Build();
        await host.StartAsync();

        ApplicationRunner? applicationRunner = host.Services.GetRequiredService<ApplicationRunner>();
        await applicationRunner.RunAsync();

        // Delay the closing of the application to ensure that error messages or final output are visible to the user.
        await Task.Delay(10000); // 10-second delay

        await host.StopAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddHttpClient<IHttpService, HttpService>(client =>
                {
                    client.BaseAddress = new Uri("https://localhost:7285");
                });

                services.AddSingleton<IEmulatorService, EmulatorService>();
                services.AddSingleton<ApplicationRunner>();
            });
}