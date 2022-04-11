namespace ApartmentRental.Core.Entities;

public class Apartment : BaseEntity
{
    public decimal RentAmount { get; set; }
    public int NumberofRooms { get; set; }
    public int SquereMeters { get; set; }
    public int Floor { get; set; }
    public bool IsElevator { get; set; }
    
}