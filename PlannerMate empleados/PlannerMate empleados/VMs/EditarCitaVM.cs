using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PlannerMate_empleados.Classes;
using PlannerMate_empleados.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static PlannerMate_empleados.Services.DiaSeleccionadoMessage;

namespace PlannerMate_empleados.VMs
{
    class EditarCitaVM : ObservableObject
    {
        private Day diaSeleccionado;
        public Day DiaSeleccionado
        {
            get
            {
                return diaSeleccionado;
            }
            set
            {
                SetProperty(ref diaSeleccionado, value);
            }
        }

        private Cita citaToEdit;
        public Cita CitaToEdit
        {
            get => citaToEdit;
            set
            {
                SetProperty(ref citaToEdit, value);
            }
        }

        private string visibilidad = "Visible";
        public string Visibilidad
        {
            get => visibilidad;
            set => SetProperty(ref visibilidad, value);
        }

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

        private Trabajador trabajadorSeleccionado;
        public Trabajador TrabajadorSeleccionado
        {
            get => trabajadorSeleccionado;
            set => SetProperty(ref trabajadorSeleccionado, value);
        }
        private ObservableCollection<Trabajador> trabajadoresRest;
        public ObservableCollection<Trabajador> TrabajadoresRest
        {
            get => trabajadoresRest;
            set => SetProperty(ref trabajadoresRest, value);
        }

        public RelayCommand EditarCitaCommand { get; }

        CitasRESTService cRestService = new CitasRESTService();

        TrabajadoresRESTService tRestService = new TrabajadoresRESTService();

        public EditarCitaVM()
        {


            tRestService = new TrabajadoresRESTService();
            TrabajadoresRest = tRestService.GetTrabajadores();

            try
            {
                CitaToEdit = WeakReferenceMessenger.Default.Send<CitaSeleccionadaRequest>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al obtener la cita seleccionada. Detalles del error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Visibilidad = "Collapsed";
                return;
            }

            if (DiaSeleccionado == null)
            {
                try
                {
                    DiaSeleccionado = WeakReferenceMessenger.Default.Send<DiaSeleccionadoRequest>();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al obtener el día seleccionado. Detalles del error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Visibilidad = "Collapsed";
                    return;
                }
            }
            EditarCitaCommand = new RelayCommand(UpdateCita);
        }

        private void ValidarHora()
        {
            TimeZoneInfo tmi = TimeZoneInfo.Local;


            string[] formatos = new string[] { "HH:mm:ss" };

            if (DateTime.TryParseExact(HoraToAdd, formatos, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out DateTime hora))
            {

                DateTime fechaActual = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tmi);
                bool esHorarioVerano = tmi.IsDaylightSavingTime(fechaActual);

                fechaConHora = new DateTime(DiaSeleccionado.Date.Year, DiaSeleccionado.Date.Month, DiaSeleccionado.Date.Day,
                                                     hora.Hour, hora.Minute, hora.Second);
            }
            else
            {
                MessageBox.Show("Formato de hora inválido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void UpdateCita()
        {
            CitaToEdit.fecha = FechaConHora;

            if (CitaToEdit.trabajador == null)
            {
                CitaToEdit.trabajador = TrabajadorSeleccionado;

                Trace.WriteLine($"TRABAJADOR NULL");

                Trace.WriteLine($"Asignado trabajador: {CitaToEdit.trabajador}");
            }

            if (string.IsNullOrEmpty(CitaToEdit.nombre) || CitaToEdit.trabajador == null) //Trabajador a añadir
            {
                MessageBox.Show("El campo nombre o trabajador seleccionado están vacíos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //DiaSeleccionado.AddCita(CitaToAdd);


            cRestService.PutCita(CitaToEdit);
            MessageBox.Show("Cita añadida", "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);

            CitaToEdit = new Cita();

        }
    }
}
