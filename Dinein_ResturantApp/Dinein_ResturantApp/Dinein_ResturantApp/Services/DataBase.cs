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
        private readonly string FirebaseClientUrl = "https://dine-in-54308-default-rtdb.firebaseio.com/";
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
                        
                      //  Console.WriteLine("hiiiiiii" + user.UserName);

                    }

                }

                reservationList.Add(reservation);

            }

            return reservationList;

        }



        public async Task<List<OrderItem>> GetOrderById(string userId)
        {
            try
            {
                var items = new List<OrderItem>();
                var orderQueryResult = await _firebaseClient.Child("Order")
                    .OrderBy("UserId")
                    .EqualTo(userId)
                    .OnceAsync<OrderItem>();
                foreach (var itemSnapshot in orderQueryResult)
                {
                    var item = itemSnapshot.Object;
                    items.Add(new OrderItem
                    {
                        MenuItemName = item.MenuItemName,
                        MenuItemPrice = item.MenuItemPrice,
                        OrderId = item.OrderId,
                        Quantity = item.Quantity,
                        TotalPrice = item.TotalPrice,
                        UserId = item.UserId

                    });
                }

                return items;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting the order by ID: {ex.Message}");
                return null;
            }
        }

    }
}
