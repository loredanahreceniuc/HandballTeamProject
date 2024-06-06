using exp.NET6.Infrastructure.Context;
using exp.NET6.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Infrastructure.Repositories.Matches
{
    public class MatchRepository : GenericRepository<Match>, IMatchRepository
    {
        public MatchRepository(FlowerPowerDbContext context) : base(context) { }

        public async Task<Match> DeleteVirtual(int id)
        {
            try
            {
                var entity = await context.Set<Match>().FindAsync(id);
                if (entity == null)
                    throw new KeyNotFoundException("This object does not exist, please enter a valid id");
                entity.IsDeleted = true;
                await context.SaveChangesAsync();
                return entity;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IQueryable<Match> GetMatchesQueryable() {

            return context.Matches.Include(x => x.Competition).AsNoTracking().AsQueryable();
        }
    }
}
