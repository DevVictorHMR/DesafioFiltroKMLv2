using Microsoft.OpenApi.Models;
using Core.Domain.Interfaces;
using DesafioFiltroKML.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configura��o para caminho do arquivo KML
var kmlFilePath = Path.Combine(builder.Environment.WebRootPath, "DIRECIONADORES1.kml");

// Registro de depend�ncias
builder.Services.AddSingleton<IPlacemarkRepository>(sp =>
    new PlacemarkRepository(kmlFilePath, sp.GetRequiredService<IKmlLoader>()));
builder.Services.AddScoped<IKmlLoader, KmlLoader>();

// Configura��o para API e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Desafio Filtro KML API",
        Version = "v1",
        Description = "API para filtrar e exportar dados do arquivo KML."
    });
});

var app = builder.Build();

// Configura��o do Swagger para ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Desafio Filtro KML API V1");
        options.RoutePrefix = string.Empty; // Para acessar diretamente no root
    });
}

// Configura��o para arquivos est�ticos (se necess�rio)
app.UseStaticFiles();

// Mapear controllers
app.MapControllers();

// Rodar a aplica��o
app.Run();
