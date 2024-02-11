using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerMate_empleados.Classes
{
    public class Day : ObservableObject
    {
        private int dayNumber;
        public int DayNumber
        {

            get => dayNumber;

            set => SetProperty(ref dayNumber, value);
        }
        private DateTime date;
        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        private bool diaActual;
        public bool DiaActual
        {
            get => diaActual;
            set => SetProperty(ref diaActual, value);
        }

        private bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set => SetProperty(ref isSelected, value);
        }

        public Day(int day, DateTime date)
        {
            DayNumber = day;
            Date = date;
            DiaActual = date.Date == DateTime.Today;

        }


        public override string ToString()
        {
            return dayNumber.ToString();
        }
    }
}
