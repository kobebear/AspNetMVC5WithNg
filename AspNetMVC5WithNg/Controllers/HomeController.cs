using AspNetMVC5WithNg.Models;
using AspNetMVC5WithNg.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMVC5WithNg.Controllers
{
    public class HomeController : Controller
    {
        //私有属性: 保存操作客户的业务对象
        private CustomerService objCust;
        //构造函数,用于创建一个操作客户的业务对象实例
        public HomeController()
        {
            this.objCust = new CustomerService();
        }
        //不能去掉，默认用于响应schtml页面中@Html.ActionLink("主页", "Index", "Home")超链接，加载首页index.cshtml
        public ActionResult Index()
        {
            return View();
        }

        // GET: All Customer
        [HttpGet]
        public JsonResult GetAllData()
        {
            int Count = 10; IEnumerable<object> customers = null;
            try
            {
                object[] parameters = { Count };
                customers = objCust.GetAll(parameters);
            }
            catch { }
            return Json(customers.ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: Get Single Customer
        [HttpGet]
        public JsonResult GetbyID(int id)
        {
            object customer = null;
            try
            {
                object[] parameters = { id };
                customer = this.objCust.GetbyID(parameters);
            }
            catch { }
            return Json(customer, JsonRequestBehavior.AllowGet);
        }

        // POST: Save New Customer
        [HttpPost]
        public JsonResult Insert(Customer model)
        {
            int result = 0; bool status = false;
            if (ModelState.IsValid)
            {
                try
                {
                    object[] parameters = { model.CustName, model.CustEmail };
                    result = objCust.Insert(parameters);
                    if (result == 1)
                    {
                        status = true;
                    }
                    return Json(new { success = status });
                }
                catch { }
            }
            return Json(new
            {
                success = false,
                errors = ModelState.Keys.SelectMany(i => ModelState[i].Errors).Select(m => m.ErrorMessage).ToArray()
            });
        }

        // POST: Update Existing Customer
        [HttpPost]
        public JsonResult Update(Customer model)
        {
            int result = 0; bool status = false;
            if (ModelState.IsValid)
            {
                try
                {
                    object[] parameters = { model.Id, model.CustName, model.CustEmail };
                    result = objCust.Update(parameters);
                    if (result == 1)
                    {
                        status = true;
                    }
                    return Json(new { success = status });
                }
                catch { }
            }
            return Json(new
            {
                success = false,
                errors = ModelState.Keys.SelectMany(i => ModelState[i].Errors).Select(m => m.ErrorMessage).ToArray()
            });
        }

        // POST: Delete Customer
        [HttpPost]
        public JsonResult Delete(int id)
        {
            int result = 0; bool status = false;
            try
            {
                object[] parameters = { id };
                result = objCust.Delete(parameters);
                if (result == 1)
                {
                    status = true;
                }
            }
            catch { }
            return Json(new
            {
                success = status
            });
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}