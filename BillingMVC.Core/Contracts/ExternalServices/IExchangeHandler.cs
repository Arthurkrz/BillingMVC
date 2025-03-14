﻿using BillingMVC.Core.DTOS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BillingMVC.Core.Contracts.ExternalServices
{
    public interface IExchangeHandler
    {
        Task<ExchangeResult> GetExchangeOfTheDay();
    }
}
