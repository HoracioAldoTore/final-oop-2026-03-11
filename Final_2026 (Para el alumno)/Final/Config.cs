using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.ISTEA
{
    public static class Config
    {
        private static string _ConnectionString = "Data Source=localhost\\SQLEXPRESS; Initial Catalog=CuentaBancariaDB; Integrated Security=True; TrustServerCertificate=True;";
        //private static string _ConnectionString = "Data Source=(localdb)\\DatabaseLocalDB_X; Initial Catalog=CuentaBancariaDB; Integrated Security=True; TrustServerCertificate=True;";

        /// <summary>
        /// Unico ConnectionString a la base de datos, para toda la aplicación.
        /// </summary>
        public static string ConnectionString 
        {
            get { return _ConnectionString; }                
        }
    }
}
