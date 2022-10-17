using CadastroDeCampeonato.Controllers;
using CadastroDeCampeonato.Data;
using CadastroDeCampeonato.Repositories;
using CadastroDeCampeonato.Repositories.Interfaces;
using CadastroDeCampeonato.Repository;
using CadastroDeCampeonato.Repository.Interfaces;
using CadastroDeCampeonato.Services;
using CadastroDeCampeonato.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ApiDbContext>();
builder.Services.AddTransient<ITimeRepository, TimeRepository>();
builder.Services.AddTransient<ICampeonatoRepository, CampeonatoRepository>();
builder.Services.AddTransient<ICampeonatoTimeRepository, CampeonatoTimeRepository>();
builder.Services.AddTransient<IPartidaRepository, PartidaRepository>();
builder.Services.AddTransient<IPartidaTimeRepository, PartidaTimeRepository>();

builder.Services.AddTransient<ICampeonatoService, CampeonatoService>();
builder.Services.AddTransient<ITimeService, TimeService>();





builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
