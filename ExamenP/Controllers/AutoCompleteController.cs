using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExamenP.Models;
using System.Data.SqlClient;

namespace ExamenP.Controllers
{
    public class AutoCompleteController : Controller
    {
        SqlClientConnection SqlQuery = new SqlClientConnection();
        // GET: AutoComplete
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetNit(string term)
        {
            List<AutoComplete> list = new List<AutoComplete>();
            string Query = "select distinct top(10) Nit from Pe_cliente where Nit like '" + term + "%'";
            var query = SqlQuery.connQuerry(Query);
            foreach (var data in query)
            {
                list.Add(new AutoComplete()
                {
                    Id_name = 0,
                    Name = data[0].ToString().Length > 0 ? (string)data[0] : ""
                });
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}