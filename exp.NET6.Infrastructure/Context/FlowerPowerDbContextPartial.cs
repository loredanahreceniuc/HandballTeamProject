using exp.NET6.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.NET6.Infrastructure.Context
{
    public partial class FlowerPowerDbContext : DbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetUser>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Article>().HasQueryFilter(x => x.IsDeleted != null ? (bool)!x.IsDeleted : false);
            modelBuilder.Entity<BlogCategory>().HasQueryFilter(x => x.IsDeleted != null ? (bool)!x.IsDeleted : false);
            modelBuilder.Entity<Player>().HasQueryFilter(x => x.IsDeleted != null ? (bool)!x.IsDeleted : false );
            modelBuilder.Entity<staff>().HasQueryFilter(x => x.IsDeleted != null ? (bool)!x.IsDeleted : false);
            modelBuilder.Entity<UserLocation>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<UserLocation>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<TeamCategory>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<PlayerGallery>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Sponsor>().HasQueryFilter(x => x.IsDeleted != null ? (bool)!x.IsDeleted : false);
            modelBuilder.Entity<Match>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Trophy>().HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
