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
    internal class TenantRepository : ITenantRepository
    {
        private readonly MainContext _mainContext;

        public TenantRepository(MainContext mainContext)
        {
            _mainContext = mainContext;
        }
        public async Task AddAsync(Tenant entity)
        {
            entity.DateOfCreation = DateTime.UtcNow;
            await _mainContext.AddAsync(entity);
            await _mainContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var tenantToDelete = await _mainContext.Tenant.SingleOrDefaultAsync(x => x.Id == id);
            if (tenantToDelete != null)
            {
                _mainContext.Tenant.Remove(tenantToDelete);
                await _mainContext.SaveChangesAsync();
            }

            throw new EntityNotFoundException();
        }

        public async Task<IEnumerable<Tenant>> GetAllAsync()
        {
            var tenants = await _mainContext.Tenant.ToListAsync();

            foreach (var tenant in tenants)
            {
                await _mainContext.Entry(tenant).Reference(x => x.Account).LoadAsync();
            }
            return tenants;
        }

        public async Task<Tenant> GetByIdAsync(int id)
        {
            var tenants = await _mainContext.Tenant.SingleOrDefaultAsync(x => x.Id == id);
            if (tenants != null)
            {
                await _mainContext.Entry(tenants).Reference(x => x.Account).LoadAsync();
                return tenants;
            }

            throw new EntityNotFoundException();
        }

        public async Task UpdateAsync(Tenant entity)
        {
            var tenantToUpdate = await _mainContext.Tenant.SingleOrDefaultAsync(x => x.Id == entity.Id);
            if (tenantToUpdate == null)
            {
                throw new EntityNotFoundException();
            }

            tenantToUpdate.Account = entity.Account;
            tenantToUpdate.DateOfUpDateTime = DateTime.UtcNow;

            await _mainContext.SaveChangesAsync();
        }
    }
}
