using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dinein_ResturantApp.Models
{
    public class Order
    {
       
        public string UserId { get; set; }
        public List<OrderItem> OrderList { get; set;}
        public string BillOrderID { get; set; }
        public string OrderTotalPrice{ get; set; }

    }
}
