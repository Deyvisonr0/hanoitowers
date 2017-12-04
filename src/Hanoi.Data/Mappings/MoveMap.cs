using System.Data.Entity.ModelConfiguration;
using HanoiTower.Domain.Entities;

namespace Hanoi.Data.Mappings
{
    public class MoveMap : EntityTypeConfiguration<Move>
    {
        public MoveMap()
        {
            ToTable("Move");
            HasKey(x => x.Id);
            Property(x => x.Snapshot);
        }
    }
}