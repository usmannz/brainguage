using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FRCSPreparationPortal.API.Controllers
{
    public class BaseController : Controller
    {
        public int UserId => this.User.GetUserId();
    }
}