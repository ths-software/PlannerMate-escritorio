using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using PlannerMate_empleados.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PlannerMate_empleados.Services.DiaSeleccionadoMessage;

namespace PlannerMate_empleados.VMs
{
    class AddCitaVM : ObservableObject
    {
        private Day diaSeleccionado;
        public Day DiaSeleccionado
        {
            get => diaSeleccionado;
            set => SetProperty(ref diaSeleccionado, value);
        }

        public AddCitaVM()
        {
            DiaSeleccionado = WeakReferenceMessenger.Default.Send<DiaSeleccionadoRequest>();
        }
    }
}
