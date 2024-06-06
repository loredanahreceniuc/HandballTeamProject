using exp.NET6.Infrastructure.Context;
using exp.NET6.Infrastructure.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Infrastructure.Repositories.Sponsors
{
    public class SponsorRepository: GenericRepository<Sponsor>, ISponsorRepository
    {
        public SponsorRepository(FlowerPowerDbContext context) : base(context) { }
    }
}
