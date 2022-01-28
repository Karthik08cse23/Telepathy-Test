using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelBooking;
using HotelBooking.Controllers;
using HotelBooking.Repository;
using HotelBooking.Service;
using HotelBooking.Concrete;
using Moq;
using HotelBooking.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Web.Http.Results;
using System.Web.Http;

namespace HotelBooking.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        IAvailableRoomRepository _availableRoomRepository;
        public HomeControllerTest()
        {
           
        }

        [TestInitialize]
        public void Setup()
        {
            List<AvailableRooms> availableRooms = new List<AvailableRooms>(){
                new AvailableRooms()
                {
                    roomNo = "1A",
                    roomStatus = HotelEnum.Status.Available
                },
                new AvailableRooms()
                {
                    roomNo = "1B",
                    roomStatus = HotelEnum.Status.Available
                },
                new AvailableRooms()
                {
                    roomNo = "1C",
                    roomStatus = HotelEnum.Status.Occupied
                },
                
            };
            
            var _availableRoomMoq = new Mock<IAvailableRoomRepository>();
            List<string> bookedRooms = new List<string>();
            bookedRooms.Add(availableRooms.First().roomNo);
            _availableRoomMoq.Setup(x => x.GetAvailableRooms()).Returns(availableRooms.Where(x=>x.roomStatus==HotelEnum.Status.Available).ToList());
            _availableRoomMoq.Setup(x => x.BookRooms(1)).Returns(bookedRooms);
            _availableRoomRepository = _availableRoomMoq.Object;
        }
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController(_availableRoomRepository);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CheckOut()
        {
            HomeController controller = new HomeController(_availableRoomRepository);

            ViewResult result = controller.CheckOut() as ViewResult;

            Assert.AreEqual("Check Out Rooms", result.ViewBag.Message);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void HouseKeeping()
        {
            // Arrange
            HomeController controller = new HomeController(_availableRoomRepository);

            ViewResult result = controller.HouseKeeping() as ViewResult;
         
            Assert.IsNotNull(result);
            Assert.AreEqual("House Keeping Wizard", result.ViewBag.Message);
        }

        [TestMethod]
        public void GetRoomCount()
        {
            // Arrange
            HotelRoomsController controller = new HotelRoomsController(_availableRoomRepository);

            IHttpActionResult result = controller.GetRooms() ;
            var contentResult = result as OkNegotiatedContentResult<List<AvailableRooms>>;
            Assert.AreEqual(contentResult.Content.Count, 2);
            Assert.AreEqual(contentResult.Content.First().roomNo, "1A");
            Assert.AreEqual(contentResult.Content.Last().roomNo, "1B");
            Assert.IsNotNull(contentResult);
        }

        [TestMethod]
        public void BookRoom()
        {
            // Arrange
            HotelRoomsController controller = new HotelRoomsController(_availableRoomRepository);

            IHttpActionResult bookedRooms = controller.BookRoom(1);
            var contentResult = bookedRooms as OkNegotiatedContentResult<List<string>>;
            Assert.AreEqual(contentResult.Content.First().ToString(), "1A");
            Assert.IsNotNull(contentResult);
        }

        [TestMethod]
        public void CheckOutRoom()
        {
            HotelRoomsController controller = new HotelRoomsController(_availableRoomRepository);

            IHttpActionResult checkOutRooms = controller.CheckOut("1C");
            var contentResult = checkOutRooms as OkNegotiatedContentResult<string>;
            Assert.AreEqual(contentResult.Content.ToString(), "Check Out Successful");
            Assert.IsNotNull(contentResult);
        }

        [TestMethod]
        public void HouseKeepingRoom()
        {
            HotelRoomsController controller = new HotelRoomsController(_availableRoomRepository);
            HouseKeeping houseKeeping = new HouseKeeping()
            {
                roomNo = "1D",
                Status = HotelEnum.Status.Available
            };
            IHttpActionResult houseKeepingRooms = controller.HouseKeeping(houseKeeping);
            var contentResult = houseKeepingRooms as OkNegotiatedContentResult<string>;
            Assert.AreEqual(contentResult.Content.ToString(), "HouseKeeping Successful");
            Assert.IsNotNull(contentResult);
        }


    }
}
