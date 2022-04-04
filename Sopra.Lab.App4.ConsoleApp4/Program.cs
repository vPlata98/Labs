using System;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using Sopra.Lab.App4.ConsoleApp4.Models;
using Microsoft.EntityFrameworkCore;

namespace Sopra.Lab.App4.ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EjerciciosExtra();
        }

        static void EjerciciosExtra()
        {
            var con = new ModelNorthwind();

            //Listado de unidades vendidas de cada producto ordenado por número total de unidad
            var c1 = con.Order_Details.Include(c => c.Product)
                .AsEnumerable()
                .GroupBy(c => c.ProductID)
                .OrderBy(c => c.Sum(p => p.Quantity));

            foreach (var c in c1)
            {
                Console.WriteLine($"Producto {c.Key} cantidades vendidas: {c.Sum( p => p.Quantity)} ");
                foreach (var p in c)
                {
                    Console.WriteLine($"{p.OrderID} {p.ProductID} {p.Product.ProductName} {p.Quantity}");
                }
            }
            Console.ReadLine();
            // Importe facturado por cada producto ordenado por producto

            var c2 = con.Order_Details.Include(c => c.Product)
                .OrderBy(c => c.ProductID)
                .AsEnumerable()
                .GroupBy(c => c.ProductID);

            foreach (var c in c2)
            {
                Console.WriteLine($"Producto {c.Key} facturado por producto: {c.Sum( p=> p.Quantity * p.UnitPrice)} ");
                foreach (var p in c)
                {
                    Console.WriteLine($"{p.OrderID} {p.ProductID} {p.Product.ProductName}");
                }
            }
            
            Console.ReadLine();

            // En la tabla Orders tenemos los gastos de envio en el campo Freight.
            // Listado de Pedidos agrupado por Empleado,
            // con número de pedidos, importe total factura en concepto de gastos de envio

            var c3 = con.Orders.Include(c => c.Employee)
                .AsEnumerable()
                .GroupBy(c => c.EmployeeID);
            
            foreach (var c in c3)
            {
                Console.WriteLine($"Empleado: {c.Key} Numero de pedidos realizados por empleado: {c.Count()} " +
                    $"Factura total en gastos de envio: {c.Sum(c => c.Freight)} ");
                
                foreach (var p in c)
                {
                    Console.WriteLine($"{p.OrderID} {p.Freight}");
                }

            }
            Console.ReadLine();

            //En la tabla Orders tenemos el identificador de la empresa de transportes ShipVia.
            //Número de pedidos enviado por cada empresa de transporte.

            var c4 = con.Orders.Include(c => c.ShipViaNavigation)
                .AsEnumerable()
                .GroupBy(c => c.ShipVia);

            foreach (var c in c4)
            {
                Console.WriteLine($"Empresa de transportes: { c.Key} Numero de pedidos: {c.Count()}");

                foreach (var p in c)
                {
                   Console.WriteLine($" {p.ShipViaNavigation.CompanyName}");
                }

            }

            Console.ReadLine();
            //Listado de pedido enviados por la empresa 3 que incluya el OrderID
            //y número de lineas de pedido(registros en Orders_Details)

            var c5 = con.Order_Details.Include(c => c.Order)
                .Where(c => c.Order.ShipVia == 3);

            c5.ToList().ForEach(c => Console.WriteLine($"Pedido: {c.OrderID} " +
                $"{c.Quantity} {c.UnitPrice} Precio total " +
                $"(con gastos de envio {c.Order.Freight}): " +
                $"{c.UnitPrice * c.Quantity + c.Order.Freight}"));


        }
    }
}
