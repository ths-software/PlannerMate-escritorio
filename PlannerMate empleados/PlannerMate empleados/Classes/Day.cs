using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        private ObservableCollection<Cita> citas;
        public ObservableCollection<Cita> Citas
        {
            get => citas;
            set => SetProperty(ref citas, value);
        }

        private string infoCitas;
        public string InfoCitas
        {
            get => infoCitas;
            set => SetProperty(ref infoCitas, value);
        }

        public Day(int day, DateTime date)
        {
            Citas = new ObservableCollection<Cita>();
            DayNumber = day;
            Date = date;
            DiaActual = date.Date == DateTime.Today;

        }


        public void AddCita(Cita cita)
        {

            Citas.Add(cita);
            SetInfoCitas();
        }

        public void SetInfoCitas()
        {
            StringBuilder sb = new StringBuilder();

            if(Citas.Count > 4)
            {
                for(int i = 0; i < 4; i++)
                {
                    sb.Append($"{Citas[i].Nombre}: {Citas[i].Descripcion}\n");
                }
                sb.Append("   ...");
            }
            else
            {
                foreach (Cita cita in Citas)
                {
                    sb.Append($"{cita.Nombre}: {cita.Descripcion}\n");
                }
            }

            InfoCitas = sb.ToString();
        }

        public override string ToString()
        {
            //if (Citas.Any())
            //{
            //    return $"{dayNumber} {Citas.First().Descripcion}";
            //}
            //else
            //{
            //    return $"{dayNumber}";
            //}


            return DayNumber == 0 ? "" : $"{DayNumber.ToString()}";
        }
    }
}
