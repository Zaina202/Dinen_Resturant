using Dinein_ResturantApp.Models;
using Dinein_ResturantApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dinein_ResturantApp.ViewModels
{
    class OrdersViewModel : INotifyPropertyChanged
    {
        private DataBase dataBase;

        private List<ReservationModel> _ReservationItems;

        public List<ReservationModel> ReservationItems
        {
            get { return _ReservationItems; }
            set
            {
                _ReservationItems = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ReservationItems)));
            }

        }

        public OrdersViewModel()
        {
            dataBase = new DataBase();
            LoadReservationItems();
        }

        private async void LoadReservationItems()
        {
            ReservationItems = await dataBase.GetAllReservations();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
