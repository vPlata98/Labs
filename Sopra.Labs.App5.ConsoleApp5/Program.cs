﻿using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace Sopra.Labs.App5.ConsoleApp5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ZipCodeInfo();
        }
        static void GeoLocationIP()
        {
            //www.ip-api.com/json/193.146.141.207

            // Instanciar el cliente http
            var http = new HttpClient();

            // Definir direccion base ( parte de url que se repite en todas las llamadas)
            http.BaseAddress = new Uri("http://ip-api.com/json/");

            // Definir cabeceras

            // Definir el cuerpo del mensaje

            // Llamada al microservicio (API Rest o HTTP based)
            // Metodo o verbo a utilizar: GET
            HttpResponseMessage response = http.GetAsync("193.146.141.207").Result;

            if(response.StatusCode == HttpStatusCode.OK) 
            {
                // Leer el contenido del body

                string content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine($"Contenido en JSON: {content}");

                // Deserializar el objeto
                var infoIP = JsonConvert.DeserializeObject<dynamic>(content);
                Console.WriteLine($"Info: {infoIP["as"]}{infoIP.isp} {infoIP.country} {infoIP.countryCode} {infoIP.regionName}");

            }
            else { Console.WriteLine($"Error: {response.StatusCode}"); }

            




        }

        static void ZipCodeInfo()
        {
            //www.ip-api.com/json/193.146.141.207

            // Instanciar el cliente http
            var http = new HttpClient();

            // Definir direccion base ( parte de url que se repite en todas las llamadas)
            http.BaseAddress = new Uri("http://api.zippopotam.us/");

            // Definir cabeceras

            // Definir el cuerpo del mensaje

            // Llamada al microservicio (API Rest o HTTP based)
            // Metodo o verbo a utilizar: GET
            Console.WriteLine($"Introduce el codigo de pais (es): ");
            var pais = Console.ReadLine();
            Console.WriteLine($"Introduce codigo postal: ");
            var cp = Console.ReadLine();

            HttpResponseMessage response = http.GetAsync(pais + "/" + cp).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                // Leer el contenido del body

                string content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine($"Contenido en JSON: {content}");

                // Deserializar el objeto
                var info = JsonConvert.DeserializeObject<dynamic>(content);
                Console.WriteLine($"Info: {info.postalCode} " +
                    $"{info.country} " +
                    $"{info.countryAbbreviation} ");
                foreach (var item in info.places)
                {
                    Console.WriteLine($"Info lugar: {item}");
                }

            }
            else { Console.WriteLine($"Error: {response.StatusCode}"); }
        }
    }
    //public class InfoIP2
    //{
    //    public string country { get; set; }
    //    public string city { get; set; }
    //    public string query { get; set; }

    //    //public string as { get; set; }
    //}
    //public class InfoIP
    //{
    //    public string status { get; set; }
    //    public string country { get; set; }
    //    public string countryCode { get; set; }
    //    public string region { get; set; }
    //    public string regionName { get; set; }
    //    public string city { get; set; }
    //    public string zip { get; set; }
    //    public string timezone { get; set; }
    //    public string isp { get; set; }
    //    public string org { get; set; }
    //    public string query { get; set; }
    //    public decimal lat { get; set; }
    //    public decimal lon { get; set; }

    //    //public string as { get; set; }
    //}

    interface IVehiculo
    {
        public string Nombre { get; set; }
        public string Ruedas { get; set; }

        void Arrancar();

        void Parar();
    }

    class Coche : IVehiculo
    {
        public string Nombre { get ; set ; }
        public string Ruedas { get; set; }

        public void Arrancar()
        {
            Console.WriteLine("Coche Arrancando");
        }

        public void Parar()
        {
            Console.WriteLine("Coche Parando");
        }

        //void IVehiculo.Arrancar()
        //{
        //    Console.WriteLine("Coche Arrancando");
        //}

        //void IVehiculo.Parar()
        //{
        //    Console.WriteLine("Coche Parando");
        //}
    }

    class Avion : IVehiculo
    {
        string IVehiculo.Nombre { get; set; }
        string IVehiculo.Ruedas { get; set; }

        void IVehiculo.Arrancar()
        {
            Console.WriteLine("Avion Arrancando");
        }

        void IVehiculo.Parar()
        {
            Console.WriteLine("Avion Parando");
        }

        void Despegar()
        {
            Console.WriteLine("Despegando");
        }

        void Aterrizar()
        {
            Console.WriteLine("Aterrizando");
        }
    }
}