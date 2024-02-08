using FormEnhancer.Data;
using FormEnhancer.Repository;
using FormEnhancer.Services;

namespace FormEnhancer
{
    public class Startup
    {
        public static void Start(WebApplicationBuilder builder)
        {
            ConfigureServices(builder.Services);
            Configure(builder);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddTransient<IDataAccess, DataAccess>();

            services.AddTransient<ISectorService, SectorService>();
            services.AddTransient<ISectorRepository, SectorRepository>();

            services.AddTransient<IReplyService, ReplyService>();
            services.AddTransient<IReplyRepository, ReplyRepository>();
        }

        private static void Configure(WebApplicationBuilder builder)
        {
            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}
