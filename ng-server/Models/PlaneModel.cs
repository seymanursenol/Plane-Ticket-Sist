using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ng_server.Models
{
    public class PlaneModel
    {
        [Required]
        public string Outgoing { get; set; }
        
        [Required]
        public string Incoming { get; set; }
        
        [Required]
        public string Time { get; set; }
        
        [Required]
        [Range(0, int.MaxValue)]
        public int TicketTotal { get; set; }
        
        [Required]
        [Range(0.0, double.MaxValue)]
        public double Price { get; set; }
    }

    
}


 