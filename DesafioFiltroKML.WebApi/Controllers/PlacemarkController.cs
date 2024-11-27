using DesafioFiltroKML.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/placemarks")]
public class PlacemarkController : ControllerBase
{
    private readonly GetFilteredPlacemarksService _getFilteredPlacemarksService;
    private readonly KmlExportService _kmlExportService;

    public PlacemarkController(GetFilteredPlacemarksService getFilteredPlacemarksUseCase, KmlExportService kmlExportService)
    {
        _getFilteredPlacemarksService = getFilteredPlacemarksUseCase;
        _kmlExportService = kmlExportService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPlacemarks([FromQuery] PlacemarkFilter filter)
    {
        var placemarks = await _getFilteredPlacemarksService.ExecuteAsync(filter);
        return Ok(placemarks); 
    }

    [HttpPost("export")]
    public async Task<IActionResult> ExportPlacemarks([FromBody] PlacemarkFilter filter)
    {
        var placemarks = await _getFilteredPlacemarksService.ExecuteAsync(filter);
        var filePath = await _kmlExportService.ExportFilteredPlacemarksAsync((IEnumerable<SharpKml.Dom.Placemark>)placemarks);

        return Ok(new { Message = "Arquivo exportado com sucesso.", FilePath = filePath });
    }
}
