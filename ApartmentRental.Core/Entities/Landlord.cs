﻿namespace ApartmentRental.Core.Entities;

public class Landlord : BaseEntity
{
    public List<Apartment> Apartments { get; set; }
}