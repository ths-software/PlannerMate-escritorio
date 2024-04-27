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
        CitasRESTService cRestService = new CitasRESTService();

        TrabajadoresRESTService tRestService = new TrabajadoresRESTService();

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
            set=> SetProperty(ref trabajadoresRest, value);
        }

        private string visibilidad = "Visible";
        public string Visibilidad
        {
            get => visibilidad;
            set => SetProperty(ref visibilidad, value);
        }

        public AddCitaVM()
        {
            tRestService = new TrabajadoresRESTService();
            TrabajadoresRest = tRestService.GetTrabajadores();

            if(DiaSeleccionado==null)
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

            AddCitaCommand = new RelayCommand(AddCita);

        }

        private void ValidarHora()
        {
            TimeZoneInfo tmi = TimeZoneInfo.Local;


            string[] formatos = new string[] { "HH:mm:ss" };

            if (DateTime.TryParseExact(HoraToAdd, formatos, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out DateTime hora))
            {

                DateTime fechaActual = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tmi);
                bool esHorarioVerano = tmi.IsDaylightSavingTime(fechaActual);
                int ajusteHora = esHorarioVerano ? 2 : 1;   // esta parte de horarios no hace falta, habia un error con los campos y me la ponía mal

                fechaConHora = new DateTime(DiaSeleccionado.Date.Year, DiaSeleccionado.Date.Month, DiaSeleccionado.Date.Day,
                                                     hora.Hour, hora.Minute, hora.Second);
            }
            else
            {
                MessageBox.Show("Formato de hora inválido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void AddCita()
        {
            CitaToAdd.idCita = 0;
            CitaToAdd.fecha = FechaConHora;

            if (CitaToAdd.trabajador == null)
            { 
                CitaToAdd.trabajador = TrabajadorSeleccionado;

                Trace.WriteLine($"TRABAJADOR NULL");

                Trace.WriteLine($"Asignado trabajador: {CitaToAdd.trabajador}");
            }

            if (string.IsNullOrEmpty(CitaToAdd.nombre) || CitaToAdd.trabajador == null) //Trabajador a añadir
            {
                MessageBox.Show("El campo nombre o trabajador seleccionado están vacíos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //DiaSeleccionado.AddCita(CitaToAdd);

            
            cRestService.PostCita(CitaToAdd);
            MessageBox.Show("Cita añadida", "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);

            CitaToAdd = new Cita();
            
        }


    }
}
