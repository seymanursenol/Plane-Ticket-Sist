using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ng_server.Entity
{
    public class Rezervation
    {
        public int Id { get; set; }
        public string RezNumber { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PaymentId { get; set; }
        public string ConversationId { get; set; }
        public List<RezervationItem> RezervationItems { get; set; }
    }
}