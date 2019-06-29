using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExamenP.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ExamenP.Controllers
{
    public class MantenimentoProductoController : Controller
    {
        public SqlClientConnection slqQueryCnn = new SqlClientConnection();
        string connString = ConfigurationManager.ConnectionStrings["DbTemp"].ConnectionString;
        // GET: MantenimentoProducto
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductoTable()

        {
            List<producto> Table = new List<producto>();
            try
            {
                String SqlQuery = "select Id_Producto,Nombre,Codigo,Cantidad,Precio,Medida from Pe_Productos";
                var ResultQuery = slqQueryCnn.connQuerry(SqlQuery);

                foreach (var Q in ResultQuery)
                {
                    Table.Add(new producto()
                    {
                        Id_Producto = (int)Q[0],
                        Nombre = (string)Q[1],
                        Codigo =(int)Q[2],
                        Cantidad = (int)Q[3],
                        Precio = Q[4]==null ?0:  Double.Parse(Q[4].ToString()),
                        Medida = (string)Q[5]
                       
                    });
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
           
            return Json(new { data = Table }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetGuardar(string Nombre,int? codigo ,int? cantidad, decimal  precio,string Medida)
        {
            String result = "error";
            string SqlFind = "Select * from Pe_productos where codigo = " + codigo;
            int resultFind = slqQueryCnn.connQuerry(SqlFind).Count();
            if (resultFind == 0)
            {
                
                try
                {
                  
                   
                    string sql = "Insert Into Pe_Productos (Nombre,Codigo,Medida,Cantidad,Precio)Values(@Nombre,@Codigo,@Medida,@Cantidad,@Precio); "
                + "SELECT CAST(scope_identity() AS int)";
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.Add("@Nombre", SqlDbType.VarChar);
                        cmd.Parameters["@nombre"].Value = Nombre;
                     
                        cmd.Parameters.Add("@Codigo", SqlDbType.Int);
                        cmd.Parameters["@codigo"].Value = codigo;

                        cmd.Parameters.Add("@Cantidad", SqlDbType.Int);
                        cmd.Parameters["@cantidad"].Value = cantidad;

                        cmd.Parameters.Add("@Medida", SqlDbType.VarChar);
                        cmd.Parameters["@medida"].Value = Medida;
                        
                        cmd.Parameters.Add("@Precio", SqlDbType.Decimal);
                        cmd.Parameters["@precio"].Value = precio;
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
            }
              

            
            

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetUpdate(int ID, string Nombre, int? codigo, int? cantidad, decimal precio, string Medida)
        {
            string result = "";
            try
            {
                


                string sql = "Update  Pe_Productos Set Nombre = @Nombre, Codigo = @Codigo, Cantidad = @Cantidad,Precio= @Precio,Medida =@Medida where Id_Producto = " + ID
            + "  SELECT CAST(scope_identity() AS int)";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add("@Nombre", SqlDbType.VarChar);
                    cmd.Parameters["@nombre"].Value = Nombre;
                    cmd.Parameters.Add("@Medida", SqlDbType.VarChar);
                    cmd.Parameters["@Medida"].Value = Nombre;
                    cmd.Parameters.Add("@Codigo", SqlDbType.Int);
                    cmd.Parameters["@codigo"].Value = codigo;
                    cmd.Parameters.Add("@Cantidad", SqlDbType.Int);
                    cmd.Parameters["@cantidad"].Value = cantidad;

                    cmd.Parameters.Add("@Precio", SqlDbType.Decimal);
                    cmd.Parameters["@precio"].Value = precio;
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




            }catch(Exception e)
            {

            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Get_Delete(int ID)
        {
            string result = "";
            try
            {
                


                string sql = "delete from  Pe_Productos  where Id_Producto = " + ID
            + "  SELECT CAST(scope_identity() AS int)";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                   

                    try
                    {
                        conn.Open();
                        int newProdID = (Int32)cmd.ExecuteScalar();
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
        public ActionResult Get_Edit (int Id)
        {

            List<producto> Table = new List<producto>();
            try
            {
                String SqlQuery = "select Id_Producto,Codigo,Nombre,Medida,Cantidad,Precio from Pe_Productos where Id_Producto = "+Id;
                var ResultQuery = slqQueryCnn.connQuerry(SqlQuery);

                foreach (var Q in ResultQuery)
                {
                    Table.Add(new producto()
                    {
                        Id_Producto = (int)Q[0],
                        Codigo = (int)Q[1],
                        Nombre = (string)Q[2],
                        Medida =(string)Q[3],
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
    }
}