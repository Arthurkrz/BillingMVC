using AutoMapper;
using BillingMVC.Core.Contracts.Services;
using BillingMVC.Core.Entities;
using BillingMVC.Service.Filters;
using BillingMVC.Service.PredicateBuilder;
using BillingMVC.Web.Models;
using Microsoft.AspNetCore.Mvc;
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
            IEnumerable<Bill> bills = _billService.List();

            //bills = _billService.GetAllFromCurrentMonth();
            var billsVM = _mapper.Map<List<BillViewModel>>(bills);

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
                return BadRequest("Conta não cadastrada.");

            var entity = _mapper.Map<Bill>(model);
            _billService.CreateBill(entity);

            TempData["SuccessMessage"] = "Conta cadastrada com sucesso!";
            return RedirectToAction("Index");
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
    }
}
