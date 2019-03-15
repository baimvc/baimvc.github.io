using BlogDemo.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Infrastructure.Services
{
    public abstract class PropertyMapping<TSource, TDestination> : IPropertyMapping
       where TDestination : EntityBase
    {
        public Dictionary<string, List<MappedProperty>> MappingDictionary { get; }

        protected PropertyMapping(Dictionary<string, List<MappedProperty>> mappingDictionary)
        {
            MappingDictionary = mappingDictionary;
            MappingDictionary[nameof(EntityBase.Id)] = new List<MappedProperty>
            {
                new MappedProperty { Name = nameof(EntityBase.Id), Revert = false}
            };
        }
    }
}
