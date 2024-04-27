using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PlannerMate_empleados.Classes;
using PlannerMate_empleados.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static PlannerMate_empleados.Services.DiaSeleccionadoMessage;

namespace PlannerMate_empleados.VMs
{
    internal class EditarClienteVM : ObservableObject
    {
        private Cliente clienteToEdit;
        public Cliente ClienteToEdit
        {
            get => clienteToEdit;
            set
            {
                SetProperty(ref clienteToEdit, value);
                
            }
        }
        ClientesRESTService clientesRESTService;

        public RelayCommand UpdateClienteCommand { get; }

        private string visibilidad = "Visible";
        public string Visibilidad
        {
            get => visibilidad;
            set => SetProperty(ref visibilidad, value);
        }


        public EditarClienteVM()
        {
            clientesRESTService = new ClientesRESTService();

            try
            {
                ClienteToEdit = WeakReferenceMessenger.Default.Send<ClienteSeleccionadoRequest>();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al obtener el cliente. Detalles del error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Visibilidad = "Collapsed";
                return;
            }
            UpdateClienteCommand = new RelayCommand(UpdateCliente);

        }

        private void UpdateCliente()
        {
            clientesRESTService.PutCliente(ClienteToEdit);
            MessageBox.Show("Cliente editado", "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
