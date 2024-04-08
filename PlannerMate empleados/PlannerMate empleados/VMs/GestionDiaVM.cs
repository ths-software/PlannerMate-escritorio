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

        private ObservableCollection<TrabajadorConCitas> trabajadoresConCitas;
        public ObservableCollection<TrabajadorConCitas> TrabajadoresConCitas
        {
            get => trabajadoresConCitas;
            set => SetProperty(ref trabajadoresConCitas, value);
        }


        public RelayCommand AbrirAñadirCita { get; }

        public GestionDiaVM()
        {

            DiaSeleccionado = WeakReferenceMessenger.Default.Send<DiaSeleccionadoRequest>(); // si no hago click en un dia, se rompe 
            // como que no manda un mensaje y esto no recibe nada


            if (DiaSeleccionado != null)
            {
                var citasPorTrabajador = DiaSeleccionado.Citas.GroupBy(cita => cita.Trabajador);
                // Seleccionamos solo los trabajadores únicos
                var trabajadoresUnicos = diaSeleccionado.Citas.Select(cita => cita.Trabajador).Distinct();

                // Para cada trabajador único, obtenemos sus citas y creamos instancias de TrabajadorConCitas
                var citasAgrupadas = trabajadoresUnicos.Select(trabajador =>
                {
                    var citasTrabajador = diaSeleccionado.Citas.Where(cita => cita.Trabajador.Equals(trabajador));
                    return new TrabajadorConCitas
                    {
                        Trabajador = trabajador,
                        Citas = new ObservableCollection<Cita>(citasTrabajador)
                    };
                });

                // Creamos la lista de trabajadores con citas
                TrabajadoresConCitas = new ObservableCollection<TrabajadorConCitas>(citasAgrupadas);


            }

            AbrirAñadirCita = new RelayCommand(CargarAddCita);
            System.Diagnostics.Debug.WriteLine("LoqueHay:\n" + DiaSeleccionado.InfoCitas);
        }

        public void CargarAddCita()
        {
            NavigationServices.LoadAddCita();
        }


    }




    public class TrabajadorConCitas
    {
        public Trabajador Trabajador { get; set; }

        public string CitasInfo
        {
            get
            {
                return GetCitasTrabajador();
            }
        }
        public ObservableCollection<Cita> Citas { get; set; }


        public string GetCitasTrabajador()
        {
            StringBuilder sb = new StringBuilder();

            foreach (Cita cita in Citas)
            {
                sb.Append($"Cliente: {cita.Nombre} Descripción: {cita.Descripcion}.\n");
            }
            sb.Append("");

            return sb.ToString();
        }

        public override string ToString()
        {
            return $"{Trabajador}";
        }
    }
}