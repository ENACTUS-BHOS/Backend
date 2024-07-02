public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ExhibitionContext>(opt =>
            opt.UseInMemoryDatabase("ExhibitionsDB"));
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
