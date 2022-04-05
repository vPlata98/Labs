using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Sopra.Labs.App5.ConsoleApp5
{
    internal class Program
    {
        static HttpClient http = new HttpClient();
        
        static void Main(string[] args)
        {
            FreeParking();
        }
        
        static void FreeParking()
        {
            var token = EMTMadridAT();

            http.DefaultRequestHeaders.Add("accessToken", token);
            var response = http.GetAsync("citymad/places/parkings/availability/").Result;
            if (response.IsSuccessStatusCode)
            {
                var content = JsonConvert.DeserializeObject<Dictionary<dynamic,dynamic>>(response.Content.ReadAsStringAsync().Result);
                var data = (IEnumerable<dynamic>) content["data"];
                Console.WriteLine("Plazas totales de aparcamiento"+ 
                    data.Where(c => (int?) c["freeParking"] != null).Sum(c => (int) c["freeParking"]));
                data.Where(c => (int?)c["freeParking"] != null).ToList()
                    .ForEach(c => {
                        Console.WriteLine("En el aparcamiento "
                        + c.name + " que se encuentra en: " );
                        Console.WriteLine(c.address);
                        Console.WriteLine("Hay "
                        + c.freeParking + " plazas de aparcamiento.");
                    }
                );
            }
            else
            {
                Console.WriteLine(response.ToString());
            }
        }
        static void EMTMadrid()
        {
            var token = EMTMadridAT();
            var param = new Dictionary<string, string>()
            {
                { "cultureInfo", "ES" },
                { "Text_StopRequired_YN", "Y" },
                { "Text_EstimationsRequired_YN", "Y" },
                { "Text_IncidencesRequired_YN", "N" },
            };
            Console.Write("Introduce el numero de parada: ");
            http.DefaultRequestHeaders.Add("accessToken", "6abb3b32-b4e2-11ec-8498-02dc461f16f3");
            var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");
            var response = http.PostAsync($"transport/busemtmad/stops/{Console.ReadLine()}/arrives/", content).Result;
            if (response.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);
                var info = data.data[0].Arrive;
                foreach (var linea in info)
                {
                    //double.TryParse(linea.estimateArrive, out double esperaSeg);
                    Console.WriteLine($"{linea.line} a {String.Format("{0:0.0}",linea.estimateArrive / 60.0)} minutos");
                }
            }
            else
            {
                Console.WriteLine(response.ToString());
            }
        }
        static string EMTMadridAT() 
        {
            http.BaseAddress = new Uri("https://openapi.emtmadrid.es/v2/");
            http.DefaultRequestHeaders.Add("X-ClientId", "d84d5b34-3778-43cd-a491-17a5618bc49c");
            http.DefaultRequestHeaders.Add("passKey", 
                "B6DC937C60C5757D53B3F9CB4CFF89EBA6E39770222C31215478A4547A95AA10B18CAF131121DB75836FE944DE87C5660D3A49C6121B93A9DF159985F85402A5");
            //http.DefaultRequestHeaders.Add("Content-Type", "application/json");

            var response = http.GetAsync("mobilitylabs/user/login/").Result;

            if(response.IsSuccessStatusCode)
            {
                var info = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result); 
                Console.WriteLine($"Token: {info.data[0].accessToken}");
                return info.data[0].accessToken;
            }
            else
            {
                Console.WriteLine(response.ToString());
            }
            return null;    
        }
        static void DeleteStudent()
        {
            //http.BaseAddress = new Uri("http://school.labs.com.es/api/students/");
            Console.WriteLine("Introduce id del estudiante a borrar:");
            Int32.TryParse(Console.ReadLine(), out int id);
            
            var response = http.DeleteAsync(id.ToString()).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Usuario borrado con exito");
            }
            else
            {
                Console.WriteLine(response.ToString());
            }
        }
        static void PutStudentShort()
        {
            http.BaseAddress = new Uri("http://school.labs.com.es/api/students/");
            var alumRef = StudentsInfo();
            Console.Write("Introduce nombre:");
            var firstName = Console.ReadLine();
            Console.Write("Introduce apellido:");
            var lastName = Console.ReadLine();
            Console.Write("Introduce fecha de nacimiento(dd/mm/yyyy):");
            var dateOfBirth = Console.ReadLine();
            DateTime.TryParse(dateOfBirth, out DateTime date);
            Console.Write("Introduce id de clase:");
            Int32.TryParse(Console.ReadLine(), out int classId);
            var alum = new Student()
            {
                Id = alumRef.Id,
                Firstname = firstName,
                Lastname = lastName,
                DateOfBirth = date,
                ClassId = classId
            };
            //var content = new StringContent(JsonConvert.SerializeObject(alum), Encoding.UTF8, "application/json");
            var response = http.PutAsJsonAsync<Student>(alumRef.Id.ToString(), alum).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Usuario cambiado con exito");
            }
            else
            {
                Console.WriteLine(response.ToString());
            }
        }
        static void PutStudent()
        {
            http.BaseAddress = new Uri("http://school.labs.com.es/api/students/");
            var alumRef= StudentsInfo();
            Console.Write("Introduce nombre:");
            var firstName = Console.ReadLine();
            Console.Write("Introduce apellido:");
            var lastName = Console.ReadLine();
            Console.Write("Introduce fecha de nacimiento(dd/mm/yyyy):");
            var dateOfBirth = Console.ReadLine();
            DateTime.TryParse(dateOfBirth, out DateTime date);
            Console.Write("Introduce id de clase:");
            Int32.TryParse(Console.ReadLine(), out int classId);
            var alum = new Student()
            {
                Id = alumRef.Id,
                Firstname = firstName,
                Lastname = lastName,
                DateOfBirth = date,
                ClassId = classId
            };
            var content = new StringContent(JsonConvert.SerializeObject(alum), Encoding.UTF8, "application/json");
            var response = http.PutAsync(alumRef.Id.ToString(), content).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Usuario cambiado con exito");
            }
            else
            {
                Console.WriteLine(response.ToString());
            }
        }
        static void PostStudent()
        {
            
            //http.BaseAddress  = new Uri("http://school.labs.com.es/api/students/");
            Console.Write("Introduce nombre:");
            var firstName = Console.ReadLine();
            Console.Write("Introduce apellido:");
            var lastName = Console.ReadLine();
            Console.Write("Introduce fecha de nacimiento(dd/mm/yyyy):");
            var dateOfBirth = Console.ReadLine();
            DateTime.TryParse(dateOfBirth, out DateTime date);
            Console.Write("Introduce id de clase:");
            Int32.TryParse(Console.ReadLine(), out int classId);
            var alum = new Student()
            {
                Firstname = firstName,
                Lastname = lastName,
                DateOfBirth = date,
                ClassId = classId
            };
            var content = new StringContent(JsonConvert.SerializeObject(alum),Encoding.UTF8,"application/json");
            var response = http.PostAsync("", content).Result;
            if (response.IsSuccessStatusCode) 
            {
                var data = JsonConvert.DeserializeObject<Student>(response.Content.ReadAsStringAsync().Result);
                Console.WriteLine($"{data.Id} {data.Firstname} {data.Lastname}");
            }
            else
            {
                Console.WriteLine(response.ToString());
            }

        }
        
        static void GeoLocationIP()
        {
            //www.ip-api.com/json/193.146.141.207

            // Instanciar el cliente http
            

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

        static void StudentsInfo1()
        {
            //www.ip-api.com/json/193.146.141.207

            // Instanciar el cliente http


            // Definir direccion base ( parte de url que se repite en todas las llamadas)
            //http.BaseAddress = new Uri("http://school.labs.com.es/api/students/");

            // Definir cabeceras

            // Definir el cuerpo del mensaje

            // Llamada al microservicio (API Rest o HTTP based)
            // Metodo o verbo a utilizar: GET
            
            HttpResponseMessage response = http.GetAsync(Console.ReadLine()).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                // Leer el contenido del body

                string content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine($"Contenido en JSON: {content}");

                // Deserializar el objeto
                var info = JsonConvert.DeserializeObject<dynamic>(content);
                Console.WriteLine($"Info: {info.firstName} ");

            }
            else { Console.WriteLine($"Error: {response.StatusCode}"); }

        }
        static Student StudentsInfo()
        {
            Console.Write("Id del estudiante a consultar/modificar: ");
            //http.BaseAddress = new Uri("http://school.labs.com.es/api/students/");
            try
            {
                var infoStudent = http.GetFromJsonAsync<Student>(Console.ReadLine()).Result;
                if (infoStudent != null)
                {

                    Console.WriteLine($"Estudiante: {infoStudent.Id} {infoStudent.Firstname}" +
                    $" {infoStudent.Lastname} {infoStudent.DateOfBirth} {infoStudent.ClassId}");
                    return infoStudent;
                }
                else { Console.WriteLine(infoStudent); }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            return null;
            
            
        }

        static void ZipCodeInfo()
        {
            //www.ip-api.com/json/193.146.141.207

            // Instanciar el cliente http
            

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
                Console.WriteLine($"Info: {info["post code"]} " +
                    $"{info.country} " +
                    $"{info["country abbreviation"]} ");
                foreach (var item in info.places)
                {
                    Console.WriteLine($"Info lugar: {item}");
                }

            }
            else { Console.WriteLine($"Error: {response.StatusCode}"); }
        }
    }
    public class Student
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int ClassId { get; set; }
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
