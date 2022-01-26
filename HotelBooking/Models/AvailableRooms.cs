using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HotelBooking.HotelEnum;

namespace HotelBooking.Models
{
    public class AvailableRooms
    {
        public string roomNo { get; set; }

        public Status roomStatus { get; set; }

        public int priority { get; set; }
    }
}