using AutoSync.API.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;

namespace AutoSync.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TokenHelper _tokenHelper;
        private readonly JwtIssuerOptions _jwtOptions;
        ResponseModel response;
        public AuthController(TokenHelper tokenHelper, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _tokenHelper = tokenHelper;
            _jwtOptions = jwtOptions.Value;
        }
        [HttpPost("Login")]
        public IActionResult Login()
        {
            response = new ResponseModel() { Status = true };
            Response.Headers.Add("user-key", GetToken(1));
            return Ok(response);
        }

        private string GetToken(long userId)
        {
            return _tokenHelper.CreateToken(_jwtOptions, new[] { new Claim("UserId", Convert.ToString(userId)) });
        }
    }
}
