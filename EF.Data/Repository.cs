using EF.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Data
{
    /// <summary>
    /// 泛型仓储类
    /// </summary>
    /// <typeparam name="T"></typeparam>
   public class Repository<T> where T:BaseEntity
    {
       /// <summary>
       /// 数据上下文变量
       /// </summary>
       private readonly EFDbContext db;
       string errorMessage = string.Empty;

       #region 封装属性
       /// <summary>
       /// 实体访问字段
       /// </summary>
       private IDbSet<T> entities;

       /// <summary>
       /// 封装属性
       /// </summary>
       public IDbSet<T> Entities
       {
           get
           {
               if (entities == null)
               {
                   return entities = db.Set<T>();
               }
               else
               {
                   return entities; 
               }
           }
       } 
       #endregion

       #region 构造函数
       public Repository(EFDbContext context)
       {
           this.db = context;
       } 
       #endregion

       #region 泛型方法--根据Id查找实体
       /// <summary>
       /// 泛型方法--根据Id查找实体
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public T GetById(int id)
       {
           return this.Entities.Find(id);
       } 
       #endregion

       #region Insert
       public void Insert(T entity)
       {
           try
           {
               if (entity == null)
               {
                   throw new ArgumentNullException("entity");
               }
               else
               {
                   this.Entities.Add(entity);//把实体的状态设置为Added
                   this.db.SaveChanges();//保存到数据库
               }
           }
           catch (DbEntityValidationException dbEx)//DbEntityValidationException
           {
               foreach (var validationErrors in dbEx.EntityValidationErrors)
               {
                   foreach (var item in validationErrors.ValidationErrors)
                   {
                       errorMessage += string.Format("Property:{0} Error:{1}", item.PropertyName, item.ErrorMessage) + Environment.NewLine;
                   }
               }
               throw new Exception(errorMessage, dbEx);
           }
       } 
       #endregion

       #region Update
       public void Update(T entity)
       {
           try
           {
               if (entity == null)
               {
                   throw new ArgumentNullException("entity");
               }
               else
               {
                   this.db.SaveChanges();//保存到数据库
               }
           }
           catch (DbEntityValidationException dbEx)
           {
               foreach (var validationErrors in dbEx.EntityValidationErrors)
               {
                   foreach (var error in validationErrors.ValidationErrors)
                   {
                       errorMessage += string.Format("Property:{0} Error:{1}", error.PropertyName, error.ErrorMessage) + Environment.NewLine;
                   }
               }
               //抛出捕获的异常
               throw new Exception(errorMessage, dbEx);
           }
       } 
       #endregion

       #region Delete
       public void Delete(T entity)
       {
           try
           {
               if (entity == null)
               {
                   throw new ArgumentException("entity");
               }
               else
               {
                   this.db.Entry(entity).State = EntityState.Deleted;
                   this.db.SaveChanges();
               }
           }
           catch (DbEntityValidationException dbEx)
           {
               foreach (var validationErrors in dbEx.EntityValidationErrors)
               {
                   foreach (var error in validationErrors.ValidationErrors)
                   {
                       errorMessage += string.Format("Property:{0} Error:{1}", error.PropertyName, error.ErrorMessage) + Environment.NewLine;
                   }
               }

               throw new Exception(errorMessage, dbEx);
           }
       } 
       #endregion

       #region Table
       public virtual IQueryable<T> Table
       {
           get
           {
               return this.Entities;
           }
       } 
       #endregion


    }
}
