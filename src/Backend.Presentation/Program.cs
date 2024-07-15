using System.Reflection;
using Backend.Core.Repositories;
using Backend.Core.Services;
using Backend.Infrastructure.Data;
using Backend.Infrastructure.Repositories;
using Backend.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<MirasDbContext>(dbContextOptionsBuilder =>
{
    var connectionString = builder.Configuration.GetConnectionString("MirasDb");

    dbContextOptionsBuilder.UseNpgsql(connectionString, o => 
    {
        o.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
    });
});

// Register the Artists services and repositories
builder.Services.AddTransient<IArtistsRepository, ArtistsSqlRepository>();
builder.Services.AddTransient<IArtistsService, ArtistsSqlService>();

// Register the Team services and repositories
builder.Services.AddTransient<ITeamRepository, TeamSqlRepository>();
builder.Services.AddTransient<ITeamService, TeamSqlService>();

// Register the Tutorial services and repositories
builder.Services.AddTransient<ITutorialRepository, TutorialSqlRepository>();
builder.Services.AddTransient<ITutorialService, TutorialSqlService>();

// Register the Exhibition services and repositories

builder.Services.AddTransient<IExhibitionRepository, ExhibitionSqlRepository>();
builder.Services.AddTransient<IExhibitionService, ExhibitionSqlService>();

// Register the Products services and repositories
builder.Services.AddTransient<IProductsRepository, ProductsSqlRepository>();
builder.Services.AddTransient<IProductsService, ProductSqlService>();

// Register the Orders services and repositories
builder.Services.AddTransient<IOrdersRepository, OrdersSqlRepository>();
builder.Services.AddTransient<IOrdersService, OrdersSqlServer>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorWasmPolicy", corsBuilder =>
    {
        corsBuilder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("BlazorWasmPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
