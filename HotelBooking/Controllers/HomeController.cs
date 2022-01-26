using HotelBooking.Models;
using HotelBooking.Repository;
using HotelBooking.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelBooking.Controllers
{
    public class HomeController : Controller
    {
        AvailableRoomService availableRoomService = null;
        public HomeController(IAvailableRoomRepository availableRoomRepository)
        {
            availableRoomService = new AvailableRoomService(availableRoomRepository);
        }
        public ActionResult Index()
        {
            List<AvailableRooms> availableRooms = new List<AvailableRooms>();
            availableRooms = availableRoomService.GetRooms();
            ViewData["ReadyRooms"] = availableRooms.Where(x => x.roomStatus == HotelEnum.Status.Available).Count();
            return View();
        }

        public ActionResult BookRoom()
        {
            ViewBag.Message = "Book you room here";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}