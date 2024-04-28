using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PlannerMate_empleados.Classes;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PlannerMate_empleados.Services
{
    class CitasRESTService
    {

        // considerar hacer esta funcion asincrona para evitar bloqueos
        public ObservableCollection<Cita> GetCitas()
        {
            var client = new RestClient(@"http://plannermate.westeurope.cloudapp.azure.com:8080/PlannerAPIRest/datos");
            var request = new RestRequest("citas",Method.Get); // podria hacer un try catch para capturar que este online o no
            string contentWithoutTimeZone = "";

            try
            {
                var response = client.Get(request);
                var format = "yyyy-MM-dd HH:mm:ss";
                var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format };

                // Remover el sufijo de zona horaria [UTC]
                contentWithoutTimeZone = response.Content.Replace("[UTC]", "");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error",$"Error al conectarse con la base de datos {ex}",MessageBoxButton.OK,MessageBoxImage.Error);
            }

            
            // Deserializar la respuesta a ObservableCollection<Cita>
            return JsonConvert.DeserializeObject<ObservableCollection<Cita>>(contentWithoutTimeZone);
        }

        public RestResponse PostCita(Cita cita)
        {
            var client = new RestClient(@"http://plannermate.westeurope.cloudapp.azure.com:8080/datos");
            var request = new RestRequest("citas", Method.Post);
            string data = JsonConvert.SerializeObject(cita);

            request.AddParameter("application/json",data,ParameterType.RequestBody);

            var response = client.Execute(request);

            Trace.WriteLine($"cita request: {response.Content} {response.ErrorException} {response.ErrorMessage}");
            Trace.WriteLine($"data: {data}");

            return response;
        }

        public RestResponse PutCita(Cita cita)
        {
            var client = new RestClient(@"http://plannermate.westeurope.cloudapp.azure.com:8080/datos");
            var request = new RestRequest($"citas", Method.Put);
            string data = JsonConvert.SerializeObject(cita);
            request.AddParameter ("application/json",data,ParameterType.RequestBody);
            var response = client.Execute(request);

            Trace.WriteLine($"cita request: {response.Content} {response.ErrorException} {response.ErrorMessage}");
            Trace.WriteLine($"data: {data}");

            return response;
        }


        public RestResponse DeleteCita(int id)
        {
            var cita = new RestClient(@"http://plannermate.westeurope.cloudapp.azure.com:8080/datos");
            var request = new RestRequest($"citas/{id}", Method.Delete);
            var response = cita.Execute(request);

            return response;
        }

    }
}
