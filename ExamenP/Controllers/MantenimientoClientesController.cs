using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExamenP.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ExamenP.Controllers
{
    public class MantenimientoClientesController : Controller
    {
        public SqlClientConnection slqQueryCnn = new SqlClientConnection();
        string connString = ConfigurationManager.ConnectionStrings["DbTemp"].ConnectionString;
        // GET: MantenimientoClientes
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ClienteTable()

        {
            List<Cliente> Table = new List<Cliente>();
            try
            {
                String SqlQuery = "select Id_Cliente,Nombre,Direccion,Telefono,Nit from Pe_Clientes";
                var ResultQuery = slqQueryCnn.connQuerry(SqlQuery);

                foreach (var Q in ResultQuery)
                {
                    Table.Add(new Cliente()
                    {
                        Id_Cliente = (int)Q[0],
                        Nombre = (string)Q[1],
                        Direccion = (string)Q[2],
                        Telefono = (string)Q[3],
                        Nit = (string)Q[4]
                      

                    });
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }

            return Json(new { data = Table }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetGuardar(string Nombre, string Direccion, int telefono, string Nit)
        {

            string result = "";
              
                try
                {


                    string sql = "Insert Into Pe_Clientes (Nombre,Direccion,Telefono,Nit)Values( @Nombre, @Direccion, @Telefono, @Nit ); "
                + "SELECT CAST(scope_identity() AS int)";
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.Add("@Nombre", SqlDbType.VarChar);
                        cmd.Parameters["@nombre"].Value = Nombre;

                        cmd.Parameters.Add("@Telefono", SqlDbType.Int);
                        cmd.Parameters["@telefono"].Value = telefono;
                    

                        cmd.Parameters.Add("@Direccion", SqlDbType.VarChar);
                        cmd.Parameters["@direccion"].Value = Direccion;

                        cmd.Parameters.Add("@Nit", SqlDbType.VarChar);
                        cmd.Parameters["@nit"].Value = Nit;
                        try
                        {
                            conn.Open();
                            var newProdID = cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            result = "OK";
                            return Json(result, JsonRequestBehavior.AllowGet);
                        }
                    }

                    //string sqlInsert = "Insert Into P_Productos (Id_Producto,Nombre,Codigo,Cantidad,Precio)Values( "+_id+",'" + Nombre + "'," + codigo + "," + cantidad + "," + precio + ");";
                    //slqQueryCnn.insertSql(sqlInsert);
                }
                catch (Exception error)
                {

                }
            





            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get_Edit(int Id)
        {

            List<Cliente> Table = new List<Cliente>();
            try
            {
                String SqlQuery = "select Id_cliente,Nombre,Direccion,Telefono,Nit from Pe_Clientes where Id_Cliente = " + Id;
                var ResultQuery = slqQueryCnn.connQuerry(SqlQuery);

                foreach (var Q in ResultQuery)
                {
                    Table.Add(new Cliente()
                    {
                        Id_Cliente = (int)Q[0],
                        Nombre = (string)Q[1],
                        Direccion = (string)Q[2],
                        Telefono = (string)Q[3],
                        Nit = (string)Q[4]
                    });
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }

            return Json(new { data = Table.ToList() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get_Delete(int Id)
        {
            string result = "";
            try
            {



                string sql = "delete from  Pe_Clientes  where Id_Cliente = " + Id
            + "  SELECT CAST(scope_identity() AS int)";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);


                    try
                    {
                        conn.Open();
                        int newProdID = (Int32)cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        result = "OK";
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }


                }




            }
            catch (Exception e)
            {

            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetUpdate(int Id, string Nombre, string Direccion  , int telefono, string Nit)
        {
            string result = "";
            try
            {



                string sql = "Update  Pe_Clientes Set Nombre = @Nombre, Direccion = @direccion, telefono =@telefono,nit=@nit where Id_Cliente = " + Id
            + "  SELECT CAST(scope_identity() AS int)";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add("@Nombre", SqlDbType.VarChar);
                    cmd.Parameters["@nombre"].Value = Nombre;

                    cmd.Parameters.Add("@Telefono", SqlDbType.Int);
                    cmd.Parameters["@telefono"].Value = telefono;


                    cmd.Parameters.Add("@Direccion", SqlDbType.VarChar);
                    cmd.Parameters["@direccion"].Value = Direccion;

                    cmd.Parameters.Add("@Nit", SqlDbType.VarChar);
                    cmd.Parameters["@nit"].Value = Nit;
                    try
                    {
                        conn.Open();
                        var newProdID = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        result = "OK";
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }


                }




            }
            catch (Exception e)
            {

            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}