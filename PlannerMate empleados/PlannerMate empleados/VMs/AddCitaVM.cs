using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PlannerMate_empleados.Classes;
using PlannerMate_empleados.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        private Cita citaToAdd = new Cita();
        public Cita CitaToAdd
        {
            get => citaToAdd;
            set => SetProperty(ref citaToAdd, value);
        }

        public RelayCommand AddCitaCommand { get; }

        private string horaToAdd;
        public string HoraToAdd
        {
            get => horaToAdd;
            set { SetProperty(ref horaToAdd, value); ValidarHora(); }
        }

        private DateTime fechaConHora;
        public DateTime FechaConHora
        {
            get => fechaConHora;
            set => SetProperty(ref fechaConHora, value);
        }
        CitasRESTService RestService = new CitasRESTService();

        public AddCitaVM()
        {
            DiaSeleccionado = WeakReferenceMessenger.Default.Send<DiaSeleccionadoRequest>();
            AddCitaCommand = new RelayCommand(AddCita);
        }

        private void ValidarHora()
        {
            string[] formatos = new string[] { "HH:mm:ss" };

            if (DateTime.TryParseExact(HoraToAdd, formatos, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime hora))
            {
                fechaConHora = new DateTime(DiaSeleccionado.Date.Year, DiaSeleccionado.Date.Month, DiaSeleccionado.Date.Day,
                                                     hora.Hour, hora.Minute, hora.Second);
                

                MessageBox.Show($"Fecha con hora añadida: {fechaConHora}", "Resultado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Formato de hora inválido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void AddCita()
        {
            CitaToAdd.IdCita = 0;
            CitaToAdd.fecha = FechaConHora;

            Trabajador trabajador = new Trabajador();
            trabajador.idTrabajador = 2;

            if (CitaToAdd.trabajador == null)
            { 
                CitaToAdd.trabajador = trabajador;

                Trace.WriteLine($"TRABAJADOR NULL");

                Trace.WriteLine($"Asignado trabajador: {CitaToAdd.trabajador}");
            }

            if (string.IsNullOrEmpty(CitaToAdd.nombre) /*|| CitaToAdd.Trabajador == null*/ ) //Trabajador a añadir
            {
                MessageBox.Show("El campo nombre o trabajador seleccionado están vacíos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //DiaSeleccionado.AddCita(CitaToAdd);

            
            RestService.PostCita(CitaToAdd);
            Trace.WriteLine($"cita supuestamente añadida: {CitaToAdd.IdCita} {CitaToAdd.nombre} {CitaToAdd.descripcion}");
            
        }


    }
}
