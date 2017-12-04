using System.Data.Entity.ModelConfiguration;

namespace Hanoi.Data.Mappings
{
    public class HanoiMap : EntityTypeConfiguration<HanoiTower.Domain.Entities.Hanoi>
    {
        public HanoiMap()
        {
            ToTable("Hanoi");
            HasKey(x => x.Id);
            HasMany(x => x.Moves).WithRequired(x => x.Hanoi);
        }
    }
}