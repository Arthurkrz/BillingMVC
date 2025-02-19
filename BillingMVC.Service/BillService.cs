using BillingMVC.Core.Contracts.Repositories;
using BillingMVC.Core.Contracts.Services;
using BillingMVC.Core.Entities;
using BillingMVC.Service.PredicateBuilder;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BillingMVC.Service
{
    public class BillService : IBillService
    {
        private readonly IValidator<Bill> _validatorBill;
        private readonly IValidator<BillFilter> _validatorBillFilter;
        private readonly IBillRepository _billRepository;   

        public BillService
               (IBillRepository billRepository, 
                IValidator<Bill> validatorBill, 
                IValidator<BillFilter> validatorBillFilter)
        {
            _billRepository = billRepository;
            _validatorBill = validatorBill;
            _validatorBillFilter = validatorBillFilter;
        }

        public async Task<ServiceResponse> CreateBill(Bill bill)
        {
            if (bill == null)
            {
                return new ServiceResponse 
                { Success = false, Errors = new List<string> 
                { "A despesa não pode ser nula." } };
            }

            var result = _validatorBill.Validate(bill);

            if (!result.IsValid)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Errors = result.Errors
                                   .Select(e => e.ErrorMessage)
                                   .ToList()
                };
            }

            await _billRepository.Add(bill);
            return new ServiceResponse { Success = true };
        }

        public async Task<IEnumerable<Bill>> List()
        {
            return await _billRepository.GetAll();
        }

        public async Task<ServiceResponseGeneric<IEnumerable<Bill>>> GetBillsWithFilter
                                                                     (BillFilter filter)
        {
            var validationResult = _validatorBillFilter.Validate(filter);
            if (!validationResult.IsValid)
            {
                return new ServiceResponseGeneric<IEnumerable<Bill>>
                {
                    Success = false,
                    Errors = validationResult.Errors
                                             .Select(e => e.ErrorMessage)
                                             .ToList()
                };
            }

            Expression<Func<Bill, bool>> filterExpression = 
                BillPredicateBuilder.Build(filter);

            var bills = await _billRepository.GetBillsWithFilter
                                              (filterExpression);

            return new ServiceResponseGeneric<IEnumerable<Bill>>
            {
                Success = true,
                Data = bills
            };
        }

        public async Task<ServiceResponse> UpdateBill(Bill bill)
        {
            var existingBill = _billRepository.GetById(bill.Id);
            if (existingBill == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Errors = new List<string> 
                    { "A despesa não foi encontrada." }
                };
            }

            var result = _validatorBill.Validate(bill);
            if (!result.IsValid)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Errors = result.Errors
                                   .Select(e => e.ErrorMessage)
                                   .ToList()
                };
            }

            await _billRepository.Update(bill);
            return new ServiceResponse { Success = true };
        }

        public async Task<ServiceResponse> DeleteBill(Guid id)
        {
            var entity = await _billRepository.GetById(id);

            if (entity == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Errors = new List<string>() 
                    { "ID não corresponde a nenhuma despesa." }
                };
            }

            await _billRepository.Delete(entity);
            return new ServiceResponse { Success = true };
        }
    }
}