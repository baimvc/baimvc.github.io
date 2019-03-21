using BlogDemo.Core.Entities;
using BlogDemo.Core.Request;
using BlogDemo.Infrastructure.Database;
using BlogDemo.Infrastructure.Extensions;
using BlogDemo.Infrastructure.Resources;
using BlogDemo.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlogDemo.Infrastructure.Imp
{
    public class SysMenuRepository
    {
        private readonly MyDBContext _db;
        private readonly IPropertyMappingContainer _propertyMappingContainer;

        public SysMenuRepository(
            MyDBContext db,
            IPropertyMappingContainer propertyMappingContainer
        )
        {
            _db = db;
            _propertyMappingContainer = propertyMappingContainer;
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="banner"></param>

        public void AddSysMenu(AddSysMenu sysMenu)
        {
            if (sysMenu != null)
            {
                var sysMenuModel = new SysMenu()
                {
                    ParentID = sysMenu.ParentID,
                    Name = sysMenu.Name,
                    Url = sysMenu.Url,
                    Area = sysMenu.Area,
                    Controller = sysMenu.Controller,
                    Action = sysMenu.Action,
                    Sort = sysMenu.Sort,
                    Picname = sysMenu.Picname,
                    Level = sysMenu.Level,
                    CreatorName = "admin",
                    CreatorTime = DateTime.Now
                };
                _db.SysMenu.Add(sysMenuModel);
            }

        }
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="banner"></param>
        public void EditSysMenu(EditSysMenu sysMenu)
        {
            var editSysMenu = _db.SysMenu.Find(sysMenu.Id);
            if (editSysMenu != null)
            {
                editSysMenu.ParentID = sysMenu.ParentID;
                editSysMenu.Name = sysMenu.Name;
                editSysMenu.Url = sysMenu.Url;
                editSysMenu.Area = sysMenu.Area;
                editSysMenu.Controller = sysMenu.Controller;
                editSysMenu.Action = sysMenu.Action;
                editSysMenu.Sort = sysMenu.Sort;
                editSysMenu.Picname = sysMenu.Picname;
                editSysMenu.Level = sysMenu.Level;
                editSysMenu.UpdateName = "admin";
                editSysMenu.UpdateTime = DateTime.Now;
                _db.SysMenu.Update(editSysMenu);
            }


        }
        /// <summary>
        /// 获取分页菜单
        /// </summary>
        /// <returns></returns>
        public async Task<PaginatedList<SysMenu>> GetPagingSysMenus(SysMenuQueryParameters sysMenuQueryParameters)
        {
            var qureySysMenu = _db.SysMenu.Where(x => x.State != 1).AsQueryable();
            if (!string.IsNullOrEmpty(sysMenuQueryParameters.Name))
            {
                qureySysMenu = qureySysMenu.Where(x => x.Name.ToLowerInvariant() == sysMenuQueryParameters.Name.ToLowerInvariant());
            }
            //OrderBy 根据字段动态排序
            qureySysMenu = qureySysMenu.ApplySort(sysMenuQueryParameters.OrderBy, _propertyMappingContainer.Resolve<SysMenuResources, SysMenu>());

            var count = await qureySysMenu.CountAsync();

            var data = await qureySysMenu
                .Skip(sysMenuQueryParameters.PageIndex * sysMenuQueryParameters.PageSize)
                .Take(sysMenuQueryParameters.PageSize)
                .ToListAsync();

            return new PaginatedList<SysMenu>(sysMenuQueryParameters.PageIndex, sysMenuQueryParameters.PageSize, count, data);
        }
        /// <summary>
        /// 返回所有的菜单数据
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SysMenu>> GetAllSysMenu()
        {
            return await _db.SysMenu.Where(x => x.State != 1).OrderBy(x => x.Id).ToListAsync();
        }

        /// <summary>
        /// 根据ID获取菜单数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SysMenu> GetSysMenuByIdAsync(int id)
        {
            return await _db.SysMenu.Where(x => x.State != 1).FirstOrDefaultAsync(x => x.Id == id);

        }
        /// <summary>
        /// 根据ID删除菜单数据
        /// </summary>
        /// <returns></returns>
        public void DeleteSysMenuById(int id)
        {
            var delSysMenu = _db.SysMenu.Find(id);
            if (delSysMenu != null)
            {
                delSysMenu.State = 1;
                _db.SysMenu.Update(delSysMenu);
            }

        }
        /// <summary>
        /// 根据条件返回菜单数据
        /// </summary>
        /// <param name="SysMenu"></param>
        /// <returns></returns>

        public async Task<SysMenu> GetSearchOneBanner(Expression<Func<SysMenu, bool>> where)
        {
            return await _db.SysMenu.Where(x => x.State != 1).FirstOrDefaultAsync(where);
        }
    }
}
