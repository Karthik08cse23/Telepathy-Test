using HotelBooking.Filters;
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
    [ExceptionFilters]
    public class HotelRoomsController : ApiController
    {
        IAvailableRoomRepository _availableRoomRepository;
        public HotelRoomsController(IAvailableRoomRepository availableRoomRepository)
        {

            _availableRoomRepository = availableRoomRepository;
        }

        [HttpGet]
        [Route("GetRoomsCount")]
        public IHttpActionResult GetRoomsCount()
        {
            List<AvailableRooms> availableRooms = new List<AvailableRooms>();
            availableRooms = _availableRoomRepository.GetAvailableRooms().Where(x => x.roomStatus == HotelEnum.Status.Available).ToList();
            var count = availableRooms.Count();
            return Ok(count);
        }

        [HttpGet]
        [Route("GetRooms")]
        public IHttpActionResult GetRooms()
        {
            List<AvailableRooms> availableRooms = new List<AvailableRooms>();
            availableRooms = _availableRoomRepository.GetAvailableRooms().Where(x => x.roomStatus == HotelEnum.Status.Available).ToList();
            var AvailableCount = availableRooms.Count();
            return Ok(availableRooms);
        }

        [HttpPut]
        [Route("BookRoom/{id}")]
        public IHttpActionResult BookRoom(int id)
        {
            List<string> bookedRooms = new List<string>();
            bookedRooms = _availableRoomRepository.BookRooms(id);
            return Ok( bookedRooms );
        }

        [HttpPut]
        [Route("CheckOut/{id}")]
        public IHttpActionResult CheckOut(string id)
        {
            _availableRoomRepository.CheckOut(id);
            return Ok("Check Out Successful" );
        }

        [HttpPost]
        [Route("HouseKeeping")]
        public IHttpActionResult HouseKeeping(HouseKeeping houseKeeping)
        {
            _availableRoomRepository.HouseKeeping(houseKeeping);
            return Ok("HouseKeeping Successful" );
        }
    }

}