using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using PlannerMate_empleados.Classes;
using PlannerMate_empleados.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PlannerMate_empleados.Services.DiaSeleccionadoMessage;

namespace PlannerMate_empleados.VMs
{
    class ClientesVM : ObservableObject
    {
        private ObservableCollection<Cliente> clientes;
        public ObservableCollection<Cliente> Clientes
        {
            get => clientes;
            set => SetProperty(ref clientes, value);
        }

        private Cliente clienteSeleccionado = new Cliente();
        public Cliente ClienteSeleccionado
        {
            get => clienteSeleccionado;
            set
            {
                SetProperty(ref clienteSeleccionado, value);
                EnviarClienteSeleccionado();

                //WeakReferenceMessenger.Default.Register<ClientesVM, ClienteSeleccionadoRequest>(this, (r, m) =>
                //{
                //    m.Reply(r.ClienteSeleccionado);
                //});
            }
        }

        ClientesRESTService clientesRESTService;



        public ClientesVM()
        {
            clientesRESTService = new ClientesRESTService();
            Clientes = clientesRESTService.GetClientes();
        }



        public void EnviarClienteSeleccionado()
        {

            WeakReferenceMessenger.Default.Reset();

            WeakReferenceMessenger.Default.Register<ClientesVM, ClienteSeleccionadoRequest>(this, (r, m) =>
            {
                m.Reply(r.ClienteSeleccionado);
            });

        }
    }
}
