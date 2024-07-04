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
builder.Services.AddScoped<IArtistsRepository, ArtistsSqlRepository>();
builder.Services.AddScoped<IArtistsService, ArtistsSqlService>();

// Register the Team services and repositories
builder.Services.AddScoped<ITeamRepository, TeamSqlRepository>();
builder.Services.AddScoped<ITeamService, TeamSqlService>();

// Register the Tutorial services and repositories
builder.Services.AddScoped<ITutorialRepository, TutorialSqlRepository>();
builder.Services.AddScoped<ITutorialService, TutorialSqlService>();

// Register the Exhibition services and repositories

builder.Services.AddScoped<IExhibitionRepository, ExhibitionSqlRepository>();
builder.Services.AddScoped<IExhibitionService, ExhibitionSqlService>();

// Register the Products services and repositories
builder.Services.AddScoped<IProductsRepository, ProductsSqlRepository>();
builder.Services.AddScoped<IProductsService, ProductSqlService>();

// Register the Orders services and repositories
builder.Services.AddScoped<IOrdersRepository, OrdersSqlRepository>();
builder.Services.AddScoped<IOrdersService, OrdersSqlServer>();

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
