public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ExhibitionContext>();
            context.Exhibitions.AddRange(
                new Exhibition { Title = "Exhibition 1", Description = "Description 1", PictureUrl = "url1.jpg", VideoUrl = "video1.mp4" },
                new Exhibition { Title = "Exhibition 2", Description = "Description 2", PictureUrl = "url2.jpg", VideoUrl = "video2.mp4" }
            );
            context.SaveChanges();
        }

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
