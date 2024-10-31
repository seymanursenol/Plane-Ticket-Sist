using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ng_server.Entity
{
    public class CartItem
    {
        public int Id { get; set; }
    
        public int PlaneId { get; set; }
        [ForeignKey("PlaneId")]
        public Planes IPlane { get; set; }
        
        public int CartId { get; set; }
        [ForeignKey("CartId")]
        public Cart Cart { get; set; }
    }
}