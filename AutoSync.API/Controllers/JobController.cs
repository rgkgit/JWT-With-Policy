using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoSync.API.Helper;
using AutoSync.API.Models;
using AutoSync.Core.Authorization;
using AutoSync.Core.Job;
using AutoSync.EFC.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static AutoSync.API.Helper.Constants;

namespace AutoSync.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ApiBaseController
    {
        ResponseModel response;
        IUnitOfWork _unitOfWork;
        public JobController(IHttpContextAccessor accessor, IUnitOfWork unitOfWork) : base(accessor) 
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("GetJobId")]
        public async Task<IActionResult> GetJobId()
        {
            response = new ResponseModel();
            try
            {
                long userId = base.UserId;
                Random random = new Random();
                string jobId = DateTime.Now.ToString("yyy-MM-dd") + "-" + random.Next(1000000).ToString("D6");
                string destinationFolderName = jobId + "-" + base.UserId;
                Setting setting = await _unitOfWork.GetRepository<Setting>().Where(x => x.UserId == userId).SingleOrDefaultAsync();
                Job job = new Job()
                {
                    UserId = userId,
                    JobId = jobId,
                    SourceFilePath = setting.FolderFilePath,
                    DestinationFilePath = destinationFolderName,
                    Status = JobStatus.Initiated.ToString()
                };
                _unitOfWork.GetRepository<Job>().Insert(job);
                int count = _unitOfWork.Commit();
                response.Status = count > 0;
                if (response.Status)
                    response.Data = new { JobId = jobId, DestinationFolderName = destinationFolderName };
            }
            catch (Exception ex)
            {
                response.Message = Constants.Error;
            }
            return Ok(response);
        }

        [HttpPost("InProgress")]
        public async Task<IActionResult> InProgress(JobRequestModel jobRequest)
        {
            response = new ResponseModel();
            try
            {
                long userId = base.UserId;
                Job job = await _unitOfWork.GetRepository<Job>().Where(x => x.UserId == userId && x.JobId == jobRequest.JobId).SingleOrDefaultAsync();
                if (job == null) { response.Message = Constants.Invalid_JobId; return BadRequest(response); }
                job.Status = JobStatus.InProgress.ToString();
                job.SyncType = jobRequest.IsManualSync ? "Manual" : "Automatic";
                if (!string.IsNullOrWhiteSpace(jobRequest.Reason))
                {
                    job.Status = JobStatus.Stopped.ToString();
                    job.Reason = jobRequest.Reason;
                }

                List<JobDetail> jobDetails = new List<JobDetail>();
                jobDetails.Add(await GetJobDetail(jobRequest.Reads, job.Id, userId, job.Status));
                jobDetails.Add(await GetJobDetail(jobRequest.Bills, job.Id, userId, job.Status));
                jobDetails.AddRange(await GetJobDetail(jobRequest.Images, job.Id, userId, job.Status));

                _unitOfWork.GetRepository<JobDetail>().Insert(jobDetails);

                int count = _unitOfWork.Commit();
                response.Status = count > 0;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = Constants.Error;
                return StatusCode(500, response);
            }
            return Ok(response);
        }

        [HttpPost("Completed")]
        public async Task<IActionResult> Completed(JobCompletedModel jobRequest)
        {
            response = new ResponseModel();
            try
            {
                long userId = base.UserId;
                Job job = await _unitOfWork.GetRepository<Job>().Where(x => x.UserId == userId && x.JobId == jobRequest.JobId).SingleOrDefaultAsync();
                if (job == null) { response.Message = Constants.Invalid_JobId; return BadRequest(response); }

                string filename = Path.GetFileName(jobRequest.FileName);
                JobDetail jobDetail = await _unitOfWork.GetRepository<JobDetail>().Where(x => x.JobId == job.Id && x.FileName == filename).SingleOrDefaultAsync();
                if (jobDetail == null) { response.Message = Constants.Invalid_FileName; return BadRequest(response); }

                jobDetail.S3FileUrl = jobRequest.S3Url;
                jobDetail.Status = JobStatus.Completed.ToString();
                int count = _unitOfWork.Commit();
                response.Data = new { JobCompleted = false };
                bool flag = _unitOfWork.GetRepository<JobDetail>().Any(x => x.JobId == job.Id && x.Status == JobStatus.InProgress.ToString());
                if (!flag)
                {
                    job.Status = JobStatus.Completed.ToString();
                    count = _unitOfWork.Commit();
                    response.Data = new { JobCompleted = true };
                }
                response.Status = count > 0;
            }
            catch (Exception ex)
            {
                response.Message = Constants.Error;
            }
            return Ok(response);
        }

        #region Private Methods
        private async Task<JobDetail> GetJobDetail(FileModel file, long jobId, long userId, string jobStatus)
        {
            return new JobDetail()
            {
                JobId = jobId,
                BaseFolderPath = !string.IsNullOrWhiteSpace(file.FileName) ? Path.GetDirectoryName(file.FileName) : string.Empty,
                FileName = !string.IsNullOrWhiteSpace(file.FileName) ? Path.GetFileName(file.FileName) : string.Empty,
                FileType = !string.IsNullOrWhiteSpace(file.FileName) ? Path.GetExtension(file.FileName) : string.Empty,
                FileSize = file.FileSize,
                Status = jobStatus,
            };
        }

        private async Task<List<JobDetail>> GetJobDetail(List<FileModel> files, long jobId, long userId, string jobStatus)
        {
            List<JobDetail> jobDetails = new List<JobDetail>();
            foreach (var file in files)
            {
                JobDetail jobDetail = new JobDetail()
                {
                    JobId = jobId,
                    BaseFolderPath = !string.IsNullOrWhiteSpace(file.FileName) ? Path.GetDirectoryName(file.FileName) : string.Empty,
                    FileName = !string.IsNullOrWhiteSpace(file.FileName) ? Path.GetFileName(file.FileName) : string.Empty,
                    FileType = !string.IsNullOrWhiteSpace(file.FileName) ? Path.GetExtension(file.FileName) : string.Empty,
                    FileSize = file.FileSize,
                    Status = jobStatus
                };

                jobDetails.Add(jobDetail);
            }
            return jobDetails;
        }
        #endregion
    }
}
