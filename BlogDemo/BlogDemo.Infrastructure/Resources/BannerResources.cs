using BlogDemo.Core.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Infrastructure.Resources
{
    public class BannerResources
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public DateTime? Updatetime { get; set; }
        public string Remark { get; set; }
    }
    public class BannerResourcesValidator : AbstractValidator<BannerResources>
    {
        public BannerResourcesValidator()
        {
            RuleFor(x => x.Image)
                .NotNull()
                .WithName("图片名称")
                .WithMessage("{PropertyName}是必填的")
                .MaximumLength(200)
                .WithMessage("{PropertyName}的最大长度是{MaxLengh}");
            RuleFor(x => x.Url)
                .NotNull()
                .WithName("图片地址");


        }
    }
}
