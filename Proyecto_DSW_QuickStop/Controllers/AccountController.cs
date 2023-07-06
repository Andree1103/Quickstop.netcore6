using Microsoft.AspNetCore.Mvc;
using Proyecto_DSW_QuickStop.Models;
using System.Data.SqlClient;
namespace Proyecto_DSW_QuickStop.Controllers
{
    public class AccountController : Controller
    {

        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        void connecionString()
        {
            con.ConnectionString = "data source=.;database=BDQUICKSTOP; integrated security = SSPI;";
        }
        [HttpPost]
        public ActionResult Verify(Account acc)
        {
            connecionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "select * from tbl_login where username='"+acc.Name+"'and password='"+acc.Password+"'";
            dr = com.ExecuteReader();
            if(dr.Read())
            {
                con.Close();
                return View("~/Views/Home/Index.cshtml");
            }
            else
            {
                con.Close();
                return View("Error");
            }


        }
             
    }

}
