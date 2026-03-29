using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.ISTEA
{
    /// <summary>
    /// Representacion en objetos de una cuenta bancaria.
    /// </summary>
    public interface ICuenta
    {
        /// <summary>
        /// Nombre del titular de la cuanta.
        /// </summary>
        string Titular { get; }

        /// <summary>
        /// Dinero disponible en la cuanta.
        /// </summary>
        /// <remarks>
        /// Cada vez que el "Saldo" es modificado debe persistirse (insertarse)
        /// un registro en la tabla "[CuentaBancariaDB].[Cuenta]" de la base de datos.
        /// Puede utilizar para esto el metodo IPersistible.Persistir()
        /// </remarks>
        decimal Saldo { get; }

        /// <summary>
        /// Dinero que ingresa en la cuenta.
        /// </summary>
        /// <exception cref="ArgumentException">        
        /// Si el monto es menor o igual a cero el método debe lanzar una excepción.
        /// </exception>
        /// <param name="monto">
        /// Dinero a depositar.
        /// </param>
        void Depositar(decimal monto);

        /// <summary>
        /// Dinero que egresa en la cuenta.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Si el monto es mayor al saldo, el metodo debe lanzar una lanzar una excepción.
        /// </exception>        
        /// <param name="monto">
        /// Dinero a retirar.
        /// </param>
        void Retirar(decimal monto);

        /// <summary>
        /// Dinero que se mueve de una cuenta (origen) a otra (destino).
        /// </summary>
        /// <param name="cuentaDestino">
        /// Cuenta destino.
        /// </param>
        /// <param name="monto">
        /// Dinero a ingresar en la cuenta destino.
        /// </param>
        /// <exception cref="">
        /// Al tratarse de un retiro y un depósito, debe cumplir con los mismos comportamientos
        /// de excepciones, que los métodos Retirar() y Depositar().
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// No puede transferirse a si misma.
        /// Si la cuanta de origen y la de destino son la misma, el metodo debe lanzar una Excepción.
        /// </exception>
        void Transferir(ICuenta cuentaDestino, decimal monto);
    }
}
