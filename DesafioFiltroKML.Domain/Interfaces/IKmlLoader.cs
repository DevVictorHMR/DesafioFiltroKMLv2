using DesafioFiltroKML.Domain.Entities;

namespace Core.Domain.Interfaces
{
    public interface IKmlLoader
    {
        Task<List<Placemark>> LoadKmlAsync(string filePath);
    }
}
