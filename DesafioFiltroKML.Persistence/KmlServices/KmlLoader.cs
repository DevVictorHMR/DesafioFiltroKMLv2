using Core.Domain.Interfaces;
using DesafioFiltroKML.Domain.Entities;
using SharpKml.Base;
using SharpKml.Dom;
using SharpKml.Engine;
using Placemark = DesafioFiltroKML.Domain.Entities.Placemark;

public class KmlLoader : IKmlLoader
{
    public async Task<List<Placemark>> LoadKmlAsync(string filePath)
    {
        var placemarks = new List<Placemark>();

        await using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
        var parser = new Parser();
        await Task.Run(() => parser.Parse(fileStream));

        if (parser.Root is Kml kml && kml.Feature is Document document)
        {
            placemarks.AddRange(document.Flatten().OfType<Placemark>());
        }

        return placemarks;
    }

}
