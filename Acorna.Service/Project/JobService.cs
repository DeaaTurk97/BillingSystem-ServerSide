using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acorna.Core.Entity;
using Acorna.Core.IServices.Project;
using Acorna.Core.Models.Project;
using Acorna.Core.Repository;
using Acorna.Core.Sheard;

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

        public async Task<List<JobModel>> GetAllAsync()
        {
            try
            {
                List<Job> job = await _jobRepository.GetAllAsync();
                List<JobModel> jobModelList = _mapper.Map<List<JobModel>>(job);
                return jobModelList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<PaginationRecord<JobModel>> GetAllAsync(int pageIndex, int pageSize)
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

        public int Insert(JobModel jobModel)
        {
            int id = 0;
            try
            {
                Job job = _mapper.Map<Job>(jobModel);
                if (job != null)
                    _jobRepository.Insert(job);
                id = job.Id;
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(JobModel jobModel)
        {
            try
            {
                Job job = _jobRepository.GetSingle(jobModel.Id);

                if (job != null)
                {
                    job.JobName = jobModel.JobName;
                    _jobRepository.Update(job);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int id)
        {
            try
            {
                Job job = _jobRepository.GetSingle(id);
                if (job != null)
                    _jobRepository.Delete(job);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

