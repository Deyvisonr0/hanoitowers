using System.Collections.Generic;
using System.Linq;
using HanoiTower.Domain.Enums;
using HanoiTower.Shared.Entities;

namespace HanoiTower.Domain.Entities
{
    public class Move : Entity
    {
        protected Move() { }
        public Move(List<Tower> towers, Disk disk, int step, Hanoi hanoi)
        {
            Step = step;
            Snapshot = SnapshotGen(towers, disk);
            Hanoi = hanoi;
        }

        public int Step { get; set; }

        public Hanoi Hanoi { get; set; }

        public string Snapshot { get; protected set; }

        private static string SnapshotGen(List<Tower> towers, Disk disk)
        {
            return
                $"Disco {disk} movido\n" +
                $"Torre A: {GetTowerState(towers, ETowerPosition.Start)}\n" +
                $"Torre B: {GetTowerState(towers, ETowerPosition.Mid)}\n" +
                $"Torre C: {GetTowerState(towers, ETowerPosition.End)}\n" +
                "**********************\n";
        }

        private static string GetTowerState(IEnumerable<Tower> torres, ETowerPosition posicaoTorre) => $"{torres.First(x => x.Position == posicaoTorre)}";
    }
}