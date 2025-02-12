using AutoMapper;
using BillingMVC.Core.Contracts.Services;
using BillingMVC.Core.Entities;
using BillingMVC.Service.Filters;
using BillingMVC.Service.PredicateBuilder;
using BillingMVC.Web.Models;
using BillingMVC.Web.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var bills = _billService.List();
            var billsVM = _mapper.Map<List<BillViewModel>>(bills);

            double euroToRealRate = 5.50;
            double realToEuroRate = 1 / euroToRealRate;

            double totalInReais = billsVM.Sum(bill => bill.Currency == CurrencyVM.Euro ? bill.Value * euroToRealRate : bill.Value);
            double totalInEuros = billsVM.Sum(bill => bill.Currency == CurrencyVM.Real ? bill.Value * realToEuroRate : bill.Value);

            ViewBag.TotalInReais = totalInReais;
            ViewBag.TotalInEuros = totalInEuros;

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
            {
                var errors = ModelState.Values
                                       .SelectMany(v => v.Errors)
                                       .Select(e => e.ErrorMessage)
                                       .ToList();

                return Json(new { success = false, errors = errors });
            }

            var entity = _mapper.Map<Bill>(model);
            var result = _billService.CreateBill(entity);

            if (!result.Success)
            {
                return Json(new { success = false, errors = result.Errors });
            }

            TempData["SuccessMessage"] = "Despesa cadastrada com sucesso!";
            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult Filter()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Filter(BillFilterViewModel filterViewModel)
        {
            var filterModel = _mapper.Map<BillFilter>(filterViewModel);
            var filterExpression = BillPredicateBuilder.Build(filterModel);
            var filteredBills = _billService.GetBillsWithFilter(filterExpression);
            var billsViewModel = _mapper.Map<List<BillViewModel>>(filteredBills);
            filterViewModel.Bills = billsViewModel;

            return View(filterViewModel);
        }

        public IActionResult Delete(Guid id)
        {
           var result = _billService.DeleteBill(id);

            if (!result.Success)
            {
                return Json(new { success = false, errors = result.Errors });
            }

            TempData["SuccessMessage"] = "Despesa deletada com sucesso!";
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Update(BillViewModel billVM)
        {
            if (billVM.Id == Guid.Empty)
            {
                return Json(new { success = false, errors = new List<string> { "ID da despesa inválido." } });
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                                       .SelectMany(v => v.Errors)
                                       .Select(e => e.ErrorMessage)
                                       .ToList();

                return Json(new { success = false, errors = errors });
            }

            var entity = _mapper.Map<Bill>(billVM);
            var result = _billService.UpdateBill(entity);

            if (!result.Success)
            {
                return Json(new { success = false, errors = result.Errors });
            }

            TempData["SuccessMessage"] = "Despesa alterada com sucesso!";
            return Json(new { success = true });
        }
    }
}
