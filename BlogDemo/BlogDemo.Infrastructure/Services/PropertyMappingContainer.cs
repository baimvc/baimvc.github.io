using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlogDemo.Core;

namespace BlogDemo.Infrastructure.Services
{
    public class PropertyMappingContainer : IPropertyMappingContainer
    {
        protected internal readonly IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        public void Register<T>() where T : IPropertyMapping, new()
        {
            if(_propertyMappings.All(x => x.GetType() != typeof(T)))
            {
                _propertyMappings.Add(new T());
            }
           
        }

        public IPropertyMapping Resolve<TSource, TDestination>() where TDestination : EntityBase
        {
            var matchingMapping = _propertyMappings.OfType<PropertyMapping<TSource, TDestination>>().ToList();
            if (matchingMapping.Count == 1)
            {
                return matchingMapping.First();
            }

            throw new Exception($"找不到属性映射的实例 <{typeof(TSource)},{typeof(TDestination)}");
        }
        /// <summary>
        /// 验证属性存不存在
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="fields"></param>
        /// <returns></returns>
        public bool ValidMappingExistsFor<TSource, TDestination>(string fields) where TDestination : EntityBase
        {
            var propertyMapping = Resolve<TSource, TDestination>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            var fieldsAfterSplit = fields.Split(',');
            foreach (var field in fieldsAfterSplit)
            {
                var trimmedField = field.Trim();
                var indexOfFirstSpace = trimmedField.IndexOf(" ", StringComparison.Ordinal);
                var propertyName = indexOfFirstSpace == -1 ?
                    trimmedField : trimmedField.Remove(indexOfFirstSpace);
                if (!propertyMapping.MappingDictionary.ContainsKey(propertyName))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
