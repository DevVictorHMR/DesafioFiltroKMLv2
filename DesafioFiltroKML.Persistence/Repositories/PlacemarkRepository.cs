using Core.Domain.Interfaces;
using DesafioFiltroKML.Domain.Entities;

public class PlacemarkRepository : IPlacemarkRepository
{
    private readonly List<Placemark> _placemarks;
    private string kmlFilePath;
    private IKmlLoader kmlLoader;

    public PlacemarkRepository(List<Placemark> placemarks)
    {
        _placemarks = placemarks ?? new List<Placemark>();
    }

    public PlacemarkRepository(string kmlFilePath, IKmlLoader kmlLoader)
    {
        this.kmlFilePath = kmlFilePath;
        this.kmlLoader = kmlLoader;
    }

    public Dictionary<string, IEnumerable<string>> GetAvailableFilters()
    {
        return new Dictionary<string, IEnumerable<string>>
        {
            { "CLIENTE", _placemarks.Select(p => p.Cliente).Where(c => c != null).Distinct().OrderBy(c => c) },
            { "SITUACAO", _placemarks.Select(p => p.Situacao).Where(s => s != null).Distinct().OrderBy(s => s) },
            { "BAIRRO", _placemarks.Select(p => p.Bairro).Where(b => b != null).Distinct().OrderBy(b => b) }
        };
    }

    public IEnumerable<Placemark> GetAllPlacemarks() => _placemarks;

    public async Task<IEnumerable<Placemark>> GetFilteredPlacemarksAsync(PlacemarkFilter filter)
    {
        var query = _placemarks.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Cliente))
        {
            query = query.Where(p => p.Cliente.Equals(filter.Cliente, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(filter.Situacao))
        {
            query = query.Where(p => p.Situacao.Equals(filter.Situacao, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(filter.Bairro))
        {
            query = query.Where(p => p.Bairro.Equals(filter.Bairro, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(filter.Referencia) && filter.Referencia.Length >= 3)
        {
            query = query.Where(p => p.Referencia != null &&
                                     p.Referencia.Contains(filter.Referencia, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(filter.RuaCruzamento) && filter.RuaCruzamento.Length >= 3)
        {
            query = query.Where(p => p.RuaCruzamento != null &&
                                     p.RuaCruzamento.Contains(filter.RuaCruzamento, StringComparison.OrdinalIgnoreCase));
        }

        return await Task.Run(() => query.ToList());
    }

    public IEnumerable<Placemark> GetFilteredPlacemarks(PlacemarkFilter filter)
    {
        throw new NotImplementedException();
    }
}