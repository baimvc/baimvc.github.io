using BlogDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BlogDemo.Core.Interface
{
    public interface ISysUserInfoRepository
    {
        SysUserInfo GetLoginOn(Expression<Func<SysUserInfo, bool>> where);
    }
}
