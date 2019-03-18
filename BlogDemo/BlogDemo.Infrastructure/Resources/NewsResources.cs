using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDemo.Infrastructure.Resources
{
    public class NewsResources
    {
        public int Id { get; set; }
        public int NewsClassifyId { get; set; }
        public string NewsClassifyName{ get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Contents { get; set; }
        public DateTime? PublishDate { get; set; }
        public int CommentCount { get; set; }
        public string Remark { get; set; }
    }
    public class NewsResourcesValidator : AbstractValidator<NewsResources>
    {
        public NewsResourcesValidator()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .WithName("新闻名称")
                .WithMessage("{PropertyName}是必填的")
                .MaximumLength(50)
                .WithMessage("{PropertyName}的最大长度是{MaxLengh}");
            RuleFor(x => x.Contents)
                .NotNull()
                .WithName("新闻内容");


        }
    }
}
