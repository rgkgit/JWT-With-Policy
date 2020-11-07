using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoSync.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoSync.API.Controllers
{
    public class ApiBaseController : ControllerBase
    {
        private readonly IHttpContextAccessor accessor;
        public ApiBaseController(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public long UserId
        {
            get
            {
                var obj = this.accessor.HttpContext.Items["UserId"];
                if (obj == null) { return 0; }
                return Convert.ToInt64(obj);
            }
        }

        public long RoleId
        {
            get
            {
                var obj = this.accessor.HttpContext.Items["RoleId"];
                if (obj == null) { return 0; }
                return Convert.ToInt64(obj);
            }
        }

        protected string EncryptPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
        }

        protected bool ValidatePassword(string password, string dbPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, dbPassword);
        }
    }
}
