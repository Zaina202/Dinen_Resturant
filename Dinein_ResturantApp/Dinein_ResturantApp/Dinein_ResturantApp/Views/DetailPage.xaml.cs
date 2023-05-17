using Dinein_ResturantApp.Models;
using Dinein_ResturantApp.Services;
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
        private readonly ReservationModel _selectedItem;

        public DetailPage(string userName, ReservationModel selectedItem)
        {
            InitializeComponent();
            BindingContext = new DetailViewModel(selectedItem.UserId, userName);
            _selectedItem = selectedItem;

        }

        private async void Delete_Order(object sender, EventArgs e)
        {
            var viewModel = BindingContext as DetailViewModel;

            _ = viewModel.DeleteAndResrsh(_selectedItem.UserId);
            await Navigation.PopAsync();

        }
    }
}