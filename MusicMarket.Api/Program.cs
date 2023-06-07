using Microsoft.EntityFrameworkCore;
using MusicMarket.Core;
using MusicMarket.Core.Services;
using MusicMarket.Data;
using MusicMarket.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MusicMarketDbContext>(opt=>opt.UseSqlServer(builder.Configuration.GetConnectionString("oguzhanMSSQL"),x=>x.MigrationsAssembly("MusicMarket.Data")));


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); //AddScoped: her oturum icin yeni bir intance alacak


builder.Services.AddTransient<IMusicService, MusicService>();//AddTransient: Her seferinde intance yaratacak
builder.Services.AddTransient<IArtistService, ArtistService>();//AddSingleton: Butun uygulama boyunca 1 intance alccak

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
