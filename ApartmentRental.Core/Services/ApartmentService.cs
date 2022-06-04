using ApartmentRental.Core.DTO;

namespace ApartmentRental.Core.Services;
public class ApartmentService : IApartmentService
{
    private readonly IApartmentRepository _apartmentRepository;

    public ApartmentService(IApartmentRepositry apartmentRepository)
    {
        _apartmentRepository = apartmentRepositry;
    }
    public async Task<IEnumerable<ApartmentBasicInformationResponseDto>> GetAllApartmentsBasicInfosAsync()
    {
        var apartments = await _apartmentRepository.GetAllAsync();
        return apartments.Select(x => new ApartmentBasicInformationResponseDto(
        x.RentAmount,
        x.NumberOfRooms,
        x.SquareMeters,
        x.IsElevator,
        x.Address.City,
        x.Address.Street));
    }

    public async Task<ApartmentBasicInformationResponseDto?> GetTheCheapestApartmentAsync()
    {
        var apartments = await _apartmentRepository.GetAllSync();
        
        var cheapestOne = apartments.MinBy(x => x.RentAmount);

        if (cheapestOne is null) return null;

        return new ApartmentBasicInformationResponseDto(
            cheapestOne.RentAmount,
            cheapestOne.NumberOfRooms,
            cheapestOne.SquareMeters,
            cheapestOne.IsElevator,
            cheapestOne.Address.City,
            cheapestOne.Address.Street);
    }
}