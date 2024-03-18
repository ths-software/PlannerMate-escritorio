using PlannerMate_empleados.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PlannerMate_empleados.Services
{
    static class NavigationServices
    {
        //public static UserControl LoadGestionDia() => new GestionDia();


        public static UserControl LoadCalendario()
        {
            return new Calendario();
        }

    }
}
