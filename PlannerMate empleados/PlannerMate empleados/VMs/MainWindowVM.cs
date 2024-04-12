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
using System.Windows.Controls;
using System.Windows.Navigation;

namespace PlannerMate_empleados.VMs
{
    class MainWindowVM : ObservableObject
    {
        private UserControl contenidoPrincipal;


        public RelayCommand AbrirCalendario { get; }

        public RelayCommand AbrirGestionDia { get; }

        public RelayCommand AbrirAñadirCita { get; }

        public UserControl ContenidoPrincipal
        {
            get { return contenidoPrincipal; }
            set => SetProperty(ref contenidoPrincipal, value);
        }

        public MainWindowVM()
        {

            AbrirCalendario = new RelayCommand(CargarCalendario);
            AbrirGestionDia = new RelayCommand(CargarGestionDia);
            AbrirAñadirCita = new RelayCommand(CargarAddCita);
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
    }
}
