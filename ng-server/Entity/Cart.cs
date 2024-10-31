using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ng_server.Entity
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}