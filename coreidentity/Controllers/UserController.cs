using System.Threading.Tasks;
using coreidentity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace coreidentity.Controllers{

    [Route("/api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public UserController(UserManager<AppUser> userManager){
            this.userManager = userManager;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<AppUser>> Get(string id){
            var user = await userManager.FindByIdAsync(id);
            if(user != null) return Ok(user);
            return BadRequest("User not found");            
        }
    }
}