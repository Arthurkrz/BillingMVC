using BillingMVC.Core.Contracts.Repositories;
using BillingMVC.Core.Contracts.Services;
using BillingMVC.Core.Entities;
using BillingMVC.Service.Filters;
using BillingMVC.Service.PredicateBuilder;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BillingMVC.Service
{
    public class BillService : IBillService
    {
        private readonly IBillRepository _billRepository;
        public BillService(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        public void CreateBill(Bill bill)
        {
            if (bill.ExpirationDate == default)
            {
                throw new ArgumentException("Data de validade inválida");
            }

            if (bill.ExpirationDate < DateTime.Now.AddMonths(-6) && !bill.IsPaid)
            {
                throw new ArgumentException
                    ("Contas vencidas há mais de 6 meses não podem ser adicionadas.");
            }

            if (bill.ExpirationDate < DateTime.Now.AddYears(-1))
            {
                throw new ArgumentException
                    ("Contas de anos anteriores não podem ser adicionadas.");
            }

            if (!_billRepository.FindIdentical(bill.Name, bill.Currency, bill.Value,
                                               bill.Type, bill.ExpirationDate,
                                               bill.Source, bill.IsPaid))
            {
                _billRepository.Add(bill);
            }
            else
            {
                throw new ArgumentException
                    ("Uma conta idêntica já foi adicionada.");
            }
        }

        public IEnumerable<Bill> GetBillsWithFilter(Expression<Func<Bill, bool>> filter)
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

        public IEnumerable<Bill> GetAllBills()
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