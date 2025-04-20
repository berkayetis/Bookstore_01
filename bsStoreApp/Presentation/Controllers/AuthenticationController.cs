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
    [ApiExplorerSettings(GroupName = "v1")]

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

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto userForAuthenticationDto)
        {
            var isValid = await _serviceManager.AuthenticationService.ValidateUser(userForAuthenticationDto);
            if(!isValid)
            {
                return Unauthorized("Invalid user and password");
            }

            var tokenDto = await _serviceManager.AuthenticationService.CreateToken(true);
            return Ok(tokenDto);
        }

        [HttpPost("refresh")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
        {
            TokenDto resultToken = await _serviceManager.AuthenticationService.RefreshToken(tokenDto);
            return Ok(resultToken);
        }
    }
}
