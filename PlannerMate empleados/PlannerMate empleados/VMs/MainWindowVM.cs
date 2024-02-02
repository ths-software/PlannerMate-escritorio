using CommunityToolkit.Mvvm.ComponentModel;
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
        private UserControl contenidoCalendario;
        public UserControl ContenidoCalendario
        {
            get { return contenidoCalendario; }
            set => SetProperty(ref contenidoCalendario, value);
        }


        public void CargarCalendario()
        {
            ContenidoCalendario = NavigationServices.LoadCalendario();
        }
    }
}
