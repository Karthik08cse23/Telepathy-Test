using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking
{
    public static class HotelEnum
    {
        public enum Status
        {
            Available=1,
            Occupied=2,
            Vaccant=3,
            Repair=4
        }
    }
}