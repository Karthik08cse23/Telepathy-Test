using HotelBooking.Concrete;
using HotelBooking.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace HotelBooking
{
    public class UnityConfiguration
    {
        public IUnityContainer Config()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IAvailableRoomRepository, AvailableRoomRepository>();

            // return the container so it can be used for the dependencyresolver.  
            return container;
        }
    }
}
