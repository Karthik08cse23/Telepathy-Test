using HotelBooking.Models;
using HotelBooking.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelBooking.Controllers
{
    [RoutePrefix("api/HotelRooms")]
    public class HotelRoomsController : ApiController
    {
        IAvailableRoomRepository _availableRoomRepository;
        public HotelRoomsController(IAvailableRoomRepository availableRoomRepository)
        {

            _availableRoomRepository = availableRoomRepository;
        }
        [HttpGet]
        [Route("GetRooms")]
        public IHttpActionResult GetRooms()
        {
            List<AvailableRooms> availableRooms = new List<AvailableRooms>();
            availableRooms = _availableRoomRepository.GetAvailableRooms().Where(x => x.roomStatus == HotelEnum.Status.Available).ToList();
            var AvailableCount = availableRooms.Count();
            return Json(new { count = AvailableCount, availRooms = availableRooms });
        }

        [HttpPut]
        [Route("BookRoom/{id}")]
        public IHttpActionResult BookRoom(int id)
        {
            List<string> roomsBooked = new List<string>();
            roomsBooked = _availableRoomRepository.BookRooms(id);
            return Json(new { bookedRooms = roomsBooked });
        }

        [HttpPut]
        [Route("CheckOut/{id}")]
        public IHttpActionResult CheckOut(string id)
        {
            _availableRoomRepository.CheckOut(id);
            return Json(new { result = "Check Out Successful" });
        }

        [HttpPost]
        [Route("HouseKeeping")]
        public IHttpActionResult HouseKeeping(HouseKeeping houseKeeping)
        {
            _availableRoomRepository.HouseKeeping(houseKeeping);
            return Json(new { result = "HouseKeeping Successful" });
        }
    }

}