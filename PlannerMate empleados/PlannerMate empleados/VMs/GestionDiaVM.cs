using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PlannerMate_empleados.Classes;
using PlannerMate_empleados.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PlannerMate_empleados.Services.DiaSeleccionadoMessage;
using static PlannerMate_empleados.VMs.GestionDiaVM;

namespace PlannerMate_empleados.VMs
{
    class GestionDiaVM : ObservableObject
    {
        private Day diaSeleccionado;
        public Day DiaSeleccionado
        {
            get => diaSeleccionado;
            set => SetProperty(ref diaSeleccionado, value);
        }

        private ObservableCollection<Trabajador> trabajadoresConCitas;
        public ObservableCollection<Trabajador> TrabajadoresConCitas
        {
            get => trabajadoresConCitas;
            set => SetProperty(ref trabajadoresConCitas, value);
        }




        public GestionDiaVM()
        {

            DiaSeleccionado = WeakReferenceMessenger.Default.Send<DiaSeleccionadoRequest>(); // si no hago click en un dia, se rompe 
            // como que no manda un mensaje y esto no recibe nada


            if (DiaSeleccionado != null)
            {
                var citasPorTrabajador = DiaSeleccionado.Citas.GroupBy(cita => cita.trabajador);
                // Seleccionamos solo los trabajadores únicos
                var trabajadoresUnicos = DiaSeleccionado.Citas.Select(cita => cita.trabajador).Distinct();

                // Para cada trabajador único, obtenemos sus citas y creamos instancias de TrabajadorConCitas
                var citasAgrupadas = trabajadoresUnicos.Select(trabajador =>
                {
                    var citasTrabajador = DiaSeleccionado.Citas.Where(cita => cita.trabajador.Equals(trabajador));
                    return new Trabajador
                    {
                        nombre = trabajador.nombre,
                        Citas = new ObservableCollection<Cita>(citasTrabajador)
                    };
                });

                // Creamos la lista de trabajadores con citas
                TrabajadoresConCitas = new ObservableCollection<Trabajador>(citasAgrupadas);


            }
        }
    }
}