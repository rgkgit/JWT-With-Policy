using AutoSync.API.Helper;
using AutoSync.API.Models;
using AutoSync.API.Options;
using AutoSync.Core.Authorization;
using AutoSync.EFC;
using AutoSync.EFC.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AutoSync.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiBaseController
    {
        private readonly TokenHelper _tokenHelper;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly IUnitOfWork _unitOfWork;
        ResponseModel response;
        public UserController(IHttpContextAccessor accessor, TokenHelper tokenHelper, IOptions<JwtIssuerOptions> jwtOptions, IUnitOfWork unitOfWork) : base(accessor)
        {
            _tokenHelper = tokenHelper;
            _jwtOptions = jwtOptions.Value;
            _unitOfWork = unitOfWork;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            response = new ResponseModel();
            try
            {
                User user = await _unitOfWork.GetRepository<User>().Where(x => x.Username == loginModel.Username && x.Password == loginModel.Password && x.IsActive).SingleOrDefaultAsync();
                if (user == null) { response.Message = Constants.User_Not_Exist; return NotFound(response); }

                UserRoles userRole = await _unitOfWork.GetRepository<UserRoles>().Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

                if (userRole.RoleId == 5)
                {
                    if (string.IsNullOrWhiteSpace(loginModel.DeviceId)) { response.Message = Constants.Device_Id_Required; return BadRequest(response); }

                    Setting setting = await _unitOfWork.GetRepository<Setting>().Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

                    if (string.IsNullOrWhiteSpace(setting.DeviceId))
                    {
                        setting.DeviceId = loginModel.DeviceId;
                        _unitOfWork.Commit();
                    }
                    else
                    {
                        bool deviceExist = setting.DeviceId == loginModel.DeviceId;
                        if (!deviceExist) { response.Message = Constants.Device_Exist; return Conflict(response); }
                    }
                }

                response.Status = true;
                Response.Headers.Add("user-key", GetToken(user.Id, userRole.Id));

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = Constants.Error;
                return StatusCode(500, response);
            }
            return Ok(response);
        }

        [HttpPost("Profile")]
        public async Task<IActionResult> Profile()
        {
            response = new ResponseModel();
            try
            {
                long userId = base.UserId;
                UserDetailModel profile = await _unitOfWork.GetRepository<Setting>().Where(x => x.UserId == userId).Select(x => new UserDetailModel()
                {
                    Id = x.UserId,
                    DeviceId = x.DeviceId,
                    FolderFilePath = x.FolderFilePath,
                    SupervisorId = x.SupervisorId,
                    AutoSyncTime = x.AutoSyncTime,
                    AutoSyncDays = x.AutoSyncDays,
                    AutoDeleteInterval = x.AutoDeleteInterval,
                }).SingleOrDefaultAsync();
                response.Status = profile != null;
                if (response.Status)
                    response.Data = profile;
                else
                    response.Message = response.Message = Constants.User_Not_Exist;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = Constants.Error;
                return StatusCode(500, response);
            }
            return Ok(response);
        }
        private string GetToken(long userId, long roleId)
        {
            return _tokenHelper.CreateToken(_jwtOptions, new[] { new Claim("UserId", Convert.ToString(userId)), new Claim("LangId", Convert.ToString(roleId)) });
        }
    }
}
