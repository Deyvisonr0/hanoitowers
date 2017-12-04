using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Flunt.Notifications;

namespace HanoiTower.Rest.Controllers
{
    public class BaseController : ApiController
    {
        protected async Task<HttpResponseMessage> Response(object result, IEnumerable<Notification> notifications)
        {
            var enumerable = notifications as IList<Notification> ?? notifications.ToList();
            if (enumerable.Any())
                return Request.CreateResponse(HttpStatusCode.BadRequest, new
                {
                    success = false,
                    errors = enumerable
                });
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                success = true,
                data = result
            });
        }

    }
}
