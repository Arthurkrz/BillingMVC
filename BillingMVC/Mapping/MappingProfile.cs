using BillingMVC.Core.Contracts.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BillingMVC.Web.Mapping
{
    public class MappingProfile : IMap
    {
        public TTarget Map<TSource, TTarget>(TSource source) where TTarget : new()
        {
            TTarget target = new TTarget();
            Type sourceType = typeof(TSource);
            Type targetType = typeof(TTarget);
            PropertyInfo[] sourceInfo = sourceType.GetProperties();

            foreach (var propSource in sourceInfo)
            {
                PropertyInfo[] targetInfo = targetType.GetProperties();
                foreach (var propTarget in targetInfo)
                {
                    if (propTarget.Name == propSource.Name && 
                        propTarget.PropertyType == propSource.PropertyType && 
                       !propTarget.PropertyType.IsClass && 
                       !propTarget.PropertyType.IsEnum)
                    {
                        object propSourceValue = propSource.GetValue(source);
                        propTarget.SetValue(target, propSourceValue);
                        targetInfo.ToList().Remove(propTarget);
                    }
                    else if (propTarget.Name == propSource.Name && 
                             propTarget.PropertyType != propSource.PropertyType && 
                            !propTarget.PropertyType.IsClass &&
                             propTarget.PropertyType.IsEnum)
                    {
                        object propSourceValue = propSource.GetValue(source);
                        propTarget.SetValue(target, propSourceValue);
                        targetInfo.ToList().Remove(propTarget);
                    }
                    else if (propTarget.PropertyType.IsClass && propSource.PropertyType.IsClass)
                    {
                        var mappedClass = ClassMap<TSource, TTarget>(propTarget, propSource);
                        propTarget.SetValue(target, mappedClass);
                        targetInfo.ToList().Remove(propTarget);
                    }
                }
            }
        }

        private PropertyInfo ClassMap<TSource, TTarget>(TSource source, PropertyInfo targetInfo, PropertyInfo sourceInfo)
        {
            if (!targetInfo.PropertyType.IsPrimitive && targetInfo.PropertyType != typeof(string))
            {
                object classPropValue = targetInfo.GetValue(source);
                foreach (var targetClassProp in classPropValue)
                {

                }
            }
            return sourceInfo;
        }

        private void SpecialMap<TSource, TTarget>(List<PropertyInfo> targetInfo)
        {

        }
        // class map method

        // special map method
    }
}

// MÉTODO SIMPLES DE CONVERSÃO DE MESMO NOME E TIPO
// CONDIÇÕES ESPECIAIS DENTRO DESSE MÉTODO (LISTA, STRINGDOUBLE, DOUBLESTRING)
// LÓGICA RECURSIVA PARA PROPRIEDADES COM CLASSE

// TU

// BACKEND FILTER

// API

// TI