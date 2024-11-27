using DesafioFiltroKML.Domain.Entities;

namespace Core.Domain.Interfaces
{
    public interface IPlacemarkRepository
    {
        Task<IEnumerable<Placemark>> GetPlacemarksAsync();  
        Task SavePlacemarkAsync(Placemark placemark); 
    }
}
