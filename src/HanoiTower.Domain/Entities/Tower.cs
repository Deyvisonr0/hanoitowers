using System.Collections.Generic;
using System.Linq;
using HanoiTower.Domain.Enums;

namespace HanoiTower.Domain.Entities
{
    public class Tower
    {
        public Tower(ETowerPosition posicao)
        {
            Disks = new Stack<Disk>();
            Position = posicao;
        }

        public Tower(int totalDisks) : this(ETowerPosition.Start)
        {
            for (var i = totalDisks; i >= 1; i--)
            {
                AddDisk(new Disk(i));
            }
        }

        public ETowerPosition Position { get; set; }

        public Stack<Disk> Disks { get; set; }

        public Disk RemoveDisk()
        {
            return Disks.Pop();
        }

        public void AddDisk(Disk disk)
        {
            if (disk == null)
            {
                return;
            }
            Disks.Push(disk);
        }

        public override string ToString()
        {
            return string.Join(", ", Disks.OrderByDescending(x => x.Valor).Select(x => x.ToString()));
        }
    }
}