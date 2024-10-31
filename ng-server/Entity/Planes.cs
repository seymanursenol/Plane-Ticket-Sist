using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iyzipay.Model;
using Microsoft.AspNetCore.Mvc;

namespace ng_server.Entity
{
    public class Planes
    {
        public int Id { get; set; }
        public string Outgoing { get; set; }
        public string Incoming { get; set; }
        public DateTime Time { get; set; }
        public int TicketTotal { get; set; }
        public double Price { get; set; }
        public EnumPlaneState PlaneState { get; set; }
    }

    public enum EnumPlaneState
    {
        waiting=0,
        cancel=1,
    }
}