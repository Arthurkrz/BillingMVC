using BillingMVC.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillingMVC.Core.Contracts.Repositories
{
    public interface IBaseRepository<T> where T : Entity
    {
    }
}
