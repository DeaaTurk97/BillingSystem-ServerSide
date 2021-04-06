using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.IServices.Project;
using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Repository;
using Acorna.Core.Sheard;
using Acorna.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Service.Project
{
    public class JobService : IJobService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public JobService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<JobModel>> GetAllJobs()
        {
            try
            {
                List<Job> job = await _unitOfWork.GetRepository<Job>().GetAllAsync();
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
                PaginationRecord<Job> job = await _unitOfWork.GetRepository<Job>().GetAllAsync(pageIndex, pageSize, x => x.Id, OrderBy.Descending);
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
                Job jobModel = await _unitOfWork.GetRepository<Job>().GetSingleAsync(jobId);
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
                return _unitOfWork.GetRepository<Job>().GetTotalCount();
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
                {
                    _unitOfWork.GetRepository<Job>().Insert(job);
                    _unitOfWork.SaveChanges();
                }

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
                Job job = _unitOfWork.GetRepository<Job>().GetSingle(jobModel.Id);

                if (job != null)
                {
                    job.JobNameAr = jobModel.JobNameAr;
                    job.JobNameEn = jobModel.JobNameEn;

                    _unitOfWork.GetRepository<Job>().Update(job);
                    _unitOfWork.SaveChanges();
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
                Job job = _unitOfWork.GetRepository<Job>().GetSingle(id);

                if (job != null)
                {
                    _unitOfWork.GetRepository<Job>().Delete(job);
                    _unitOfWork.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

