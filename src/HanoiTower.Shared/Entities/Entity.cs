using System;
using Flunt.Notifications;

namespace HanoiTower.Shared.Entities
{
    public abstract class Entity : Notifiable
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
            CreateTime = DateTime.Now;
        }

        public Guid Id { get; set; }
        public DateTime CreateTime { get; private set; }
    }
}