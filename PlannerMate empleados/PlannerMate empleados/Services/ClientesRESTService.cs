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
    class ClientesRESTService
    {

        public ObservableCollection<Cliente> GetClientes()
        {
            var client = new RestClient(@"http://localhost:8080/planner/datos");
            var request = new RestRequest("clientes", Method.Get);

            var response = client.Get(request);

            return JsonConvert.DeserializeObject<ObservableCollection<Cliente>>(response.Content);
        }

        public RestResponse PostCliente(Cliente cliente)
        {
            var client = new RestClient(@"http://localhost:8080/planner/datos");
            var request = new RestRequest("clientes", Method.Post);
            string data = JsonConvert.SerializeObject(cliente);

            request.AddParameter("application/json", data,ParameterType.RequestBody);
            var response = client.Execute(request);
            return response;
        }

        public RestResponse DeleteCliente(int id)
        {
            var cita = new RestClient(@"http://localhost:8080/planner/datos");
            var request = new RestRequest($"clientes/{id}", Method.Delete);
            var response = cita.Execute(request);

            return response;
        }


    }
}
