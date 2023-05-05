﻿using Dinein_ResturantApp.Models;
using Dinein_ResturantApp.Services;
using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Dinein_ResturantApp.ViewModels
{
    class DetailViewModel : INotifyPropertyChanged
    {
        private List<Order> _orders;
        private List<OrderItem> _OrderItems;

        private DataBase _dataBase;

        public event PropertyChangedEventHandler PropertyChanged;
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
        private string _OrderTotalPrice;

        public string OrderTotalPrice
        {
            get { return _OrderTotalPrice; }
            set
            {
                if (_OrderTotalPrice != value)
                {
                    _OrderTotalPrice = value;
                    OnPropertyChanged(nameof(OrderTotalPrice));
                }
            }
        }
        //public Command<Order> DeleteOrderCommand { get; set; }

        public DetailViewModel(string userId, string userName)
        {
            _dataBase = new DataBase();
            UserName = userName;

            _ = LoadOrders(userId);


        }
        public DetailViewModel()
        {

        }



        public List<Order> Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }

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
        public List<OrderItem> OrderItems
        {
            get { return _OrderItems; }
            set
            {
                _OrderItems = value;
                OnPropertyChanged(nameof(OrderItems));
            }
        }
        public async Task LoadOrders(string userId)
        {
            Orders = await _dataBase.GetOrderById(userId);
            OrderItems = Orders.Select(el => el.OrderList).First();
            decimal totalPrice = 0;
            foreach (var item in OrderItems)
            {
                totalPrice += item.TotalPrice;
            }


            OrderTotalPrice = totalPrice.ToString("c");

        }
    

        public Command<Order> DeleteOrderCommand
        {
            get
            {
                return new Command<Order>(async (order) =>
                {
                    if (Orders.Contains(order))
                    {
                        await DeleteAndResrsh(order);
                    }
                });
            }
        }

        public async Task DeleteAndResrsh(Order order)
        {
            await _dataBase.DeleteOrderAsync(order);
            Orders.Remove(order);
            await LoadOrders(order.UserId);
        }



        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




    }
}
