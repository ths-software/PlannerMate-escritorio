using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
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
    class TrabajadoresRESTService
    {

        public ObservableCollection<Trabajador> GetTrabajadores()
        {
            var client = new RestClient(@"http://plannermate.westeurope.cloudapp.azure.com:8080/PlannerAPIRest/datos");
            var request = new RestRequest("trabajadores", Method.Get); // podria hacer un try catch para capturar que este online o no


            var response = client.Get(request);

            // Deserializar la respuesta a ObservableCollection<Cita>
            return JsonConvert.DeserializeObject<ObservableCollection<Trabajador>>(response.Content);
        }

    }
}
