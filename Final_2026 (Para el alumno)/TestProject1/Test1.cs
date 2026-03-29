using Final.ISTEA;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void TestInitialize()
        {            
            Helper.ConnectionString = Config.ConnectionString;
            Helper.DeleteAll();
        }

        [TestMethod]
        public void Constructor_Titular_01()
        {
            // Arrange
            // Act
            var ahorro = new CuentaAhorro("Sadosky", 100);
            //Assert
            Assert.AreEqual("Sadosky", ahorro.Titular);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_Titular_02()
        {
            // Arrange
            // Act
            var ahorro = new CuentaAhorro("", 100); //El titular no puede ser string de cero caracteres.
            //Assert            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_Titular_03()
        {
            // Arrange
            // Act
            var ahorro = new CuentaAhorro(null, 100); //El titular no puede ser nulo.
            //Assert            
        }

        [TestMethod]
        public void Constructor_Saldo_01()
        {
            // Arrange
            // Act
            var ahorro = new CuentaAhorro("Sadosky", 100);
            //Assert
            Assert.AreEqual(100, ahorro.Saldo);            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_Saldo_02()
        {
            // Arrange
            // Act
            var ahorro = new CuentaAhorro("Sadosky", -10);
            //Assert          
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Retirar_1()
        {
            // Arrange
            var ahorro = new CuentaAhorro("Ana", 100);
            // Act
            ahorro.Retirar(150);
        }

        [TestMethod]
        public void Retirar_2()
        {
            // Arrange
            var ahorro = new CuentaAhorro("Sadosky", 100);
            ahorro.Depositar(100);
            // Act
            ahorro.Retirar(190);
            //Assert
            Assert.AreEqual(10, ahorro.Saldo);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Depositar_1()
        {
            // Arrange
            var ahorro = new CuentaAhorro("Sadosky", 100);            
            // Act
            ahorro.Depositar(0);
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Depositar_2()
        {
            // Arrange
            var ahorro = new CuentaAhorro("Sadosky", 100);
            // Act
            ahorro.Depositar(-10);
            //Assert
        }

        [TestMethod]        
        public void Depositar_3()
        {
            // Arrange
            var ahorro = new CuentaAhorro("Sadosky", 100);
            // Act
            ahorro.Depositar(10);
            //Assert
            Assert.AreEqual(110, ahorro.Saldo); 
        }

        [TestMethod]
        public void Transferir_01()
        {
            // Arrange
            var ahorroA = new CuentaAhorro("Sadosky", 100);
            var ahorroB = new CuentaAhorro("Sabato", 200);
            // Act
            ahorroA.Transferir(ahorroB, 50); //Transfiere de la cuenta "ahorroA" a la "ahorroB"
            //Assert
            Assert.AreEqual(50, ahorroA.Saldo);
            Assert.AreEqual(250, ahorroB.Saldo);
        }
        
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Transferir_02()
        {
            // Arrange
            var ahorroA = new CuentaAhorro("Sadosky", 100);
            var ahorroB = new CuentaAhorro("Sabato", 200);
            // Act
            ahorroA.Transferir(ahorroB, 200); //Transfiere de la cuenta "ahorroA" a la "ahorroB"
            //Assert            
        }

        [TestMethod]        
        public void Transferir_03()
        {
            // Arrange
            var ahorroA = new CuentaAhorro("Sadosky", 100);
            var ahorroB = new CuentaAhorro("Sabato", 200);
            // Act
            ahorroA.Transferir(ahorroB, 50); //Transfiere de la cuenta "ahorroA" a la "ahorroB"
            ahorroB.Transferir(ahorroA, 10); //Transfiere de la cuenta "ahorroB" a la "ahorroA"
            //Assert
            Assert.AreEqual(60, ahorroA.Saldo);
            Assert.AreEqual(240, ahorroB.Saldo);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Transferir_04()
        {
            // Arrange
            var ahorroA = new CuentaAhorro("Sadosky", 100);            
            // Act
            ahorroA.Transferir(ahorroA, 50); //Transfiere de la cuenta "ahorroA" a la "ahorroA". No puede transferirse a si misma.
            //Assert            
        }

        [TestMethod]
        public void Persistir()
        {
            // Arrange
            string titular = "Sadosky" + new Random().Next(1, 100);
            var ahorro = new CuentaAhorro(titular, 100);
            decimal monto = new Random().Next(1, 100);
            Helper.DeleteAll();
            // Act
            ahorro.Persistir(titular, monto);
            //Assert
            List<Cuenta> cuantas = Helper.GetCuentas();
            Assert.AreEqual(1, cuantas.Count);
            Cuenta cuenta = cuantas[0];
            Assert.AreEqual(titular, cuenta.Titular);
            Assert.AreEqual(monto, cuenta.Saldo);
        }

        [TestMethod]
        public void Persistir_Depositar()
        {
            // Arrange
            string titular = "Sadosky" + new Random().Next(1, 100);
            var ahorro = new CuentaAhorro(titular, 100);            
            Helper.DeleteAll();
            // Act
            ahorro.Depositar(10);
            //Assert
            List<Cuenta> cuantas = Helper.GetCuentas();            
            Cuenta cuenta = cuantas[0];            
            Assert.AreEqual(110, cuenta.Saldo);
        }

        [TestMethod]
        public void Persistir_Retirar()
        {
            // Arrange
            string titular = "Sadosky" + new Random().Next(1, 100);
            var ahorro = new CuentaAhorro(titular, 100);
            Helper.DeleteAll();
            // Act
            ahorro.Retirar(10);
            //Assert
            List<Cuenta> cuantas = Helper.GetCuentas();
            Cuenta cuenta = cuantas[0];
            Assert.AreEqual(90, cuenta.Saldo);
        }

        [TestMethod]
        public void Persistir_Transferir()
        {
            // Arrange            
            var ahorroA = new CuentaAhorro("A", 100);
            var ahorroB = new CuentaAhorro("B", 50);
            Helper.DeleteAll();
            // Act
            ahorroA.Transferir(ahorroB, 10);
            //Assert
            List<Cuenta> cuentas = Helper.GetCuentas();
            bool AConSaldoOK = cuentas.Any(c => c.Titular == "A" && c.Saldo == 90);
            bool BConSaldoOK = cuentas.Any(c => c.Titular == "B" && c.Saldo == 60);
            Assert.IsTrue(AConSaldoOK && BConSaldoOK);
        }

        [TestMethod]
        public void Persistir_Constructor()
        {
            // Arrange            

            // Act
            var ahorro = new CuentaAhorro("A", 100);
            //Assert
            List<Cuenta> cuentas = Helper.GetCuentas();
            bool conSaldoOK = cuentas.Any(c => c.Titular == "A" && c.Saldo == 100);
            Assert.IsTrue(conSaldoOK);
        }
    }
}
