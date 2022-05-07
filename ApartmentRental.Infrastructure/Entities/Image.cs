namespace ApartmentRental.Infrastructure.Entities;

public class Image : BaseEntity
{
    public byte[] Data { get; set; }

    public int AparmentId { get; set; }
    public Apartment Apartment { get; set; }
    
}