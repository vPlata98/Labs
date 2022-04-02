using System;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using Sopra.Lab.App3.ConsoleApp3.Models;
using Microsoft.EntityFrameworkCore;
namespace Sopra.Lab.App3.ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BusquedasComplejas();
        }
        static void BusquedasComplejas()
        {
            var con = new ModelNorthwind();
            // Group by

            // Lineas de pedido agrupadas por pedidos

            var c7 = con.Order_Details.AsEnumerable()
                .GroupBy(c => c.OrderID);
            
            foreach( var order in c7){
                Console.WriteLine($"{order.Key} ");
                foreach (var o in order)
                {
                    Console.WriteLine($"{o.ProductID} {o.UnitPrice}");
                }
            }


            Console.ReadLine();



            // Categoria condiments y seafood
            var c1 = con.Categories.Where(c => c.CategoryName == "condiments" || c.CategoryName == "seafood").Include(c => c.Products);
            foreach(var category in c1)
            {
                Console.WriteLine($"Categoria {category.CategoryID} {category.CategoryName}");
                foreach(var product in category.Products)
                {
                    Console.WriteLine($"Productos: {product.ProductName} {product.CategoryID}");
                }
            }
            Console.ReadLine();

            // Listado de empleados nombre apellidos y listado de pedidos del 97
            var c2 = con.Employees.Include(c => c.Orders.Where(p => p.OrderDate.Value.Year == 1997)).Select(c => new { c.FirstName, c.LastName, c.Orders.Count });
            foreach (var employee in c2)
            {
                Console.WriteLine($"Empleado {employee.FirstName} {employee.LastName} Pedidos {employee.Count}");
                
            }
            Console.ReadLine();
            // Listado de pedidos de los clientes de USA
            var c3 = con.Customers.Where(c => c.Country == "USA").Include(c => c.Orders);
            foreach (var customer in c3)
            {
                Console.WriteLine($"Cliente {customer.CompanyName} {customer.Country}");
                foreach (var order in customer.Orders)
                {
                    Console.WriteLine($"Pedido: {order.OrderID}");
                }
            }
            Console.ReadLine();

            // Clientes que han pedido el producto 57 
            var c4 = con.Order_Details.Include(c => c.Order).Where(c => c.ProductID == 57).Select(c => c.Order.CustomerID);
            foreach (var orderDetail in c4)
            {

                Console.WriteLine($"Cliente {orderDetail} ");

            }
            Console.ReadLine();
            // Clientes que han pedido el producto 72 en 1997 
            var c5 = con.Order_Details.Include(c => c.Order).Where(c => c.ProductID == 72 && c.Order.OrderDate.Value.Year == 1997).Select(c => c.Order.CustomerID);
            foreach (var orderDetail in c5)
            {
                Console.WriteLine($"Cliente {orderDetail} ");

            }
            Console.ReadLine();
            // Coincidentes de las consultas anteriores 
            var c6 = c4.Intersect(c5);
            foreach (var orderDetail in c6)
            {
                Console.WriteLine($"Cliente {orderDetail} ");
                

            }
            Console.ReadLine();
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

            // Listado de empleados que son mayores que sus jefes 
            var c20 = con.Employees.Where(p => p.ReportsTo != null && con.Employees
                .Where(c => p.BirthDate < c.BirthDate)
                .Select(c => (int?)c.EmployeeID)
                .Contains(p.ReportsTo));
            c20.ToList().ForEach(p => Console.WriteLine($"Informacion del empleado: { p.FirstName } { p.EmployeeID } Superior: {p.ReportsTo}"));
            Console.ReadLine();

            // Listado de productos Nombre stock y valor del stock
            var c21 = con.Products.Select(p => new { p.ProductName, p.UnitsInStock, TotalValue = p.UnitsInStock * p.UnitPrice });
            c21.ToList().ForEach(p => Console.WriteLine($"Nombre: {p.ProductName} Stock: {p.UnitsInStock} Valor del stock: {p.TotalValue}"));
            Console.ReadLine();

            // Listado de empleados nombre apellidos y listado de pedidos del 97
            var c22 = con.Employees.Select(p => new
            {
                p.FirstName,
                p.LastName,
                Orders = con.Orders
                    .Count(c => (c.OrderDate.HasValue ? c.OrderDate.Value.Year : 0) == 1997 && c.EmployeeID == p.EmployeeID)
            });
            c22.ToList().ForEach(p => Console.WriteLine($"Empleado {p.FirstName} {p.LastName} {p.Orders} "));
            Console.ReadLine();

            // Tiempo medio de preparacion de pedidos
            //var c23 = con.Orders.Average(p => (p.ShippedDate.HasValue && p.OrderDate.HasValue ? p.ShippedDate.Value.Subtract(p.OrderDate.Value).TotalDays  : 0));
            var c24 = con.Orders.Where(p=> p.ShippedDate.HasValue && p.OrderDate.HasValue).ToList().Average(p => p.ShippedDate.Value.Subtract(p.OrderDate.Value).TotalDays );
            Console.WriteLine($"Tiempo medio de preparacion de pedidos: {c24}");

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
            Console.ReadLine();
            

            

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
