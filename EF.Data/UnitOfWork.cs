using EF.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Data
{
    public class UnitOfWork : IDisposable
    {
        private readonly EFDbContext db;
        private bool disposed;
        private Dictionary<string, object> repositories;

        public UnitOfWork(EFDbContext context)
        {
            this.db = context;  //构造函数中初始化上下文对象
        }

        public UnitOfWork()
        {
            db = new EFDbContext(); //构造函数中初始化上下文对象
        }

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            disposed = true;
        }
        #endregion

        #region Save
        public void Save()
        {
            db.SaveChanges();
        } 
        #endregion

        #region Repository<T>()
        public Repository<T> Repository<T>() where T : BaseEntity
        {
            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();

            }
            var type = typeof(T).Name;//获取当前成员名称
            if (!repositories.ContainsKey(type))//如果repositories中不包含Name
            {
                var repositoryType = typeof(Repository<>);//获取Repository<>类型
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), db);
                repositories.Add(type, repositoryInstance);

            }
            return (Repository<T>)repositories[type];

        } 
        #endregion


    }
}
