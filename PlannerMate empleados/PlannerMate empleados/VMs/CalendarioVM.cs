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

        public RelayCommand<Day> SelectDayCommand { get; }
        public RelayCommand AdelantarMesCommand{get;}
        public RelayCommand AtrasarMesCommand{get;}



        private DateTime displayedDate;
        public DateTime DisplayedDate
        {
            get { return displayedDate; }
            set
            {
                SetProperty(ref displayedDate, value);
                OnPropertyChanged(nameof(DisplayedMonthYear));
                UpdateCalendarDays();
            }
        }

        public string DisplayedMonthYear => DisplayedDate.ToString("MMMM yyyy");



        public CalendarioVM()
        {
            SelectDayCommand = new RelayCommand<Day>(SelectDay);
            Days = new ObservableCollection<Day>();
            IniciarCalendario();

            AdelantarMesCommand = new RelayCommand(AdelantarMes);
            AtrasarMesCommand = new RelayCommand(AtrasarMes);
            DisplayedDate = DateTime.Today;
        }

        public void AtrasarMes()
        {
            DisplayedDate = DisplayedDate.AddMonths(-1);
        }

        public void AdelantarMes()
        {
            DisplayedDate = DisplayedDate.AddMonths(1);
        }



        private void MostrarMensaje(Day day)
        {
            // Mostrar un MessageBox con la información del día seleccionado
            MessageBox.Show($"Ha seleccionado el día {day.DayNumber} - {day.Date}");
        }

        private void SelectDay(Day day)
        {
            // Lógica de selección del día aquí (puedes agregar la lógica según tus necesidades)
            // Por ejemplo, puedes cambiar el color de fondo o realizar alguna acción específica
            // Lógica para cambiar el estado de IsSelected cuando se selecciona un día
            foreach (var d in Days)
            {
                d.IsSelected = d == day;
            }

            MostrarMensaje(day);
        }

        private void IniciarCalendario()
        {
            DateTime fechaActual = DateTime.Today;
            DateTime primerDiaDelMes = new DateTime(fechaActual.Year, fechaActual.Month, 1);
            DayOfWeek diaDeInicio = primerDiaDelMes.DayOfWeek;
            int diasEnMes = DateTime.DaysInMonth(fechaActual.Year, fechaActual.Month);

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
                Days.Add(new Day(i, new DateTime(fechaActual.Year, fechaActual.Month, i)));
            }
        }


        private void UpdateCalendarDays()
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
                Days.Add(new Day(i, new DateTime(DisplayedDate.Year, DisplayedDate.Month, i)));
            }
        }

    }
}
