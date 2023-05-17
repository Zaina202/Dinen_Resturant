using Dinein_ResturantApp.Models;
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
    public partial class OrdersPage : ContentPage
    {
        public OrdersPage()
        {
            InitializeComponent();
        }
        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedItem = e.Item as Dinein_ResturantApp.Models.ReservationModel;


            if (e.Item is ReservationModel reservation)
            {
                await Navigation.PushAsync(new DetailPage(reservation.UserName, selectedItem));
            }


         ((ListView)sender).SelectedItem = null;

        }
    }
}