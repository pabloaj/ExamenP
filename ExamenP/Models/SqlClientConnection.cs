using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Transactions;
namespace ExamenP.Models
{
    public class SqlClientConnection
    {
        public List<object[]> connQuerry(string QueryDc)

        {
            string connectString = ConfigurationManager.ConnectionStrings["DbTemp"].ConnectionString;


            List<object[]> dataList = new List<object[]>();

            using (SqlConnection connection = new SqlConnection(connectString))
            {
                using (SqlCommand command = new SqlCommand(QueryDc, connection))
                {
                    connection.Open();
                    command.CommandTimeout = 180;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            object[] tempRow = new object[reader.FieldCount];
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                tempRow[i] = reader[i];
                            }
                            dataList.Add(tempRow);
                        }
                    }
                }
            }
            return dataList;

        }

        public int insertSql(string QueryInsert)
        {
            int valreturn = 0;
            try
            {
                string connectString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
                using (TransactionScope scope = new TransactionScope())
                {
                    using (var conn = new SqlConnection(connectString))
                    {
                        using (var InsertdbSql = new SqlCommand(QueryInsert, conn))
                        {
                            conn.Open();
                           
                            valreturn = (int)InsertdbSql.ExecuteScalar();
                            if (valreturn > 0)
                            {
                                scope.Complete();
                            }
                            else
                            {
                                scope.Dispose();
                            }
                        }
                    }
                }
                return valreturn;
            }
            catch (Exception e)
            {

                return valreturn;
            }
        }
    }
}
    
