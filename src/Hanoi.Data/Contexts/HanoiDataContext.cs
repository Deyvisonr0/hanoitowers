using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Hanoi.Data.Mappings;
using HanoiTower.Domain.Entities;

namespace Hanoi.Data.Contexts
{
    public class HanoiDataContext : DbContext
    {
        public HanoiDataContext() : base("DefaultConnection")
        {
        }

        public DbSet<HanoiTower.Domain.Entities.Hanoi> Hanois { get; set; }
        public DbSet<Move> Moves { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
                modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
                modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

                modelBuilder.Configurations.Add(new HanoiMap());
                modelBuilder.Configurations.Add(new MoveMap());
                
            }
        
    }
    
}