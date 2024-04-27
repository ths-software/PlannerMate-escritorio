using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using PlannerMate_empleados.Classes;
using System.Reflection;
using PlannerMate_empleados.Services;
using CommunityToolkit.Mvvm.Messaging;
using static PlannerMate_empleados.Services.DiaSeleccionadoMessage;

namespace PlannerMate_empleados.VMs
{
    class CalendarioVM : ObservableObject
    {

        public ObservableCollection<Day> days;
        public ObservableCollection<Day> Days
        {
            get { return days; }
            set => SetProperty(ref days, value);
        }

        private Day diaSeleccionado;
        public Day DiaSeleccionado
        {
            get => diaSeleccionado;
            set => SetProperty(ref diaSeleccionado, value);
        }

        public RelayCommand<Day> SelectDayCommand { get; }

        public RelayCommand AdelantarMesCommand { get; }
        public RelayCommand AtrasarMesCommand { get; }

        private DateTime displayedDate;
        public DateTime DisplayedDate
        {
            get {

                TimeZoneInfo timeZone = TimeZoneInfo.Local;
                TimeSpan offset = timeZone.GetUtcOffset(DateTime.Now);

                return displayedDate;
            
            }
            set
            {
                SetProperty(ref displayedDate, value);
                OnPropertyChanged(nameof(DisplayedMonthYear)); // para que cambie el DisplayedMonthYear cuando cambie de mes
                SetCalendario();
            }
        }

        public string DisplayedMonthYear => DisplayedDate.ToString("MMMM yyyy"); // fecha mostrada encima del calendario


        CitasRESTService rest;
        ObservableCollection<Cita> citasRest;

        public CalendarioVM()
        {
            rest = new CitasRESTService();
            citasRest = rest.GetCitas();

            SelectDayCommand = new RelayCommand<Day>(SelectDay);
            Days = new ObservableCollection<Day>();

            DisplayedDate = DateTime.Today; // cambiamos el DisplayerDate por ello se ejecuta iniciarCalendario

            AdelantarMesCommand = new RelayCommand(AdelantarMes);
            AtrasarMesCommand = new RelayCommand(AtrasarMes);

        }

        public void AtrasarMes()
        {
            DisplayedDate = DisplayedDate.AddMonths(-1);
        }

        public void AdelantarMes()
        {
            DisplayedDate = DisplayedDate.AddMonths(1);
        }

        private void SelectDay(Day day)
        {
            foreach (var d in Days)
            {
                d.IsSelected = d == day;
            }

            DiaSeleccionado = day;

            WeakReferenceMessenger.Default.Reset();
            WeakReferenceMessenger.Default.Register<CalendarioVM, DiaSeleccionadoRequest>(this, (r, m) =>
            {
                m.Reply(r.DiaSeleccionado);
            });

        }

        private void SetCalendario()
        {
            Days.Clear();

            DateTime primerDiaDelMes = new DateTime(DisplayedDate.Year, DisplayedDate.Month, 1);
            DayOfWeek diaDeInicio = primerDiaDelMes.DayOfWeek;
            int diasEnMes = DateTime.DaysInMonth(DisplayedDate.Year, DisplayedDate.Month);

            // Determinar el índice de la columna de inicio según el día de inicio de la semana
            int columnaInicio = ((int)diaDeInicio + 6) % 7; // Ajuste para considerar que el lunes es el primer día (índice 0)

            // Llenar las columnas anteriores al día de inicio con días vacíos
            for (int i = 0; i < columnaInicio; i++)
            {
                Days.Add(new Day(0, DateTime.MinValue)); // Día vacío
            }

            // Agregar días del mes actual al calendario
            for (int i = 1; i <= diasEnMes; i++)
            {
                Day nuevoDia = new Day(i, new DateTime(DisplayedDate.Year, DisplayedDate.Month, i));

                DiaSeleccionado = nuevoDia.DiaActual ? nuevoDia : null;

                foreach (Cita cita in citasRest)
                {
                    if (cita.fecha.Date == nuevoDia.Date.Date)
                    {
                        nuevoDia.AddCita(cita);
                    }
                }
                Days.Add(nuevoDia);
            }
        }

    }
}
