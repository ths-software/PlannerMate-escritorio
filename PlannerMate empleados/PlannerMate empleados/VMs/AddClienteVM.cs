using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PlannerMate_empleados.Classes;
using PlannerMate_empleados.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PlannerMate_empleados.VMs
{
    class AddClienteVM : ObservableObject
    {

        private Cliente clienteToAdd = new Cliente();
        public Cliente ClienteToAdd
        {
            get => clienteToAdd;
            set => SetProperty(ref clienteToAdd, value);
        }

        public RelayCommand AddClienteCommand { get; }
        public RelayCommand CancelarCommand { get; }

        ClientesRESTService clientesRESTService;

        public AddClienteVM()
        {
            clientesRESTService = new ClientesRESTService();
            AddClienteCommand = new RelayCommand(AddCliente);
            CancelarCommand = new RelayCommand(CancelarBtn);
        }

        private void AddCliente()
        {
            if(ClienteToAdd.nombre != null && ClienteToAdd.password != null)
            {
                clientesRESTService.PostCliente(ClienteToAdd);
                MessageBox.Show("Cliente añadido", "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
                ClienteToAdd = new Cliente();

            }
        }

        private void CancelarBtn()
        {
            ClienteToAdd = new Cliente();
        }


    }
}
