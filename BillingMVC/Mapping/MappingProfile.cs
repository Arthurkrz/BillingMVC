using BillingMVC.Core.Contracts.Mapping;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace BillingMVC.Web.Mapping
{
    public class MappingProfile : IMap
    {
        public TTarget Map<TSource, TTarget>(TSource source, Dictionary<string, string> mappingProps = null) where TTarget : new()
        {

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "Objeto de origem não identificado.");
            }

            TTarget target = new TTarget();
            Type sourceType = typeof(TSource);
            Type targetType = typeof(TTarget);

            var sourceProps = sourceType.GetProperties();
            var targetProps = targetType.GetProperties();

            foreach (var sourceProp in sourceProps)
            {
                string targetPropName = mappingProps != null && mappingProps.ContainsKey(sourceProp.Name)
                     ? mappingProps[sourceProp.Name] : sourceProp.Name;

                var targetProp = targetProps.FirstOrDefault(x => x.Name == sourceProp.Name);
                if (targetProp == null) continue;

                if (mappingProps != null && mappingProps.ContainsKey(sourceProp.Name))
                {
                    var mappedSpecial = SpecialMap(source, sourceProp, targetProp);
                    targetProp.SetValue(target, mappedSpecial);
                    continue;
                }

                if (targetProp.PropertyType == sourceProp.PropertyType && 
                   !targetProp.PropertyType.IsClass && 
                   !targetProp.PropertyType.IsEnum)
                {
                    object value = sourceProp.GetValue(source);
                    targetProp.SetValue(target, value);
                }
                else if (targetProp.PropertyType.IsEnum && 
                         sourceProp.PropertyType.IsEnum)
                {
                    object propSourceValue = sourceProp.GetValue(source);
                    targetProp.SetValue(target, propSourceValue);
                }
                else if (targetProp.PropertyType.IsClass && 
                         sourceProp.PropertyType.IsClass && 
                         targetProp.PropertyType != typeof(string))
                {
                    var sourceClassProp = sourceProp.GetValue(source);
                    var mappedClass = ClassMap(sourceClassProp, targetProp.PropertyType);
                    targetProp.SetValue(target, mappedClass);
                }
            }

            return target;
        }

        private object ClassMap(object source, Type targetType)
        {
            if (source == null) return null;

            object target = Activator.CreateInstance(targetType);
            var sourceProps = source.GetType().GetProperties();
            var targetProps = targetType.GetProperties();

            foreach(var sourceProp in sourceProps)
            {
                var targetProp = targetProps.FirstOrDefault(x => x.Name == sourceProp.Name);
                if (targetProp == null) continue;

                if (targetProp.PropertyType == sourceProp.PropertyType &&
                   !targetProp.PropertyType.IsClass &&
                   !targetProp.PropertyType.IsEnum)
                {
                    object value = sourceProp.GetValue(source);
                    targetProp.SetValue(target, value);
                }

                else if (targetProp.PropertyType.IsClass &&
                         sourceProp.PropertyType.IsClass &&
                         targetProp.PropertyType != typeof(string))
                {
                    var sourcePropValue = sourceProp.GetValue(source);
                    var mappedValue = ClassMap(sourcePropValue, targetProp.PropertyType);
                    targetProp.SetValue(target, mappedValue);
                }
            }

            return target;
        }

        private object SpecialMap(object source, PropertyInfo sourceProp, PropertyInfo targetProp)
        {
            if (source == null) return null;

                if (sourceProp.Name == "Value" && 
                    targetProp.Name.Contains("ValueString") && 
                    sourceProp.PropertyType == typeof(double))
                {
                    double value = (double)sourceProp.GetValue(source);
                    return value.ToString("F2", CultureInfo.InvariantCulture);
                }
                
                else if (sourceProp.Name.Contains("ValueString") && 
                         targetProp.Name == "Value" && 
                         sourceProp.PropertyType == typeof(string))
                {
                    string valueString = (string)sourceProp.GetValue(source);
                    if (double.TryParse(valueString, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
                    {
                        return result;
                    }
                    return 0.0;
                }

            return sourceProp.GetValue(source);
        }
    }
}