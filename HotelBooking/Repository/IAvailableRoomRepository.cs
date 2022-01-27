using HotelBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Repository
{
    public interface IAvailableRoomRepository
    {
        List<AvailableRooms> GetAvailableRooms();

        List<string> BookRooms(int count);

        void CheckOut(string RoomNo);

        void HouseKeeping(HouseKeeping houseKeeping);
    }
}
