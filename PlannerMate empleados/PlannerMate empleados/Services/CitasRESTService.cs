using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PlannerMate_empleados.Classes;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerMate_empleados.Services
{
    class CitasRESTService
    {

        // considerar hacer esta funcion asincrona para evitar bloqueos
        public ObservableCollection<Cita> GetCitas()
        {
            var client = new RestClient(@"http://localhost:8080/planner/datos");
            var request = new RestRequest("citas",Method.Get); // podria hacer un try catch para capturar que este online o no


            var response = client.Get(request);

            var format = "yyyy-MM-dd HH:mm:ss";
            var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format };

            // Remover el sufijo de zona horaria [UTC]
            string contentWithoutTimeZone = response.Content.Replace("[UTC]", "");

            // Deserializar la respuesta a ObservableCollection<Cita>
            return JsonConvert.DeserializeObject<ObservableCollection<Cita>>(contentWithoutTimeZone);
        }

    }
}
