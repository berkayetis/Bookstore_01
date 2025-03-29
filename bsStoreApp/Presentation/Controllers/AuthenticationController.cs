using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        IServiceManager _serviceManager;

        public AuthenticationController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))] // UserForRegistirationDto bos mu degil mi kontrolu yapiyor
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistirationDto userForRegistirationDto)
        {
            var result = await _serviceManager.AuthenticationService.RegisterUser(userForRegistirationDto);

            if(result.Succeeded)
            {
                return StatusCode(201);
            }
            else
            {
                foreach(var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
        }
    }
}
