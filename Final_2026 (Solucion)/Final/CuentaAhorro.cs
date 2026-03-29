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
        public CuentaAhorro(string titular, decimal montoInicial) : base(titular, montoInicial) 
        {
            //precondición
            if (string.IsNullOrEmpty(titular) || montoInicial < 0) throw new
                    ArgumentException("Error de inicializacion de cuenta");

            this._Saldo = 0;
            Depositar(montoInicial);
        }

        public override decimal Saldo 
        { 
            get 
            {                
                return _Saldo;
            }
            protected set 
            {                
                _Saldo = value;
                this.Persistir(this.Titular, this._Saldo); //Persiste (inserta) en base de datos cada vez que se modifica el saldo
            }
        }

        public override string Titular        
        {
            get 
            {
                return _Titular;
            }
        }

        #region Implementacion de IPersistible        
        public void Persistir(string titular, decimal saldo)
        {
            const string sql = "INSERT INTO Cuenta (Titular, Saldo) VALUES (@titular, @saldo)";

            using (SqlConnection conexion = new SqlConnection(Config.ConnectionString))
            {
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add("@titular", SqlDbType.VarChar, 100).Value = titular;
                    comando.Parameters.Add("@saldo", SqlDbType.Money).Value = saldo;

                    conexion.Open();

                    comando.ExecuteNonQuery();

                }
            }
        }           
        #endregion

        public override void Depositar(decimal monto)
        {
            if (monto <= 0) throw new ArgumentException("Monto inválido");
            Saldo += monto;
        }

        public override void Retirar(decimal monto)
        {
            if (monto > Saldo) throw new InvalidOperationException("No tiene fondos suficientes.");
            Saldo -= monto;
        }

        public override void Transferir(ICuenta cuentaDestino, decimal monto)
        {
            //Precondicion
            ICuenta cuentaOrigen = this;
            if (object.ReferenceEquals(cuentaOrigen, cuentaDestino))
                throw new InvalidOperationException("La cuanta de origen y destino deben ser diferentes.");

            this.Retirar(monto);
            cuentaDestino.Depositar(monto);
        }
    }
}
