using Dinein_ResturantApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dinein_ResturantApp.ViewModels
{
    class DetailViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly DataBase _dataBase;


        public DetailViewModel()
        {
            _dataBase = new DataBase();
        }


    }
}
