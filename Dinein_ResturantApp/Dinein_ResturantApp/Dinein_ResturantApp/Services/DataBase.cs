using Dinein_ResturantApp.Models;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using FirebaseAdmin.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Dinein_UserApp.Services
{
    public class DataBase
    {
        private readonly string FirebaseClientUrl = "https://dine-in2-default-rtdb.firebaseio.com/";
        private readonly string FirebaseSecretKey = "pVOv2WoG1nNrDAZsbmzV8OPS51oPcgdntCXDqjHK";

        private FirebaseClient _firebaseClient;
        private FirebaseAuth _firebaseAuth;
        public DataBase()
        {
            _firebaseClient = new FirebaseClient(FirebaseClientUrl, new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(FirebaseSecretKey)
            });

            _firebaseAuth = FirebaseAuth.DefaultInstance;
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
                        reservation.UserEmail = user.Email;
                        Console.WriteLine("hiiiiiii" + user.UserName);

                    }
                   
                }

                reservationList.Add(reservation);
                
            }

            return reservationList;
          
        }





    }
}
