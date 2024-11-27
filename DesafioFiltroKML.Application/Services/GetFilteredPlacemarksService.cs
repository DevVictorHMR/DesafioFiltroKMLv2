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
        return await _placemarkRepository.GetFilteredPlacemarksAsync(filter);
    }
}
