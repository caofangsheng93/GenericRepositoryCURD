using EF.Data;
using EF.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EF.Web.Controllers
{
    public class BookController : Controller
    {
        //创建并实例化工作单元来
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<Book> bookRepository;

        public BookController()
        {
            //控制器的构造函数中，通过工作单元类的实例，来调用Repository<T>方法，实例化Book仓储
            bookRepository = unitOfWork.Repository<Book>();
        }

       
        #region Index
        public ActionResult Index()
        {
            IEnumerable<Book> lstBooks = bookRepository.Table.ToList();
            return View(lstBooks);
        } 
        #endregion

        #region CreateEditBook
        public ActionResult CreateEditBook(int? id)
        {
            Book model = new Book();
            if (id.HasValue) //如果ID有值
            {
                model = bookRepository.GetById(id.Value);
                return View(model);
            }
            else
            {
                return View(model);
            }
        } 
       

        [HttpPost]
        public ActionResult CreateEditBook(Book model)
        {
            if (model.ID == 0)//ID为0表示是添加
            {
                model.AddedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.IP = Request.UserHostAddress;//获取客户端的主机地址
                bookRepository.Insert(model);
            }
            else //ID不为0,表示是修改
            {
               Book editModel=  bookRepository.GetById(model.ID);
               editModel.Author = model.Author;
               editModel.ISBN = model.ISBN;
               editModel.Title = model.Title;
               editModel.PublishedDate = model.PublishedDate;
               editModel.ModifiedDate = DateTime.Now;
               editModel.IP = Request.UserHostAddress;
               bookRepository.Update(editModel);

            }
            if (model.ID > 0)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        #endregion

        #region DeleteBook
        public ActionResult DeleteBook(int id)
        {
            Book model = bookRepository.GetById(id);
            return View(model);
        }

        [HttpPost, ActionName("DeleteBook")]
        public ActionResult ConfirmDeleteBook(int id)
        {
            Book model = bookRepository.GetById(id);
            bookRepository.Delete(model);
            return RedirectToAction("Index");
        }   
        #endregion

        #region DetailBook
        public ActionResult DetailBook(int id)
        {
            Book model = bookRepository.GetById(id);
            return View(model);
        } 
        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }   
        #endregion
    }
}