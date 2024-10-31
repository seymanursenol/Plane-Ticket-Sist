using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace ng_server.Entity
{
    public class RezervationItem
    {
        public int ID { get; set; }
        public int RezId { get; set; }
        [ForeignKey("RezId")]
        public Rezervation Rezervation { get; set; }
        public int PlaneId { get; set; }
        [ForeignKey("PlaneId")]
        public Planes IPlane { get; set; }
        public double Price { get; set; }
        
    }
}