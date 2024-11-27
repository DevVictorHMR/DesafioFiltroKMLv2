using Core.Domain.Interfaces;
using DesafioFiltroKML.Domain.Entities;

public class GetFilteredPlacemarksService
{
    private readonly IPlacemarkRepository _placemarkRepository;

    public GetFilteredPlacemarksService(IPlacemarkRepository placemarkRepository)
    {
        _placemarkRepository = placemarkRepository;
    }

    public async Task<IEnumerable<Placemark>> ExecuteAsync(PlacemarkFilter filter)
    {
        if (!string.IsNullOrEmpty(filter.Referencia) && filter.Referencia.Length < 3)
            throw new ArgumentException("O filtro 'Referencia' deve ter pelo menos 3 caracteres.");

        if (!string.IsNullOrEmpty(filter.RuaCruzamento) && filter.RuaCruzamento.Length < 3)
            throw new ArgumentException("O filtro 'Rua/Cruzamento' deve ter pelo menos 3 caracteres.");

        var placemarks = await _placemarkRepository.GetPlacemarksAsync();

        return placemarks.Where(p =>
            (string.IsNullOrEmpty(filter.Cliente) || p.Cliente == filter.Cliente) &&
            (string.IsNullOrEmpty(filter.Situacao) || p.Situacao == filter.Situacao) &&
            (string.IsNullOrEmpty(filter.Bairro) || p.Bairro == filter.Bairro) &&
            (string.IsNullOrEmpty(filter.Referencia) || p.Referencia.Contains(filter.Referencia)) &&
            (string.IsNullOrEmpty(filter.RuaCruzamento) || p.RuaCruzamento.Contains(filter.RuaCruzamento))
        );
    }
}
