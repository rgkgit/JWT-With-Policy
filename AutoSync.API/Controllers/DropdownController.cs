using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoSync.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoSync.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DropdownController : ApiBaseController
    {
        public DropdownController(IHttpContextAccessor accessor) : base(accessor) { }
        
    }
}
