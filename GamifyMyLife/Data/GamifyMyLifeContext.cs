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

        public DbSet<Activity> Activities { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;        
    }
}
