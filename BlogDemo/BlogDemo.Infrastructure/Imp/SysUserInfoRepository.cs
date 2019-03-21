using BlogDemo.Core.Entities;
using BlogDemo.Core.Interface;
using BlogDemo.Infrastructure.Database;
using BlogDemo.Infrastructure.Services;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BlogDemo.Infrastructure.Imp
{
    public class SysUserInfoRepository: ISysUserInfoRepository
    {
        private readonly MyDBContext _db;
        private readonly IPropertyMappingContainer _propertyMappingContainer;

        public SysUserInfoRepository(
            MyDBContext db,
            IPropertyMappingContainer propertyMappingContainer
        )
        {
            _db = db;
            _propertyMappingContainer = propertyMappingContainer;
        }
        /// <summary>
        /// 判断用户是否登录
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public SysUserInfo GetLoginOn(Expression<Func<SysUserInfo, bool>> where)
        {
            return _db.SysUserInfo.Where(where).FirstOrDefault();
        }
       

    }
}
