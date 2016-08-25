using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Entity
{
   public class Book:BaseEntity
    {
       /// <summary>
       /// 书名
       /// </summary>
       public string Title { get; set; }

       /// <summary>
       /// 作者
       /// </summary>
       public string Author { get; set; }

       /// <summary>
       /// ISBN编号
       /// </summary>
       public string ISBN { get; set; }

       /// <summary>
       /// 出版时间
       /// </summary>
       public DateTime PublishedDate { get; set; }
    }
}
