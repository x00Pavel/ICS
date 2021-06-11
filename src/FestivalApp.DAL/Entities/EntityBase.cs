using System;

namespace FestivalApp.DAL.Entities
{
    public abstract record EntityBase : IEntity
    {
        public Guid Id { get; set; }
    }
}
