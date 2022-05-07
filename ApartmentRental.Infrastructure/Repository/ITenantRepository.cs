using ApartmentRental.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentRental.Infrastructure.Repository
{
    internal interface ITenantRepository : IRepository<Tenant>
    {
    }
}
