using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Sheard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Core.IServices.Project
{
    public interface IJobService
    {
        Task<List<JobModel>> GetAllJobs();
        Task<PaginationRecord<JobModel>> GetJobs(int pageIndex, int pageSize);
        Task<JobModel> GetJobId(int jobId);
        int AddJob(JobModel jobModel);
        bool UpdateJob(JobModel jobModel);
        bool DeleteJob(int id);
    }
}
