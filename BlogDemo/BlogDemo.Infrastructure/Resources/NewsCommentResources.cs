using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Infrastructure.Resources
{
    public class NewsCommentResources
    {
        public int Id { get; set; }
        public string NewsName { get; set; }
        public string Contents { get; set; }
        public DateTime? AddTime { get; set; }
        public string Remark { get; set; }
        public string Floor { get; set; }
    }
    public class NewsCommentResourcesValidator : AbstractValidator<NewsCommentResources>
    {
        public NewsCommentResourcesValidator()
        {
            RuleFor(x => x.Contents)
                .NotNull()
                .WithName("评论内容")
                .WithMessage("{PropertyName}是必填的")
                .MaximumLength(200)
                .WithMessage("{PropertyName}的最大长度是{MaxLengh}");
            RuleFor(x => x.AddTime)
                .NotNull()
                .WithName("评论内容");


        }
    }
}
