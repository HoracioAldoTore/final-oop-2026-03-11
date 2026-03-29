using Dapper;
using Microsoft.Data.SqlClient;
using Final.ISTEA;
using System.Collections.Generic;
using System.Data;

namespace TestProject1
{
    internal static class Helper
    {
        public static string ConnectionString { get; set; }

        /// <summary>
        /// Elimina todos los registros de la tabla.
        /// </summary>
        public static void DeleteAll()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = $"DELETE FROM Cuenta";
                connection.Execute(sql);
            }
        }

        /// <summary>
        /// Retorna todos los registros o cuentas.
        /// </summary>
        /// <returns></returns>
        public static List<Cuenta> GetCuentas()
        {
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {                
                string sql = @"SELECT Id, Titular, Saldo 
                               FROM Cuenta";

                return db.Query<Cuenta>(sql).ToList();
            }
        }
    }
}
