using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using OdataData.Model;
using OdataService.Repository;
using OdataService.Service;

namespace OdataAPI.Controllers
{
    [ApiController]
    [Route(Helpers.SettingVersionAPI.ApiVersion)]
    public class PetShopController : ControllerBase
    {
        private readonly IPetManagerService _petManagerService;

        public PetShopController (IPetManagerService petManagerService)
        {
            _petManagerService = petManagerService;
        }
    
        /// <summary>
        /// Login
        /// </summary>
        /// 
        [HttpPost("Login")]
        public IActionResult Login([FromQuery] string email, [FromQuery] string password)
        {
            try
            {
                var result = _petManagerService.Login(email, password);

                if (result != null)
                {
                    return Ok(result);
                }

                return BadRequest("Login failed");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}