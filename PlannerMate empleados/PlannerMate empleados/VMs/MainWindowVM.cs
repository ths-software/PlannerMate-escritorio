using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PlannerMate_empleados.Classes;
using PlannerMate_empleados.Services;
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


        public RelayCommand AbrirCalendario { get; }

        public RelayCommand AbrirGestionDia { get; }

        public RelayCommand AbrirAñadirCita { get; }

        public RelayCommand AbrirEliminarCita { get; }


        private Cita citaSeleccionada = new Cita();
        public Cita CitaSeleccionada // como los botones lo tenemos en el mainWindows, borraremos aqui la cita de la bd.
        {
            get => citaSeleccionada;
            set
            {
                SetProperty(ref citaSeleccionada, value);
            }
        }

        public UserControl ContenidoPrincipal
        {
            get { return contenidoPrincipal; }
            set => SetProperty(ref contenidoPrincipal, value);
        }

        CitasRESTService cService = new CitasRESTService();

        public MainWindowVM()
        {

            AbrirCalendario = new RelayCommand(CargarCalendario);
            AbrirGestionDia = new RelayCommand(CargarGestionDia);
            AbrirAñadirCita = new RelayCommand(CargarAddCita);
            AbrirEliminarCita = new RelayCommand(EliminarCita);

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

        public void EliminarCita()
        {

            CitaSeleccionada = WeakReferenceMessenger.Default.Send<CitaSeleccionadaRequest>();

            if (CitaSeleccionada != null)
            {
                MessageBoxResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar esta cita?", "Confirmación de eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                MessageBox.Show("Cita Eliminada","Correcto", MessageBoxButton.OK, MessageBoxImage.Information);



                if (result == MessageBoxResult.Yes)
                {
                    cService.DeleteCita(CitaSeleccionada.IdCita);


                    ContenidoPrincipal = NavigationServices.LoadCalendario();
                }
            }
            else
            {
                MessageBox.Show("Debes seleccionar una cita primero.");
            }
        }
    }
}
