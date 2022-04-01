using System;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using Sopra.Lab.App3.ConsoleApp3.Models;

namespace Sopra.Lab.App3.ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EjerciciosEF();
        }
        static void TrabajandoConADONET()
        {
            //ADO access data object

            // Consulta: SELECT * FROM Customers where Country = 'Spain' order by City

            // Crear una cadena de conexion

            var connectionString = new SqlConnectionStringBuilder()
            {
                DataSource = "LOCALHOST",               //Servidor de base de datos
                InitialCatalog = "NORTHWIND",           // Nombre de base de datos
                UserID = "",                            // Usuario
                Password = "",                          // Contraseña 
                IntegratedSecurity = true,              // Securidad basada en windows (true)


            };

            Console.WriteLine(connectionString.ToString());

            // Creamos un objeto de conexion 
            var connect = new SqlConnection()
            {
                ConnectionString = connectionString.ToString()
            };


            Console.WriteLine($"{connect.ToString()} {connect.State}");
            connect.Open();
            Console.WriteLine($"{connect.ToString()} {connect.State}");
            
            // Consulta contra la BBDD
            var command = new SqlCommand()
            {
                Connection = connect,
                CommandText = "SELECT * FROM Customers where Country = 'Spain' order by City",
            };

            var reader = command.ExecuteReader(); // Comando tipo consulta

            while (reader.Read()) { Console.WriteLine($"ID: {reader["CustomerID"]}"); }

            reader.Close();
            command.Dispose();
            connect.Close();
            connect.Dispose();
        }

        static void EjerciciosEF()
        {
            var con = new ModelNorthwind();

            // clientes de usa

            var c1 = con.Customers.Where(c => c.Country == "USA");
            c1.ToList().ForEach(c => Console.WriteLine($"Clientes de usa {c.CompanyName} {c.Country}"));
            // proveedores de berlin
            Console.ReadLine();
            var c2 = con.Suppliers.Where(c => c.City == "Berlin");
            c2.ToList().ForEach(c => Console.WriteLine($"proveedores de berlin {c.CompanyName} {c.City}"));
            Console.ReadLine();
            // trabajadores con id 1 3 5
            var c3 = con.Employees.Where(c => c.EmployeeID == 1 || c.EmployeeID == 3 || c.EmployeeID == 5);
            c3.ToList().ForEach(c => Console.WriteLine($"trabajadores con id 1 3 5 {c.FirstName} {c.EmployeeID}"));
            Console.ReadLine();
            
            //Productos con stock mayor de cero
            var c4 = con.Products.Where(p => p.UnitsInStock > 0);
            c4.ToList().ForEach(p => Console.WriteLine($"Productos con stock mayor de cero {p.ProductName} {p.UnitsInStock}"));
            Console.ReadLine();
            
            //Productos con stock mayor de cero de los proveedores con id 1, 3 y 5
            var c5 = con.Products.Where(p => p.UnitsInStock > 0 && (p.SupplierID == 1 || p.SupplierID == 3 || p.SupplierID == 5));
            c5.ToList().ForEach(p => Console.WriteLine($"Productos con stock mayor de cero de los proveedores con id 1, 3 y 5 {p.ProductName} {p.SupplierID}"));
            Console.ReadLine();
            
            //Productos precio mayor de 20 y menor 90
            var c6 = con.Products.Where(p => p.UnitPrice > 20 && p.UnitPrice < 90);
            c6.ToList().ForEach(p => Console.WriteLine($"Productos precio mayor de 20 y menor 90 {p.ProductName} {p.UnitPrice}"));
            Console.ReadLine();

            //Pedidos entre 01.01.97 y 15.07.97
            var c7 = con.Orders.Where(p => p.OrderDate >= new DateTime(1997, 1, 1) && p.OrderDate < new DateTime(1997, 7, 15));
            c7.ToList().ForEach(p => Console.WriteLine($"Pedidos entre 01.01.97 y 15.07.97 {p.OrderID} {p.OrderDate}"));
            Console.ReadLine();

            //Pedidos del 97 registrado por los empleados con id 1,3,4 y 8
            int?[] ids = new int?[] { 1, 3, 5, 8 };
            var c8 = con.Orders.Where(p => p.OrderDate.Value.Year == 1997 && ids.Contains(p.EmployeeID));
            c8.ToList().ForEach(p => Console.WriteLine($"Pedidos del 97 registrado por los empleados con id 1,3,4 y 8 {p.OrderDate} {p.EmployeeID}"));
            Console.ReadLine();

            //Pedidos de abril de 96
            var c9 = con.Orders.Where(p => p.OrderDate.Value.Year == 1996 && p.OrderDate.Value.Month == 4);
            c9.ToList().ForEach(p => Console.WriteLine($"Pedidos de abril de 96 {p.OrderDate} "));
            Console.ReadLine();

            //Pedidos del realizado los dia uno de cada mes del año 98
            var c10 = con.Orders.Where(p => p.OrderDate.Value.Year == 1998 && p.OrderDate.Value.Day == 1);
            c10.ToList().ForEach(p => Console.WriteLine($"Pedidos del realizado los dia uno de cada mes del año 98 {p.OrderDate} "));
            Console.ReadLine();

            //Clientes que no tiene fax
            var c11 = con.Customers.Where(p => p.Fax == null);
            c11.ToList().ForEach(p => Console.WriteLine($"Clientes que no tiene fax {p.CompanyName} {p.Fax}"));
            Console.ReadLine();

            //Los 10 productos más baratos
            var c12 = con.Products.OrderBy(p => p.UnitPrice).Take(10);
            c12.ToList().ForEach(p => Console.WriteLine($"Los 10 productos más baratos {p.ProductID} {p.UnitPrice}"));
            Console.ReadLine();

            //Los 10 productos más caros con stock
            var c13 = con.Products.OrderByDescending(p => p.UnitPrice).Take(10);
            c13.ToList().ForEach(p => Console.WriteLine($"Los 10 productos más caros con stock {p.ProductID} {p.UnitPrice}"));
            Console.ReadLine();

            //Empresas de la letra B de UK
            var c14 = con.Customers.Where(p => p.CompanyName.StartsWith("B") && p.Country == "UK");
            c14.ToList().ForEach(p => Console.WriteLine($"Empresas de la letra B de UK {p.CompanyName} {p.Country}"));
            Console.ReadLine();

            //Productos de la categoria 3 y 5
            var c15 = con.Products.Where(p => p.CategoryID == 3 || p.CategoryID == 5);
            c15.ToList().ForEach(p => Console.WriteLine($"Productos de la categoria 3 y 5 {p.ProductName} {p.CategoryID}"));
            Console.ReadLine();

            //Valor total del stock
            var c16 = con.Products.Where(p => p.UnitsInStock != 0 || p.UnitsInStock != null).Sum(p => p.UnitPrice* p.UnitsInStock);
            Console.WriteLine($"Valor total del stock: {c16}");
            Console.ReadLine();
            //Todos los pedidos de clientes de argentina 
            var c17 = con.Customers.Where(p => p.Country == "Argentina").Select(p => p.CustomerID);
            //c17.ToList().ForEach(p => Console.WriteLine($"Todos los clientes de argentina  {p} "));

            var c18 = con.Orders.Where(p => c17.Contains(p.CustomerID));
            //c18.ToList().ForEach(p => Console.WriteLine($"Todos los pedidos de clientes de argentina  {p.CustomerID} {p.OrderID}"));

            var c19 = con.Orders.Where(p => con.Customers.Where(c => c.Country == "Argentina").Select(r => r.CustomerID).Contains(p.CustomerID));
            c18.ToList().ForEach(p => Console.WriteLine($"Todos los pedidos de clientes de argentina  {p.CustomerID} {p.OrderID}"));

        }

        static void TrabajandoConEntityFramework()
        {
            var con = new ModelNorthwind();

            var clienteB = con.Customers.Last();
            
            con.Customers.Remove(clienteB);

            //con.SaveChanges();
            
            // Update
            var clienteU = con.Customers.First();

            clienteU.Region = "Nueva region";

            //con.SaveChanges();
            
            
            // Insercion
            var clienteI = new Customer
            {
                CustomerID = "Demo",
                CompanyName = "Empresa",
                ContactName = "Gerente",
                Country = "Spain",
                Phone = "11",
                Fax = "ww",
                ContactTitle = "gerente",
                Address = "Calle sin numero",
                City = "Madrid",
                Region = "Madrid",
                PostalCode = "21",
            };
            con.Customers.Add(clienteI);

            //con.SaveChanges();
            
            // Consulta
            var clientes = con.Customers.Where(c => c.Country == "Spain").OrderBy(r => r.City);

            clientes.ToList().ForEach(c => Console.WriteLine($"Clientes de españa: {c.ContactName} {c.CompanyName}"));

        }
    }
}
