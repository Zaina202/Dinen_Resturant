using Dinein_ResturantApp.Models;
using Dinein_ResturantApp.ViewModels;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Dinein_ResturantApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        public DetailPage(string userName, ReservationModel selectedItem)
        {
            InitializeComponent();
            BindingContext = new DetailViewModel(selectedItem.UserId,userName);
            
        }
    }
}