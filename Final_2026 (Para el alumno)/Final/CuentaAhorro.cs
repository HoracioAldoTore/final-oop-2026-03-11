using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.ISTEA
{
    /// <summary>
    /// Representacion en objetos de una caja de ahorro bancaria.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// El constructor debe evaluar la siguiente precondición: 
    /// Si el “titular” es nulo o string vacío o el “montoInicial” es negativo 
    /// Debe lanzar una excepción del tipo ArgumentException.
    /// </exception>    
    public class CuentaAhorro : CuentaBancaria, IPersistible
    {
        /***
        *   Escriba aquí el código que cumpla con:
        *     1-	La herencia.
        *     2-	La implantación de la interfaz.
        *     3-	La especificación funcional, verificada por los  test de unidad (o unit test).
        ***/
    }
}
