using BlogDemo.Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDemo.Infrastructure.Database
{
    public class MyDBContextSeed
    {
        public static async Task SeedAsync(MyDBContext myDBContext,ILoggerFactory loggerFactory,int retry = 0)
        {
            int retryBillity = retry;
            try
            {
                if (!myDBContext.Banner.Any())
                {
                    myDBContext.Banner.AddRange(
                        new List<Banner> {
                           new Banner{  Image = "图片1", Url="www.tupain.com", AddTime = DateTime.Now, Remark = "好看" },
                           new Banner{  Image = "图片2", Url="www.tupain.com", AddTime = DateTime.Now, Remark = "好看" },
                           new Banner{  Image = "图片3", Url="www.tupain.com", AddTime = DateTime.Now, Remark = "好看" },
                           new Banner{  Image = "图片4", Url="www.tupain.com", AddTime = DateTime.Now, Remark = "好看" },
                           new Banner{  Image = "图片5", Url="www.tupain.com", AddTime = DateTime.Now, Remark = "好看" },
                           new Banner{  Image = "图片6", Url="www.tupain.com", AddTime = DateTime.Now, Remark = "好看" }
                         
                        });
                    myDBContext.NewsClassify.AddRange(
                        new List<NewsClassify> {
                            new NewsClassify{ Name = "娱乐", Sort = 1, Remark = "娱乐" },
                            new NewsClassify{ Name = "搞笑", Sort = 2, Remark = "搞笑" },
                            new NewsClassify{ Name = "军事", Sort = 3, Remark = "军事" },
                            new NewsClassify{ Name = "财经", Sort = 4, Remark = "财经" }
                    });

                    await myDBContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<MyDBContextSeed>();
                logger.LogError(e,"向数据库保存数据出现异常！");
            }
        }

    }
}
