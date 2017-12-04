using System;
using System.Collections.Generic;
using Flunt.Validations;
using HanoiTower.Domain.Enums;
using HanoiTower.Shared.Entities;

namespace HanoiTower.Domain.Entities
{
    public class Hanoi : Entity
    {
        
        protected Hanoi() { }
        public Hanoi(int totalDisks) : base()
        {
            TotalDisks = totalDisks;
            EStatus = EStatus.Processing;

            AddNotifications(new Contract().IsGreaterThan(totalDisks, 1, "TotalDisks", "The Hanoi Tower needs at least 2 disks to be played"));
        }

        public int TotalDisks { get; protected set; }
        public EStatus EStatus { get; protected set; }
        public DateTime? End { get; protected set; }
        public virtual ICollection<Move> Moves { get; set; }

        public void EndHanoi()
        {
            End = DateTime.Now;
            EStatus = EStatus.Closed;
        }

        

    }
}