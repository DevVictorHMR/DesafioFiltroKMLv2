using Core.Domain.Interfaces;
using DesafioFiltroKML.Domain.Entities;

namespace DesafioFiltroKML.Persistence.Repositories
{
    public class PlacemarkRepository : IPlacemarkRepository
    {
        private readonly string _kmlFilePath;
        private readonly IKmlLoader _kmlLoader;

        public PlacemarkRepository(string kmlFilePath, IKmlLoader kmlLoader)
        {
            _kmlFilePath = kmlFilePath;
            _kmlLoader = kmlLoader;
        }

        public async Task<IEnumerable<Placemark>> GetPlacemarksAsync()
        {
            var placemarks = await _kmlLoader.LoadKmlAsync(_kmlFilePath);
            return placemarks;
        }

        public Task SavePlacemarkAsync(Placemark placemark)
        {
            throw new NotImplementedException();
        }
    }
}
