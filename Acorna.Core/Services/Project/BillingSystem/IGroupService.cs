using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Sheard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Core.Services.Project.BillingSystem
{
    public interface IGroupService
    {
        Task<List<GroupModel>> GetAllGroups();
        Task<PaginationRecord<GroupModel>> GetGroups(int pageIndex, int pageSize);
        Task<GroupModel> GetGroupId(int groupId);
        int AddGroup(GroupModel jobModel);
        bool UpdateGroup(GroupModel jobModel);
        bool DeleteGroup(int id);
    }
}
