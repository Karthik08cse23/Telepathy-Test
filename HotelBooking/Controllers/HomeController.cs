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
            return View(availableRooms);
        }

        public ActionResult BookRoom()
        {
            ViewBag.Message = "Book you room here";

            return View();
        }

        public ActionResult CheckOut()
        {
            ViewBag.Message = "Check Out Rooms";
            List<AvailableRooms> occupiedRooms = new List<AvailableRooms>();
            occupiedRooms = availableRoomService.GetRooms().Where(x => x.roomStatus == HotelEnum.Status.Occupied).ToList();

            return View(occupiedRooms);
        }

        public ActionResult HouseKeeping()
        {
            ViewBag.Message = "House Keeping Wizard";
            List<AvailableRooms> vacantRepairedRooms = new List<AvailableRooms>();
            vacantRepairedRooms = availableRoomService.GetRooms().Where(x => x.roomStatus == HotelEnum.Status.Vaccant || x.roomStatus== HotelEnum.Status.Repair).ToList();

            return View(vacantRepairedRooms);
        }
    }
}