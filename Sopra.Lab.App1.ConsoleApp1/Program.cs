using System;

namespace Sopra.Lab.App1.ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program.MostrarTablaMultiplicarFor();
            Program.MostrarTablaMultiplicarWhile();
            Program.MostrarValores();
            Program.CalcularValores();
            Program.CalcularLetraDNI();
        }
        /// <summary>
        /// Muestra la tabla de multiplicar del numero, num, usando el bucle for
        /// </summary>
        static void MostrarTablaMultiplicarFor()
        {
            string numS = null;
            int num;
            while (!int.TryParse(numS, out num))
            {
                Console.WriteLine("Introduce un numero para saber su tabla de multiplicar");
                numS = Console.ReadLine();
            }
            
            Console.WriteLine($"Tabla del {num}");
            for (int i = 0; i <= 10; i++)
            {
                Console.WriteLine(num*i);
            }
        }

        /// <summary>
        /// Muestra la tabla de multiplicar del numero, num, usando el bucle while
        /// </summary>
        static void MostrarTablaMultiplicarWhile()
        {
            string numS = null;
            int num;
            while (!int.TryParse(numS, out num))
            {
                Console.WriteLine("Introduce un numero para saber su tabla de multiplicar");
                numS = Console.ReadLine();
            }
            Console.WriteLine($"Tabla del {num}");
            int i = 0;
            while (i <= 10)
            {
                Console.WriteLine(num * i);
                i++;
            }
        }
        /// <summary>
        /// Muestra los valores desde un minimo hasta un maximo con cierto intervalo
        /// </summary>
        static void MostrarValores()
        {
            // Desde valor de inicio hasta valor final
            // con diferentes saltos
            string minS = null;
            string maxS = null;
            string saltoS = null;
            int min;
            int max = 0;
            int salto = 0;
            while (!int.TryParse(minS, out min) || !int.TryParse(maxS, out max) || !int.TryParse(saltoS, out salto))
            {
                Console.WriteLine("Introduce un valor minimo");
                minS = Console.ReadLine();

                Console.WriteLine("Introduce un valor maximo");
                maxS = Console.ReadLine();

                Console.WriteLine("Introduce un intervalo");
                saltoS = Console.ReadLine();

            }
            
            for (int i = min; i <= max; i+=salto)
            {
                Console.WriteLine(i);
            }

        }
        /// <summary>
        /// Pide valores al usuario, los guarda y calcula el maximo, el minimo, la suma 
        /// </summary>
        static void CalcularValores()
        {
            // numero de valores, almacenamos en un array
            // queremos maximo, minimo, media, suma, num
            String lenS = null;
            int len;
            while (!int.TryParse(lenS, out len))
            {
                Console.WriteLine("Introduce el numero de valores que quieres guardar");
                lenS = Console.ReadLine();
            }
            int maxValue = int.MinValue;
            int minValue = int.MaxValue;
            int acum = 0;

            int [] numeros = new int[len];
            string numS = null;
            for (int i = 0; i < len; i++)
            {   
                while (!int.TryParse(numS, out numeros[i]))
                {
                    Console.WriteLine("Introduce el numero que quieres guardar");
                    numS = Console.ReadLine();
                }
                acum += numeros[i];
                if (numeros[i] >= maxValue) { maxValue = numeros[i]; }
                if (numeros[i] < minValue) { minValue = numeros[i]; }
                numS = null;
            }
            Console.WriteLine($"El maximo es {maxValue}");
            Console.WriteLine($"El minimo es {minValue}");
            Console.WriteLine($"La suma es {acum}");
            Console.WriteLine($"La media es {(Double) acum/numeros.Length}");
        }
        /// <summary>
        /// Calcula la letra del DNI en base a este
        /// </summary>
        static void CalcularLetraDNI()
        {
            // numero de dni y nos quedamos con el modulo (numero%23)
            // posicion en el array de la letra
            // Array:
            // array = [T,R,W,A,G,M,Y,F,P,D,X,B,N,J,Z,S,Q,V,H,L,C,K,E]
            int dni;
            string dniS = null;
            while (!int.TryParse(dniS, out dni) || dniS.Length != 8)
            {
                Console.WriteLine("Introduce tu numero del DNI");
                dniS = Console.ReadLine();
            }
            char[] letras = { 'T', 'R', 'W', 'A', 'G', 'M', 'Y', 'F', 'P', 'D', 'X', 'B', 'N', 'J', 'Z', 'S', 'Q', 'V', 'H', 'L', 'C', 'K', 'E' };
            Console.WriteLine($"Tu letra del DNI es {letras[dni%23]}");
        }
    }
}
