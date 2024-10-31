using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ng_server.Models
{
    public class RezervationModel
    {
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public string Cvc { get; set; }
        public CartModel CartModel { get; set; }
    }
}


