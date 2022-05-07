using ApartmentRental.Infrastructure.Context;
using ApartmentRental.Infrastructure.Entities;
using ApartmentRental.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentRental.Infrastructure.Repository
{
    internal class AddressRepository : IAddressRepository
    {
        private readonly MainContext _mainContext;

        public AddressRepository(MainContext mainContext)
        {
            _mainContext = mainContext;
        }
        public async Task AddAsync(Address entity)
        {
            entity.DateOfCreation = DateTime.UtcNow;
            await _mainContext.AddAsync(entity);
            await _mainContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var addressToDelete = await _mainContext.Address.SingleOrDefaultAsync(x => x.Id == id);
            if (addressToDelete != null)
            {
                _mainContext.Address.Remove(addressToDelete);
                await _mainContext.SaveChangesAsync();
            }

            throw new EntityNotFoundException();
        }

        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            var addresses = await _mainContext.Address.ToListAsync();

            foreach (var address in addresses)
            {
                await _mainContext.Entry(address).Reference(x => x.Street).LoadAsync();
            }
            return addresses;
        }

        public async Task<Address> GetByIdAsync(int id)
        {
            var addresses = await _mainContext.Address.SingleOrDefaultAsync(x => x.Id == id);
            if (addresses != null)
            {
                await _mainContext.Entry(addresses).Reference(x => x.Street).LoadAsync();
                return addresses;
            }

            throw new EntityNotFoundException();
        }

        public async Task UpdateAsync(Address entity)
        {
            var addressToUpdate = await _mainContext.Address.SingleOrDefaultAsync(x => x.Id == entity.Id);
            if (addressToUpdate == null)
            {
                throw new EntityNotFoundException();
            }

            addressToUpdate.Street = entity.Street;
            addressToUpdate.ApartmentNumber = entity.ApartmentNumber;
            addressToUpdate.BuildingNumber = entity.BuildingNumber;
            addressToUpdate.City = entity.City;
            addressToUpdate.ZipCode = entity.ZipCode;
            addressToUpdate.Country = entity.Country;

            await _mainContext.SaveChangesAsync();
        }
    }
}
