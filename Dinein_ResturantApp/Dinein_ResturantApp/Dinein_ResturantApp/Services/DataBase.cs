using Dinein_ResturantApp.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinein_ResturantApp.Services
{
    public class DataBase
    {
        private readonly string FirebaseClientUrl = "https://dine-in2-default-rtdb.firebaseio.com/";

        private readonly string FirebaseSecretKey = "1AO003FSpm2dGZn4321C88RKPu2T6DPnKLfBr1Dg";

        private FirebaseClient _firebaseClient;

        public DataBase()
        {
            _firebaseClient = new FirebaseClient(FirebaseClientUrl, new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(FirebaseSecretKey)
            });


        }

        public async Task<List<ReservationModel>> GetAllReservations()
        {

            var reservationList = new List<ReservationModel>();
            var reservationModels = await _firebaseClient.Child(nameof(ReservationModel)).OnceAsync<ReservationModel>();

            foreach (var reservationModel in reservationModels)
            {
                var reservation = reservationModel.Object;

                if (reservation != null && !string.IsNullOrEmpty(reservation.UserId))
                {
                    var userQueryResult = await _firebaseClient.Child("Users")
                    .OrderBy("Id")
                    .EqualTo(reservation.UserId)
                    .OnceAsync<Users>();

                    if (userQueryResult.Any())
                    {
                        var user = userQueryResult.First().Object;
                        reservation.UserName = user.UserName;
                    }
                }
                reservationList.Add(reservation);
            }
            return reservationList;
        }
        public async Task<List<Order>> GetOrderById(string userId)
        {
            try
            {
                var orderQueryResult = await _firebaseClient.Child("BillOrder")
                    .OnceAsync<Order>();

                return orderQueryResult.Where(el => el.Object.UserId == userId).Select(el => el.Object).ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting the order by ID: {ex.Message}");
                return null;
            }
        }

        public async Task DeleteOrderAsync(string userId)
        {
            bool response = await App.Current.MainPage.DisplayAlert("Alert", "Do you want to delete this order?", "Yes", "No");

            if (response)
            {
                var toDeleteOrder = await _firebaseClient
                          .Child("BillOrder")
                          .OnceAsync<Order>();

                foreach (var x in toDeleteOrder)
                {
                    if (x.Object.UserId == userId)
                    {
                        await _firebaseClient
                            .Child("BillOrder")
                            .Child(x.Key)
                           .DeleteAsync();
                    }
                }
                await App.Current.MainPage.DisplayAlert("Success", "Deletion Succeeded", "Ok");

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Failed", "Delete Failed", "Ok");
            }
        }
        public async Task DeleteReservationAsync(string userId)
        {
            var toDeleteRes = await _firebaseClient
                      .Child("ReservationModel")
                      .OnceAsync<Order>();

            foreach (var x in toDeleteRes)
            {
                if (x.Object.UserId == userId)
                {
                    await _firebaseClient
                        .Child("ReservationModel")
                        .Child(x.Key)
                       .DeleteAsync();
                }
            }
        }



    }
}