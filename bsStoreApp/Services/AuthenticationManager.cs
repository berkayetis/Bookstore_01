using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationManager : IAuthenticationService
    {
        readonly ILoggerService _logger;
        readonly IMapper _mapper;
        readonly UserManager<User> _manager;
        readonly IConfiguration _configuration;

        public AuthenticationManager(ILoggerService logger, IMapper mapper, UserManager<User> manager, IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _manager = manager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> RegisterUser(UserForRegistirationDto userForRegistirationDto)
        {
            User user = _mapper.Map<User>(userForRegistirationDto);

            IdentityResult result = await _manager.CreateAsync(user, userForRegistirationDto.Password);
            if(result.Succeeded)
            {
                await _manager.AddToRolesAsync(user, userForRegistirationDto.Roles);
            }

            return result;
        }
    }
}
