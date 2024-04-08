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

        public AddCitaVM()
        {
            DiaSeleccionado = WeakReferenceMessenger.Default.Send<DiaSeleccionadoRequest>();
            AddCitaCommand = new RelayCommand(AddCita);
        }

        private void ValidarHora()
        {
            string[] formatos = new string[] { "HH:mm:ss", "hh:mm:ss" };

            if (DateTime.TryParseExact(HoraToAdd, formatos, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime hora))
            {
                fechaConHora = new DateTime(CitaToAdd.Fecha.Year, CitaToAdd.Fecha.Month, CitaToAdd.Fecha.Day,
                                                     hora.Hour, hora.Minute, hora.Second);

                //MessageBox.Show($"Fecha con hora añadida: {fechaConHora}", "Resultado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Formato de hora inválido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void AddCita()
        {
            CitaToAdd.Fecha = FechaConHora;
            Trabajador trandom = new Trabajador();

            if (CitaToAdd.Trabajador == null)
            { CitaToAdd.Trabajador = trandom; }

            if (string.IsNullOrEmpty(CitaToAdd.Nombre) /*|| CitaToAdd.Trabajador == null*/ ) //Trabajador a añadir
            {
                MessageBox.Show("El campo nombre o trabajador seleccionado están vacíos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DiaSeleccionado.AddCita(CitaToAdd);

            System.Diagnostics.Debug.WriteLine(DiaSeleccionado.InfoCitas);
        }


    }
}
