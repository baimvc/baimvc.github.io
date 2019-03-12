using BlogDemo.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogDemo.Infrastructure.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDBContext _db;

        public UnitOfWork(MyDBContext db)
        {
            this._db = db;
        }
        public async Task<bool> SaveAsync()
        {
            return await _db.SaveChangesAsync()>0;
        }
    }
}
