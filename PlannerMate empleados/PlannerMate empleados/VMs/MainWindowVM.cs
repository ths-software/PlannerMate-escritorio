using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PlannerMate_empleados.Classes;
using PlannerMate_empleados.Services;
using PlannerMate_empleados.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using static PlannerMate_empleados.Services.DiaSeleccionadoMessage;

namespace PlannerMate_empleados.VMs
{
    class MainWindowVM : ObservableObject
    {
        private UserControl contenidoPrincipal;
        public UserControl ContenidoPrincipal
        {
            get { return contenidoPrincipal; }
            set
            {
                SetProperty(ref contenidoPrincipal, value);

                if (ContenidoPrincipal is GestionDia || ContenidoPrincipal is ClientesWin)
                {
                    BotoneraVisibilidad = "Visible";
                }
                else
                {
                    BotoneraVisibilidad = "Collapsed";
                }

            }
        }

        private string botoneraVisibilidad;
        public string BotoneraVisibilidad
        {
            get => botoneraVisibilidad;
            set => SetProperty(ref botoneraVisibilidad, value);
        }

        private string actualizarBtnVisibilidad;
        public string ActualizarBtnVisibilidad
        {
            get => actualizarBtnVisibilidad;
            set => SetProperty(ref actualizarBtnVisibilidad, value);
        }


        public RelayCommand AbrirCalendario { get; }

        public RelayCommand AbrirGestionDia { get; }

        public RelayCommand AbrirAñadir { get; }

        public RelayCommand AbrirEditar { get; }

        public RelayCommand AbrirEliminar { get; }

        public RelayCommand AbrirTrabajadores { get; }

        public RelayCommand AbrirClientes { get; }


        private Cita citaSeleccionada = new Cita();
        public Cita CitaSeleccionada // como los botones lo tenemos en el mainWindows, borraremos aqui la cita de la bd.
        {
            get => citaSeleccionada;
            set
            {
                SetProperty(ref citaSeleccionada, value);
            }
        }

        private Cliente clienteSeleccionado = new Cliente();
        public Cliente ClienteSeleccionado
        {
            get => clienteSeleccionado;
            set
            {
                SetProperty(ref clienteSeleccionado, value);
            }
        }


            CitasRESTService citasService = new CitasRESTService();
        ClientesRESTService clientesRESTService = new ClientesRESTService();

        public MainWindowVM()
        {

            AbrirCalendario = new RelayCommand(CargarCalendario);
            AbrirGestionDia = new RelayCommand(CargarGestionDia);

            AbrirAñadir = new RelayCommand(CargarAddCitaOCliente);
            AbrirEditar = new RelayCommand(CargarEditarCitaOCliente);
            AbrirEliminar = new RelayCommand(EliminarCitaOCliente);

            AbrirTrabajadores = new RelayCommand(CargarTrabajadores);
            AbrirClientes = new RelayCommand(CargarClientes);

        }



        public void CargarCalendario()
        {
            ContenidoPrincipal = NavigationServices.LoadCalendario();
        }
        public void CargarGestionDia()
        {
            ContenidoPrincipal = NavigationServices.LoadGestionDia();

        }
        public void CargarAddCitaOCliente()
        {
            if (ContenidoPrincipal is GestionDia)
            {
                ContenidoPrincipal = NavigationServices.LoadAddCita();
            }
            else
            {
                ContenidoPrincipal = NavigationServices.LoadAddCliente();
            }
        }
        public void CargarEditarCitaOCliente()
        {
            if (ContenidoPrincipal is GestionDia)
            {
                ContenidoPrincipal = NavigationServices.LoadEditarCita();
            }
            else
            {
                ContenidoPrincipal = NavigationServices.LoadEditarCliente();
            }
        }
        public void EliminarCitaOCliente()
        {

            if (ContenidoPrincipal is GestionDia)
            {
                try
                {
                    CitaSeleccionada = WeakReferenceMessenger.Default.Send<CitaSeleccionadaRequest>();

                    if (CitaSeleccionada != null)
                    {
                        MessageBoxResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar esta cita?", "Confirmación de eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);




                        if (result == MessageBoxResult.Yes)
                        {
                            citasService.DeleteCita(CitaSeleccionada.idCita);

                            MessageBox.Show("Cita Eliminada", "Correcto", MessageBoxButton.OK, MessageBoxImage.Information);

                            ContenidoPrincipal = NavigationServices.LoadCalendario();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debes seleccionar una cita primero.");
                    }
                }
                catch
                {
                    MessageBox.Show("Ninguna cita seleccionada u ocurrió un error en el proceso", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                try
                {
                    ClienteSeleccionado = WeakReferenceMessenger.Default.Send<ClienteSeleccionadoRequest>();
                    if (ClienteSeleccionado != null)
                    {
                        MessageBoxResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar este cliente?", "Confirmación de eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            clientesRESTService.DeleteCliente(ClienteSeleccionado.idCliente);

                            MessageBox.Show("Cliente Eliminado", "Correcto", MessageBoxButton.OK, MessageBoxImage.Information);

                            ContenidoPrincipal = NavigationServices.LoadCalendario();
                        }
                        else
                        {
                            MessageBox.Show("Debes seleccionar un cliente primero.");
                        }


                    }
                }
                catch
                {
                    MessageBox.Show("Ningún cliente seleccionado u ocurrió un error en el proceso", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }


        }


        public void CargarTrabajadores()
        {
            ContenidoPrincipal = NavigationServices.LoadTrabajadores();
        }

        public void CargarClientes()
        {
            ContenidoPrincipal = NavigationServices.LoadClientes();
        }
    }
}
