using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Infrastructure.Resources
{
    public class NewsClassifyResources
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
        public string Remark { get; set; }
    }
    public class NewsClassifyResourcesValidator : AbstractValidator<NewsClassifyResources>
    {
        public NewsClassifyResourcesValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .WithName("分类名称")
                .WithMessage("{PropertyName}是必填的")
                .MaximumLength(200)
                .WithMessage("{PropertyName}的最大长度是{MaxLengh}");
            RuleFor(x => x.Sort)
                .NotNull()
                .WithName("分类排序");


        }
    }
}