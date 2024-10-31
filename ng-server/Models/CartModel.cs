using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ng_server.Models
{
    public class CartModel
    {
        public int CartId { get; set; }
        public string UserId {get;set;}
        public string UserName { get; set; }
        public List<CartItemModel> CartItems { get; set; }
        public List<UserItemsModel> UserItemsModels { get; set; }

        public double TotalPrice()
        {
            return CartItems.Sum(i=>i.Price);
        }
    }

    public class UserItemsModel
    {
        public string UserName { get; set; }
    }

    public class CartItemModel
    {
        public int CartItemId { get; set;}
        public int PlaneId { get; set; }
        public string OutGoing { get; set; }
        public string InComing { get; set; }
        public string Time { get; set; }
        public int TicketTotal { get; set; }
        public double Price { get; set; }
    }
}