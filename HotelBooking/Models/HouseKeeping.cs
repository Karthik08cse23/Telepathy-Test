using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HotelBooking.HotelEnum;

namespace HotelBooking.Models
{
    public class HouseKeeping
    {
        public string roomNo { get; set; }

        public Status Status { get; set; }
    }
}