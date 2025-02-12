using BillingMVC.Core.Contracts.Repositories;
using BillingMVC.Core.Contracts.Services;
using BillingMVC.Core.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BillingMVC.Service
{
    public class BillService : IBillService
    {
        private readonly IValidator<Bill> _validator;
        private readonly IBillRepository _billRepository;   

        public BillService
               (IBillRepository billRepository, 
                IValidator<Bill> validator)
        {
            _validator = validator;
            _billRepository = billRepository;
        }

        public ServiceResponse CreateBill(Bill bill)
        {
            if (bill == null)
            {
                return new ServiceResponse { Success = false, Errors = new List<string> { "A despesa não pode ser nula." } };
            }

            var result = _validator.Validate(bill);

            if (!result.IsValid)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Errors = result.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            _billRepository.Add(bill);
            return new ServiceResponse { Success = true };
        }

        public IEnumerable<Bill> List()
        {
            return _billRepository.GetAll();
        }

        public IEnumerable<Bill> GetBillsWithFilter
                                 (Expression<Func<Bill, bool>> 
                                  filter)
        {
            return _billRepository.GetBillsWithFilter(filter);
        }

        public List<Bill> GetAllFromCurrentMonth()
        {
            throw new NotImplementedException();
        }

        public ServiceResponse UpdateBill(Bill bill)
        {
            var existingBill = _billRepository.GetById(bill.Id);
            if (existingBill == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Errors = new List<string> { "A despesa não foi encontrada." }
                };
            }

            var result = _validator.Validate(bill);
            if (!result.IsValid)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Errors = result.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            _billRepository.Update(bill);
            return new ServiceResponse { Success = true };
        }

        public ServiceResponse DeleteBill(Guid id)
        {
            var entity = _billRepository.GetById(id);

            if (entity == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Errors = new List<string>() { "ID não corresponde a nenhuma despesa." }
                };
            }

            _billRepository.Delete(entity);
            return new ServiceResponse { Success = true };
        }
    }
}