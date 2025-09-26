using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GamifyMyLifeAPI.Entities;

namespace GamifyMyLifeAPI.Data
{
    public class GamifyMyLifeContext : DbContext
    {
        public GamifyMyLifeContext (DbContextOptions<GamifyMyLifeContext> options)
            : base(options)
        {
        }

        public GamifyMyLifeContext()
        {

        }

        public DbSet<ActivityEntity> Activities { get; set; } = default!;
        public DbSet<CategoryEntity> Categories { get; set; } = default!;
        public DbSet<RewardsEntity> Rewards { get; set; } = default!;
    }
}
