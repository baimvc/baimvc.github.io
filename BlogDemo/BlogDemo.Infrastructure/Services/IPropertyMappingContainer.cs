using BlogDemo.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Infrastructure.Services
{
    public interface IPropertyMappingContainer
    {
        void Register<T>() where T : IPropertyMapping, new();
        IPropertyMapping Resolve<TSource, TDestination>() where TDestination : EntityBase;
        bool ValidMappingExistsFor<TSource, TDestination>(string fields) where TDestination : EntityBase;
    }
}
