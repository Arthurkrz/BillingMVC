using AutoMapper;
using BillingMVC.Core.Contracts.Services;
using BillingMVC.Core.Entities;
using BillingMVC.Web.Models;
using BillingMVC.Web.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IActionResult> Index()
        {
            var bills = await _billService.List();
            var billsVM = _mapper.Map<List<BillViewModel>>(bills);

            double euroToRealRate = 5.50;
            double realToEuroRate = 1 / euroToRealRate;

            double totalInReais = billsVM.Sum(bill => bill.Currency == 
            CurrencyVM.Euro ? bill.Value * euroToRealRate : bill.Value);
            double totalInEuros = billsVM.Sum(bill => bill.Currency == 
            CurrencyVM.Real ? bill.Value * realToEuroRate : bill.Value);

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
        public async Task<IActionResult> Create(BillViewModel model)
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
            var result = await _billService.CreateBill(entity);

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
            var filterViewModel = new BillFilterViewModel
            {
                ValueStringRangeStart = string.Empty,
                ValueStringRangeEnd = string.Empty,
                Bills = new List<BillViewModel>()
            };

            return View(filterViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Filter(BillFilterViewModel filterViewModel)
        {
            if (filterViewModel == null || !filterViewModel.Bills.Any()
                && string.IsNullOrWhiteSpace(filterViewModel.NameContains)
                && string.IsNullOrWhiteSpace(filterViewModel.SourceContains)
                && filterViewModel.DateRangeStart == null
                && filterViewModel.DateRangeEnd == null
                && filterViewModel.ValueRangeStart == null
                && filterViewModel.ValueRangeEnd == null
                && filterViewModel.Month == null
                && filterViewModel.Currency == null
                && filterViewModel.Type == null)
            {
                return Json(new { success = false, errors = new List<string> 
                { "Adicione pelo menos 1 parâmetro de busca." } });
            }

            var filterModel = _mapper.Map<BillFilter>(filterViewModel);
            var response = await _billService.GetBillsWithFilter(filterModel);

            if (!response.Success)
            {
                return Json(new { success = false, errors = response.Errors });
            }

            var billsViewModel = _mapper.Map<List<BillViewModel>>(response.Data);

            return PartialView("_BillTable", billsViewModel);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
           var bill = await _billService.DeleteBill(id);

            if (bill == null)
            {
                return RedirectToAction("Index");
            }

            TempData["SuccessMessage"] = "Despesa deletada com sucesso!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(BillViewModel billVM)
        {
            if (billVM.Id == Guid.Empty)
            {
                return Json(new { success = false, errors = new List<string> 
                { "ID da despesa inválido." } });
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
            var result = await _billService.UpdateBill(entity);

            if (!result.Success)
            {
                return Json(new { success = false, errors = result.Errors });
            }

            TempData["SuccessMessage"] = "Despesa alterada com sucesso!";
            return Json(new { success = true });
        }
    }
}
