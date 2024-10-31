using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ng_server.Models
{
    public class AddRezDto
    {
        [Required]
        public int PlaneID { get; set; } 
    }
}


 