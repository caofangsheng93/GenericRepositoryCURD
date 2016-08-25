using EF.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

       public Repository(EFDbContext context)
       {
           this.db = context;
       }

       /// <summary>
       /// 泛型方法
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public T GetById(int id)
       {
           return this.entities.Find(id);
       }

       public void 


    }
}
