using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.ISTEA
{
    public interface IPersistible
    {
        /// <summary>
        /// Inserta un registro en la tabla "[CuentaBancariaDB].[Cuenta]" de la base de datos.
        /// </summary>
        /// <param name="titular">
        /// Nombre del titular de la cuanta.
        /// </param>
        /// <param name="saldo">
        /// Dinero disponible en la cuanta.
        /// </param>
        void Persistir(string titular, decimal saldo);        
    }
}
