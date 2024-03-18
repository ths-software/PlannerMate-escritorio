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
            set=> SetProperty(ref diaSeleccionado, value);
        }

        public RelayCommand<Day> SelectDayCommand { get; }

        public RelayCommand AdelantarMesCommand { get; }
        public RelayCommand AtrasarMesCommand { get; }


        public RelayCommand AbrirGestionDia { get; }
        /*
         * Gestión día necesitará diaSeleccionado
         * Como en gestionDia solo se accede cuando seleccionas un día, tendrás un día seleccionado obligatoriamente.
         * Quizás haya que hacer que el dia se seleccione con un borde en el dia clickado y luego
         * tengas que hacer click en el menú lateral en la opción, gestionar día
         */



        private DateTime displayedDate;
        public DateTime DisplayedDate
        {
            get { return displayedDate; }
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

            //IniciarCalendario();
            //SetCalendario();
            DisplayedDate = DateTime.Today; // cambiamos el DisplayerDate por ello se ejecuta iniciarCalendario

            AdelantarMesCommand = new RelayCommand(AdelantarMes);
            AtrasarMesCommand = new RelayCommand(AtrasarMes);

        }


        // quizás sea buena idea que el usuario cambie de mes de otra forma, con un dropdown por ejemplo
        public void AtrasarMes()
        {
            DisplayedDate = DisplayedDate.AddMonths(-1);
        }

        public void AdelantarMes()
        {
            DisplayedDate = DisplayedDate.AddMonths(1);
        }


        private void SeleccionarDia(Day day)
        {
            // Mostrar un MessageBox con la información del día seleccionado
            //MessageBox.Show($"Ha seleccionado el día {day.DayNumber} - {day.Date}");

            DiaSeleccionado = day;

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

            SeleccionarDia(day);
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
                    if (cita.Fecha.Date == nuevoDia.Date.Date)
                    {
                        nuevoDia.AddCita(cita);
                    }
                }
                Days.Add(nuevoDia);
            }
        }

    }
}
