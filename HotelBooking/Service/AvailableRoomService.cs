using HotelBooking.Models;
using HotelBooking.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Service
{
    public class AvailableRoomService
    {
        IAvailableRoomRepository _availableRoomRepository = null;

        public AvailableRoomService(IAvailableRoomRepository availableRoomRepository)
        {
            _availableRoomRepository = availableRoomRepository;
        }

        public List<AvailableRooms> GetRooms()
        {
            List<AvailableRooms> rooms = new List<AvailableRooms>();
            rooms = _availableRoomRepository.GetAvailableRooms();

            return rooms;
        }

    }
}