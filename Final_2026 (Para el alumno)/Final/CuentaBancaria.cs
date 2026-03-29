using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.ISTEA
{
    public abstract class CuentaBancaria : ICuenta
    {
        protected decimal _Saldo;
        protected string _Titular;

        public abstract decimal Saldo { get; protected set; }
        public abstract string Titular { get; }

        protected CuentaBancaria(string titular, decimal montoInicial)
        {
            _Titular = titular; 
            _Saldo = montoInicial;
        }

        public abstract void Depositar(decimal monto);
       
        public abstract void Retirar(decimal monto);

        public abstract void Transferir(ICuenta cuentaDestino, decimal monto);
    }
}
