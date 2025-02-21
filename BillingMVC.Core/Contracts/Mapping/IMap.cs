using System;
using System.Collections.Generic;

namespace BillingMVC.Core.Contracts.Mapping
{
    public interface IMap
    {
        TEndPoint Map<TStartPoint, TEndPoint>
            (TStartPoint source,
            Dictionary<string, string> propMappings = null,
            Dictionary<string, Func<object, object>> customConversions = null)
            where TEndPoint : new();
    }
}