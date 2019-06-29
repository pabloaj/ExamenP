using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExamenP.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace ExamenP.Controllers
{

    public class FacturacionController : Controller
    {
        public SqlClientConnection slqQueryCnn = new SqlClientConnection();
        string connString = ConfigurationManager.ConnectionStrings["DbTemp"].ConnectionString;
        // GET: Facturacion
        public ActionResult Index()
        {
            return View();
        }
       public ActionResult Get_Cliente(string Nit)
        {

            List<Cliente> Table = new List<Cliente>();
            try
            {
                String SqlQuery = "select Id_cliente,Nombre,Direccion,Telefono,Nit from Pe_Clientes where NIt like  '" + Nit+"'";
                var ResultQuery = slqQueryCnn.connQuerry(SqlQuery);

                foreach (var Q in ResultQuery)
                {
                    Table.Add(new Cliente()
                    {
                       
                        Nombre = (string)Q[1],
                        Direccion = (string)Q[2],
                        Telefono = (string)Q[3],
                       
                    });
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }

            return Json(new { data = Table.ToList() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get_Producto( int Codigo)
        {

            List<producto> Table = new List<producto>();
            try
            {
                String SqlQuery = "select Id_Producto,Codigo,Nombre,Medida,Cantidad,Precio from Pe_Productos where Codigo =  '" + Codigo + "'";
                var ResultQuery = slqQueryCnn.connQuerry(SqlQuery);

               

                foreach (var Q in ResultQuery)
                {
                    Table.Add(new producto()
                    {
                        Id_Producto = (int)Q[0],
                        Codigo = (int)Q[1],
                        Nombre = (string)Q[2],
                        Medida = (string)Q[3],
                        Cantidad = (int)Q[4],
                        Precio = Q[5] == null ? 0 : double.Parse(Q[5].ToString())
                    });
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }

            return Json(new { data = Table.ToList() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Get_Cantidad(int Codigo, int Cantidad)
        {

            List<producto> Table = new List<producto>();
            try
            {
                String SqlQuery = "select Id_Producto,Codigo,Nombre,Medida,Cantidad,Precio from Pe_Productos where Codigo =  '" + Codigo + "'";
                var ResultQuery = slqQueryCnn.connQuerry(SqlQuery);



                foreach (var Q in ResultQuery)
                {
                    Table.Add(new producto()
                    {
                        Id_Producto = (int)Q[0],
                        Codigo = (int)Q[1],
                        Nombre = (string)Q[2],
                        Medida = (string)Q[3],
                        Cantidad = (int)Q[4],
                        Precio = Q[5] == null ? 0 : double.Parse(Q[5].ToString())
                    });
                }
                int NuevaCantidad = Table.Select(a => a.Cantidad).FirstOrDefault() - Cantidad;
                if (NuevaCantidad >= 0)
                {
                    string sql = "Update  Pe_Productos Set Cantidad = @Cantidad where Codigo = " + Codigo
            + "  SELECT CAST(scope_identity() AS int)";
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.Add("@Cantidad", SqlDbType.Int);
                        cmd.Parameters["@Cantidad"].Value = NuevaCantidad;

                   
                            conn.Open();
                            cmd.ExecuteNonQuery();

                        conn.Close();

                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }

            return Json(new { data = Table.ToList() }, JsonRequestBehavior.AllowGet);
        }
    }
}