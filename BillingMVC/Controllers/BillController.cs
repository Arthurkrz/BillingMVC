using AutoMapper;
using BillingMVC.Core.Contracts.Services;
using BillingMVC.Core.Entities;
using BillingMVC.Core.Enum;
using BillingMVC.Service.Filters;
using BillingMVC.Service.PredicateBuilder;
using BillingMVC.Web.Factories;
using BillingMVC.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BillingMVC.Web.Controllers
{
    public class BillController : Controller
    {
        private readonly IBillService _billService;
        private readonly IMapper _mapper;
        public BillController(IBillService billService, IMapper mapper)
        {
            _billService = billService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            List<Bill> bills = new List<Bill>()
            {
                new Bill()
                {
                    Name = "Luz",
                    Type = BillType.Services,
                    Currency = Currency.Euro,
                    Value = 36,
                    Source = "Copel",
                    ExpirationDate = new DateTime(2025, 02, 10),
                    IsPaid = true
                },
                new Bill()
                {
                    Name = "Agua",
                    Type = BillType.Services,
                    Currency = Currency.Euro,
                    Value = 6.7,
                    Source = "Sanepar",
                    ExpirationDate = new DateTime(2025, 01, 10),
                    IsPaid = false
                }
            };

            var billsVM = _mapper.Map<List<BillViewModel>>(bills);
            //bills = _billService.GetAllFromCurrentMonth();

            return View(billsVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BillViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var factory = new BillEntityFactory();
            var entity = factory.CreateBillEntityFactory(model);
            entity = _mapper.Map<Bill>(model);
            _billService.CreateBill(entity);

            TempData["SuccessMessage"] = "Conta cadastrada com sucesso!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Filter(BillFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<BillFilter>(filterViewModel);
            var filterExpression = BillPredicateBuilder.Build(filterModel);
            var filteredBills = _billService.GetBillsWithFilter(filterExpression);
            var billsViewModel = _mapper.Map<List<BillViewModel>>(filteredBills);
            filterViewModel.Bills = billsViewModel;

            return View("Index", filterViewModel);
        }
    }
}
