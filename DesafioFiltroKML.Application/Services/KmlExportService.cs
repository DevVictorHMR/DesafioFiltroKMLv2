using SharpKml.Base;
using SharpKml.Dom;
using SharpKml.Engine;

public class KmlExportService
{
    private readonly string _exportPath;

    public KmlExportService(string exportPath)
    {
        _exportPath = exportPath;
    }

    public async Task<string> ExportFilteredPlacemarksAsync(IEnumerable<Placemark> filteredPlacemarks)
    {
        var document = new Document();

        foreach (var placemark in filteredPlacemarks)
        {
            var clonedPlacemark = placemark.Clone() as Placemark;
            document.AddFeature(clonedPlacemark);
        }

        var kml = new Kml { Feature = document };
        var serializer = new Serializer();
        serializer.Serialize(kml);

        var fileName = $"KMLExportado_{DateTime.Now:yyyyMMdd}.kml";
        var filePath = Path.Combine(_exportPath, fileName);

        await File.WriteAllTextAsync(filePath, serializer.Xml);

        return filePath;
    }
}
