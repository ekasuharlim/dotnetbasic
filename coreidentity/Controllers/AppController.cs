using Microsoft.AspNetCore.Mvc;

namespace coreidentity.Controllers{
    public class AppController : Controller{

        public ActionResult<string> Index(){
            return Ok("Hello there");
        }

    }

}