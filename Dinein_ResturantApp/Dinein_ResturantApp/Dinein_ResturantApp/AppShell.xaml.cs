using Dinein_ResturantApp.ViewModels;
using Dinein_ResturantApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Dinein_ResturantApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
