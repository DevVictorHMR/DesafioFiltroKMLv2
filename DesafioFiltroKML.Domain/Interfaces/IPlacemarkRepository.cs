using DesafioFiltroKML.Domain.Entities;

namespace Core.Domain.Interfaces
{
    public interface IPlacemarkRepository
    {
        IEnumerable<Placemark> GetFilteredPlacemarks(PlacemarkFilter filter);
        Dictionary<string, IEnumerable<string>> GetAvailableFilters();
        Task<IEnumerable<Placemark>> GetFilteredPlacemarksAsync(PlacemarkFilter filter);
    }

}
