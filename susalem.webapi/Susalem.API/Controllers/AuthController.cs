using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Susalem.Core.Application;
using Susalem.Core.Application.DTOs;
using Susalem.Core.Application.Interfaces.Services;
using Susalem.Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Susalem.Core.Application.Localize;
using Susalem.Core.Application.Commands.Auth;
using Susalem.Core.Application.Interfaces;

namespace Susalem.Api.Controllers
{
    /// <summary>
    /// User login and logout.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserAuthenticationService _userAuthenticationService;
        private readonly IApplicationConfigurationService _configurationService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly IServiceBus _serviceBus;
        private readonly IStringLocalizer<Resource> _stringLocalizer;


        public AuthController(IUserAuthenticationService userAuthenticationService, 
            IApplicationConfigurationService configurationService,
            SignInManager<ApplicationUser> signInManager, 
            IJwtFactory jwtFactory, 
            IServiceBus serviceBus,
            IStringLocalizer<Resource> stringLocalizer)
        {
            _userAuthenticationService = userAuthenticationService;
            _configurationService = configurationService;
            _signInManager = signInManager;
            _jwtFactory = jwtFactory;
            _serviceBus = serviceBus;
            _stringLocalizer = stringLocalizer;
        }

        /// <summary>
        /// login with name and password and get jwt token.
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Auth([FromBody]LoginRequestDTO loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid payload.");
            }

            var result = await _userAuthenticationService.Login(loginViewModel);
            if (result.Failed)
            {
                return BadRequest(result.MessageWithErrors);
            }
            
            var identity = _signInManager.Context.User.Identities.FirstOrDefault();
            if (identity == null)
            {
                return BadRequest("User identity not found.");
            }

            identity.AddClaim(new Claim(ClaimTypes.GivenName, result.Data.UserName));

            foreach (var permission in result.Data.Permissions)
            {
                identity.AddClaim(new Claim(Permissions.Name, permission));
            }
            var token = _jwtFactory.GenerateJwtToken(identity);
            var exportExcel = _configurationService.GetValueBool(Configuration.ExportExcel).Data;
            var customerName = _configurationService.GetValue(Configuration.CustomerNameKey).Data;
            var config = new
            {
                exportExcel,
                customerName
            };
            return Ok(new
            {
                token = token,
                userName = result.Data.UserName,
                result.Data.Permissions,
                config
            });
        }

        /// <summary>
        /// User logout
        /// </summary>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("logout")]
        [HttpPost]
        public async Task Logout()
        {
            await _serviceBus.Send(new UnAuthCommand());
        }
    }
}
