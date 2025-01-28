using BillingMVC.Core.Contracts.Repositories;
using BillingMVC.Core.Contracts.Services;
using BillingMVC.Core.Entities;
using BillingMVC.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BillingMVC.Service
{
    public class BillService : IBillService
    {
        private readonly BillValidator _validator;
        private readonly IBillRepository _billRepository;
        public BillService
               (IBillRepository billRepository, 
                BillValidator validator)
        {
            _validator = validator;
            _billRepository = billRepository;
        }

        public void CreateBill(Bill bill)
        {
            var result = _validator.Validate(bill);
            if (!result.IsValid)
            {
                var errors = new StringBuilder();
                result.Errors.ForEach(error => errors.AppendLine(error.ErrorMessage));
                throw new InvalidOperationException(errors.ToString());
            }
            else
            {
                if (!_billRepository.FindIdentical(bill.Name, bill.Currency,
                                   bill.Value, bill.Type,
                                   bill.ExpirationDate,
                                   bill.Source, bill.IsPaid,
                                   bill.IsRecurring))
                {
                    _billRepository.Add(bill);
                }
                else
                {
                    throw new ArgumentException
                        ("Uma conta idêntica já foi adicionada.");
                }
            }
        }

        public IEnumerable<Bill> List()
        {
            return _billRepository.GetAll();
        }

        public IEnumerable<Bill> GetBillsWithFilter
                                 (Expression<Func<Bill, bool>> filter)
        {
            return _billRepository.GetBillsWithFilter(filter);
        }

        public void PayBill()
        {
            throw new NotImplementedException();
        }

        public Bill GetSelectedBill(Bill bill)
        {
            throw new NotImplementedException();
        }

        public List<Bill> GetAllFromCurrentMonth()
        {
            throw new NotImplementedException();
        }

        public void UpdateBill(Bill bill)
        {
            throw new NotImplementedException();
        }

        public void DeleteBill(Bill bill)
        {
            throw new NotImplementedException();
        }
    }
}
// notificacoes
// contas recorrentes