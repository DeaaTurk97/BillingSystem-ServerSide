using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.IServices.Project;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Repository;
using Acorna.Core.Sheard;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Service.Project
{
    public class JobService : IJobService
    {
        private readonly IRepository<Job> _jobRepository;
        private readonly IMapper _mapper;

        public JobService(IRepository<Job> jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
        }

        public async Task<List<JobModel>> GetAllJobs()
        {
            try
            {
                List<Job> job = await _jobRepository.GetAllAsync();
                return _mapper.Map<List<JobModel>>(job); ;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<PaginationRecord<JobModel>> GetJobs(int pageIndex, int pageSize)
        {
            try
            {
                PaginationRecord<Job> job = await _jobRepository.GetAllAsync(pageIndex, pageSize, x => x.Id, OrderBy.Descending);
                PaginationRecord<JobModel> paginationRecordModel = new PaginationRecord<JobModel>
                {
                    DataRecord = _mapper.Map<IEnumerable<JobModel>>(job.DataRecord),
                    CountRecord = job.CountRecord,
                };
                return paginationRecordModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JobModel> GetJobId(int jobId)
        {
            try
            {
                Job jobModel = await _jobRepository.GetSingleAsync(jobId);
                return _mapper.Map<JobModel>(jobModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetCountRecord()
        {
            try
            {
                return _jobRepository.GetTotalCount();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddJob(JobModel jobModel)
        {
            try
            {
                Job job = _mapper.Map<Job>(jobModel);
                if (job != null)
                    _jobRepository.Insert(job);

                return job.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateJob(JobModel jobModel)
        {
            try
            {
                Job job = _jobRepository.GetSingle(jobModel.Id);
                job.JobNameAr = jobModel.JobNameAr;
                job.JobNameEn = jobModel.JobNameEn;

                if (job != null)
                {
                    _jobRepository.Update(job);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteJob(int id)
        {
            try
            {
                Job job = _jobRepository.GetSingle(id);
                if (job != null)
                    _jobRepository.Delete(job);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

