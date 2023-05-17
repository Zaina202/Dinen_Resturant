
using Dinein_ResturantApp.Models;
using Dinein_ResturantApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

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
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }
        public ICommand RefreshCommand { get; }

        public OrdersViewModel()
        {
            dataBase = new DataBase();
            LoadReservationItems();
            RefreshCommand = new Command(Refresh);

        }

        public void Refresh()
        {
            LoadReservationItems();
            IsRefreshing = false;
        }

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }


        public OrdersViewModel(string userName)
        {
            UserName = userName;
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