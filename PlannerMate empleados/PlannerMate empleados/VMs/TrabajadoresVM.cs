using CommunityToolkit.Mvvm.ComponentModel;
using PlannerMate_empleados.Classes;
using PlannerMate_empleados.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerMate_empleados.VMs
{
    class TrabajadoresVM : ObservableObject
    {

        private ObservableCollection<Trabajador> trabajadores;
        public ObservableCollection<Trabajador> Trabajadores
        {
            get => trabajadores;
            set => SetProperty(ref trabajadores, value);
        }

        TrabajadoresRESTService tRestService;

        public TrabajadoresVM()
        {

            tRestService = new TrabajadoresRESTService();
            Trabajadores = tRestService.GetTrabajadores();

        }

    }
}
