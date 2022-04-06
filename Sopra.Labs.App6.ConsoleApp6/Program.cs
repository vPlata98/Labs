using System;
using System.Threading;
using System.Threading.Tasks;
using Sopra.Lab.App4.ConsoleApp4.Models;

namespace Sopra.Labs.App6.ConsoleApp6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("INICIO DEL MAIN");

            Calculos();
            Console.WriteLine("FINAL DEL MAIN");
        }

        static void Test1()
        {
            Console.WriteLine("Metodo test");
        }
        static void Test2(string text)
        {
            Console.Write("Metodo test 2 " + text);
        }
        static void Tareas()
        {
            Task tarea1 = new Task(new Action(Test1));
            Task tarea2 = new Task(delegate
            {
                Thread.Sleep(5000);
                Console.WriteLine("Metodo anonimo creado por un delegado");
            });
            Task tarea3 = new Task(() =>
            {
                Console.WriteLine("Metodo anonimo creado por una lambda");
            });
            Task tarea4 = new Task(() => Test1());
            Task tarea5 = Task.Run(
                () => Console.WriteLine("Metodo anonimo, tarea 5"));
            Task<string> tarea6 = Task<string>.Run(
                () => { return "metodo anonimo, tarea 6"; });
            tarea1.Start();
            tarea2.Start();
            tarea2.Wait();
            Task.WaitAll(new Task[] { tarea1, tarea2, tarea3, tarea4, tarea5 }, 2000);
            tarea3.Start();
            tarea4.Start();
        }

        static void Calculos()
        {
            double [] array = new double [50000000];

            var f1 = DateTime.Now;
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = Math.Sqrt(i);
                //Console.WriteLine(i);
            }
            var f2 = DateTime.Now;

            Parallel.For(1, array.Length, i => {
                array[i] = Math.Sqrt(i);
                //Console.WriteLine(i);
            });

            var f3 = DateTime.Now;

            Console.WriteLine("Tiempo 1:" + (f2 - f1) + " tiempo 2: " + (f3 - f2));
        }
    }
}
