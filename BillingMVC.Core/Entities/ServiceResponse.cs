﻿using System.Collections.Generic;

namespace BillingMVC.Core.Entities
{
    public class ServiceResponse
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public ServiceResponse() 
        {
            Errors = new List<string>();
        }

        public ServiceResponse(bool success, List<string> errors = null)
        {
            Success = success;
            Errors = errors ?? new List<string>();
        }
    }
}
