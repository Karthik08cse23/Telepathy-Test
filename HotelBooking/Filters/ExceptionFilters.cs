using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace HotelBooking.Filters
{
    public class ExceptionFilters : ExceptionFilterAttribute
    {
        
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is NotImplementedException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
                
            }
            else if (context.Exception is Exception)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
            }
            Logging("Error: " +DateTime.Now.ToString() + context.Exception.Message.ToString());
        }

        private void Logging(string Error)
        {
            File.AppendAllText("D:\\Error.txt",Error + Environment.NewLine);
        }
    }
}