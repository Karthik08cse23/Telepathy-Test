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
    [RoutePrefix("api/HotelRooms/")]
    public class HotelRoomsController : ApiController
    {
        IAvailableRoomRepository _availableRoomRepository;
        public HotelRoomsController(IAvailableRoomRepository availableRoomRepository)
        {

            _availableRoomRepository = availableRoomRepository;
        }
        [HttpGet]
        public IHttpActionResult Get()
        {
            List<AvailableRooms> availableRooms = new List<AvailableRooms>();
            availableRooms = _availableRoomRepository.GetAvailableRooms();
            return Json(availableRooms);
        }





        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }

}