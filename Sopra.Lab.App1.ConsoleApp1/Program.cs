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
            Console.WriteLine("Introduce un numero para saber su tabla de multiplicar");
            string numS = Console.ReadLine();
            int num = int.Parse(numS);
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
            Console.WriteLine("Introduce un numero para saber su tabla de multiplicar");
            string numS = Console.ReadLine();
            int num = int.Parse(numS);
            Console.WriteLine($"Tabla del {num}");
            int i = 0;
            while ( i <= 10)
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
            Console.WriteLine("Introduce un valor minimo");
            string minS = Console.ReadLine();
            int min = int.Parse(minS);

            Console.WriteLine("Introduce un valor maximo");
            string maxS = Console.ReadLine();
            int max = int.Parse(maxS);

            Console.WriteLine("Introduce un intervalo");
            string saltoS = Console.ReadLine();
            int salto = int.Parse(saltoS);

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
            Console.WriteLine("Introduce el numero de valores que quieres guardar");
            string lenS = Console.ReadLine();
            int len = int.Parse(lenS);
            int maxValue = -1;
            int minValue = 500;
            int acum = 0;

            int [] numeros = new int[len];
            for (int i = 0; i < len; i++)
            {   
                
                Console.WriteLine("Introduce el numero que quieres guardar");
                numeros[i] = int.Parse(Console.ReadLine());
                acum += numeros[i];
                if (numeros[i] >= maxValue) { maxValue = numeros[i]; }
                else if (numeros[i] < minValue) { minValue = numeros[i]; }
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
            Console.WriteLine("Introduce tu numero del DNI");
            string dniS = Console.ReadLine();
            int dni = int.Parse(dniS);
            char[] letras = { 'T', 'R', 'W', 'A', 'G', 'M', 'Y', 'F', 'P', 'D', 'X', 'B', 'N', 'J', 'Z', 'S', 'Q', 'V', 'H', 'L', 'C', 'K', 'E' };
            Console.WriteLine($"Tu letra del DNI es {letras[dni%23]}");
        }
    }
}
