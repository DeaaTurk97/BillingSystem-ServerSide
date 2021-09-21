using Acorna.Core.DTOs.billingSystem;
using Acorna.Core.Sheard;
using System.Threading.Tasks;

namespace Acorna.Core.Repository.ICustomRepsitory
{
    public interface IComingServicesRepository
    {
        Task<PaginationRecord<ServicesNeedApprovedDTO>> GetAllComingServices(int pageIndex, int pageSize, int statusNumber);
        Task<PaginationRecord<ServicesNeedApprovedDTO>> GetComingServicesByGroupId(int pageIndex, int pageSize, int statusNumber, int userId);
    }
}
