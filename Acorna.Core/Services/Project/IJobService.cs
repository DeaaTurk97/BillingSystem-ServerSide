using System.Collections.Generic;
using System.Threading.Tasks;
using Acorna.Core.Models.Project;
using Acorna.Core.Services;
using Acorna.Core.Sheard;

namespace Acorna.Core.IServices.Project
{
    public interface IJobService :ICommonService<JobModel>
    {
        Task<List<JobModel>> GetAllAsync();
        Task<PaginationRecord<JobModel>> GetAllAsync(int pageIndex, int pageSize);
    }
}
