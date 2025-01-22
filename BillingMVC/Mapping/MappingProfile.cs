using AutoMapper;
using BillingMVC.Core.Entities;
using BillingMVC.Core.Enum;
using BillingMVC.Service.Filters;
using BillingMVC.Web.Models;
using BillingMVC.Web.Models.Enum;
using System;

namespace BillingMVC.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Bill, BillViewModel>()
                .ForMember(x => x.Currency, 
                           src => src.MapFrom(
                           x => Enum.Parse<CurrencyVM>(
                           x.Currency.ToString())));

            CreateMap<Bill, BillViewModel>()
               .ForMember(x => x.Type,
                          src => src.MapFrom(
                          x => Enum.Parse<BillTypeVM>(
                          x.Type.ToString())));

            CreateMap<BillViewModel, Bill>()
                .ForMember(x => x.Currency,
                           src => src.MapFrom(
                           x => Enum.Parse<Currency>(
                           x.Currency.ToString())));

            CreateMap<BillViewModel, Bill>()
                .ForMember(x => x.Type,
                           src => src.MapFrom(
                           x => Enum.Parse<BillType>(
                           x.Type.ToString())));

            CreateMap<BillFilter, BillFilterViewModel>()
                .ForMember(x => x.Currency,
                           src => src.MapFrom(
                           x => Enum.Parse<CurrencyVM>(
                           x.Currency.ToString())));

            CreateMap<BillFilter, BillFilterViewModel>()
                .ForMember(x => x.Type,
                           src => src.MapFrom(
                           x => Enum.Parse<BillTypeVM>(
                           x.Type.ToString()))); 

            CreateMap<BillFilterViewModel, BillFilter>()
                .ForMember(x => x.Currency,
                           src => src.MapFrom(
                           x => Enum.Parse<Currency>(
                           x.Currency.ToString())));

            CreateMap<BillFilterViewModel, BillFilter>()
                .ForMember(x => x.Type,
                           src => src.MapFrom(
                           x => Enum.Parse<BillType>(
                           x.Type.ToString())));
        }
    }
}
