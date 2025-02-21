using System;

namespace BillingMVC.Core.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
    }
}