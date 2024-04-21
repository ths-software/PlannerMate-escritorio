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

                if (ContenidoPrincipal is GestionDia)
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
            set => SetProperty(ref botoneraVisibilidad,value);
        }

        public RelayCommand AbrirCalendario { get; }

        public RelayCommand AbrirGestionDia { get; }

        public RelayCommand AbrirAñadirCita { get; }

        public RelayCommand AbrirEditarCita { get; }

        public RelayCommand AbrirEliminarCita { get; }

        public RelayCommand AbrirTrabajadores {  get; }


        private Cita citaSeleccionada = new Cita();
        public Cita CitaSeleccionada // como los botones lo tenemos en el mainWindows, borraremos aqui la cita de la bd.
        {
            get => citaSeleccionada;
            set
            {
                SetProperty(ref citaSeleccionada, value);
            }
        }

        

        CitasRESTService cService = new CitasRESTService();

        public MainWindowVM()
        {

            AbrirCalendario = new RelayCommand(CargarCalendario);
            AbrirGestionDia = new RelayCommand(CargarGestionDia);
            AbrirAñadirCita = new RelayCommand(CargarAddCita);
            AbrirEditarCita = new RelayCommand(CargarEditarCita);
            AbrirEliminarCita = new RelayCommand(EliminarCita);
            AbrirTrabajadores = new RelayCommand(CargarTrabajadores);

        }




        public void CargarCalendario()
        {
            ContenidoPrincipal = NavigationServices.LoadCalendario();
        }
        public void CargarGestionDia()
        {
            ContenidoPrincipal = NavigationServices.LoadGestionDia();
            
        }
        public void CargarAddCita()
        {
            ContenidoPrincipal = NavigationServices.LoadAddCita();
        }
        public void CargarEditarCita()
        {
            ContenidoPrincipal = NavigationServices.LoadEditarCita();
        }
        public void EliminarCita()
        {

            try
            {
                CitaSeleccionada = WeakReferenceMessenger.Default.Send<CitaSeleccionadaRequest>();

                if (CitaSeleccionada != null)
                {
                    MessageBoxResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar esta cita?", "Confirmación de eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    



                    if (result == MessageBoxResult.Yes)
                    {
                        cService.DeleteCita(CitaSeleccionada.idCita);

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
                MessageBox.Show("Ninguna cita seleccionada o ocurrió un error en el proceso","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }

            
        }


        public void CargarTrabajadores()
        {
            ContenidoPrincipal = NavigationServices.LoadTrabajadores();
        }
    }
}
