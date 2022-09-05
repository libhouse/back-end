using System;

namespace LibHouse.Business.Entities.Shared
{
    public abstract class Entity
    {
        public Guid Id { get; }
        public bool Active { get; private set; }
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; }

        public bool IsActive => Active;

        protected Entity()
        {
            Id = Guid.NewGuid();
            Active = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Activate()
        {
            Active = true;
        }

        public void Inactivate()
        {
            Active = false;
        }
    }
}