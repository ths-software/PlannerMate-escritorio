using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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


        public RelayCommand AbrirCalendario;


        public UserControl ContenidoPrincipal
        {
            get { return contenidoPrincipal; }
            set => SetProperty(ref contenidoPrincipal, value);
        }

        public MainWindowVM()
        {

            AbrirCalendario = new RelayCommand(CargarCalendario);
        }




        public void CargarCalendario()
        {
            ContenidoPrincipal = NavigationServices.LoadCalendario();
        }
    }
}
