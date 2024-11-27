using Core.Domain.Interfaces;
using DesafioFiltroKML.Domain.Entities;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/placemarks")]
public class PlacemarkController : ControllerBase
{
    private readonly IPlacemarkRepository _placemarkRepository;
    private readonly GetFilteredPlacemarksService _filteredPlacemarksService;
    private readonly KmlExportService _kmlExportService;

    public PlacemarkController(
        IPlacemarkRepository placemarkRepository,
        GetFilteredPlacemarksService filteredPlacemarksService,
        KmlExportService kmlExportService)
    {
        _placemarkRepository = placemarkRepository;
        _filteredPlacemarksService = filteredPlacemarksService;
        _kmlExportService = kmlExportService;
    }

    [HttpGet("filters")]
    public IActionResult GetAvailableFilters()
    {
        var availableFilters = _placemarkRepository.GetAvailableFilters();
        return Ok(availableFilters);
    }

    [HttpGet]
    public IActionResult GetPlacemarks([FromQuery] PlacemarkFilter filter)
    {
        var placemarks = _filteredPlacemarksService.ExecuteAsync(filter);
        return Ok(placemarks);
    }

    [HttpPost("export")]
    public async Task<IActionResult> ExportPlacemarks([FromBody] PlacemarkFilter filter)
    {
        try
        {
            // Agora aguardamos o resultado da execução assíncrona
            var filteredPlacemarks = await _filteredPlacemarksService.ExecuteAsync(filter);

            if (!filteredPlacemarks.Any())
            {
                return BadRequest("Nenhum placemark encontrado para os filtros fornecidos.");
            }

            var exportPath = Path.Combine(Directory.GetCurrentDirectory(), "ExportedKml");

            var filePath = await _kmlExportService.ExportFilteredPlacemarksAsync(filteredPlacemarks, exportPath);

            return Ok(new { Message = "Arquivo exportado com sucesso.", FilePath = filePath });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno ao exportar arquivo KML: {ex.Message}");
        }
    }
}
