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
    internal class LandLordRepository : ILandLordRepository
    {
        private readonly MainContext _mainContext;

        public LandLordRepository(MainContext mainContext)
        {
            _mainContext = mainContext;
        }
        public async Task AddAsync(Landlord entity)
        {
            entity.DateOfCreation = DateTime.UtcNow;
            await _mainContext.AddAsync(entity);
            await _mainContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var landLordToDelete = await _mainContext.Landlord.SingleOrDefaultAsync(x => x.Id == id);
            if (landLordToDelete != null)
            {
                _mainContext.Landlord.Remove(landLordToDelete);
                await _mainContext.SaveChangesAsync();
            }

            throw new EntityNotFoundException();
        }

        public async Task<IEnumerable<Landlord>> GetAllAsync()
        {
            var landLords = await _mainContext.Landlord.ToListAsync();

            foreach (var landLord in landLords)
            {
                await _mainContext.Entry(landLord).Reference(x => x.Account).LoadAsync();
            }
            return landLords;
        }

        public async Task<Landlord> GetByIdAsync(int id)
        {
            var landLords = await _mainContext.Landlord.SingleOrDefaultAsync(x => x.Id == id);
            if (landLords != null)
            {
                await _mainContext.Entry(landLords).Reference(x => x.Account).LoadAsync();
                return landLords;
            }

            throw new EntityNotFoundException();
        }

        public async Task UpdateAsync(Landlord entity)
        {
            var landLordToUpdate = await _mainContext.Landlord.SingleOrDefaultAsync(x => x.Id == entity.Id);
            if (landLordToUpdate == null)
            {
                throw new EntityNotFoundException();
            }

            landLordToUpdate.Account = entity.Account;
            landLordToUpdate.DateOfUpDateTime = DateTime.UtcNow;
            
            await _mainContext.SaveChangesAsync();
        }
    }
}
