using BlogDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogDemo.Core.Interface
{
    public interface ISysUserInfoRepository
    {
        Task<SysUserInfo> GetUserForLoginAsync(string account, string password);
    }
}
