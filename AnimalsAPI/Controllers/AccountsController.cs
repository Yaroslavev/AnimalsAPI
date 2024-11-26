using Core.IServices;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimalsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController(IAccountsService accountsService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> register(RegisterModel model)
        {
            await accountsService.Register(model);

            return Ok();
        }
    }
}
