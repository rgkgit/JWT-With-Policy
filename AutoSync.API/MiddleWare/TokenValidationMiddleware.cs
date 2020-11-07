using AutoSync.API.Models;
using AutoSync.API.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace AutoSync.API.MiddleWare
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly TokenHelper _tokenHelper;
        private ResponseModel response;
        public TokenValidationMiddleware(RequestDelegate next, IOptions<JwtIssuerOptions> jwtOptions, TokenHelper tokenHelper)
        {
            _next = next;
            _jwtOptions = jwtOptions.Value;
            _tokenHelper = tokenHelper;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            response = new ResponseModel();
            if (ValidateUrl(httpContext.Request.Path.Value.ToLower())) { await _next(httpContext); return; }
            if (!httpContext.Request.Method.Equals(HttpMethods.Post)) { await _next(httpContext); return; }
            if (!httpContext.Request.Headers.Keys.Contains("user-key"))
            {
                response.Message = "Authorization-Token missing";
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
                return;
            }

            httpContext.Request.Headers.TryGetValue("user-key", out StringValues val);
            bool isValid = _tokenHelper.ValidateToken(_jwtOptions, val, out long userId, out long roleId);
            if (!isValid)
            {
                response.Message = "Invalid Token";
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
                return;
            }
            httpContext.Items["UserId"] = userId;
            await _next(httpContext);
        }
        private bool ValidateUrl(string url)
        {
            List<string> exList = new List<string>
            {
                "/api/user/login"
            };
            return exList.Contains(url);
        }
    }

    public static class TokenValidationMiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenValidationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TokenValidationMiddleware>();
        }
    }
}
