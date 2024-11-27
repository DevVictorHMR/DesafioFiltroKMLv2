using Microsoft.OpenApi.Models;
using Core.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Environment.WebRootPath ??= Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
Directory.CreateDirectory(builder.Environment.WebRootPath);

var kmlFilePath = Path.Combine(builder.Environment.WebRootPath, "DIRECIONADORES1.kml");

builder.Services.AddSingleton<IPlacemarkRepository>(sp =>
    new PlacemarkRepository(kmlFilePath, sp.GetRequiredService<IKmlLoader>()));
builder.Services.AddScoped<IKmlLoader, KmlLoader>();
builder.Services.AddScoped<KmlExportService>();
builder.Services.AddScoped<GetFilteredPlacemarksService>();
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Desafio Filtro KML API V1");
    });
}


app.UseDefaultFiles(); // Habilita o `index.html` como padrão na raiz
app.UseStaticFiles();  // Serve arquivos estáticos de `wwwroot`

app.MapControllers();

app.Run();
