using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Sheard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Core.Services.Project.BillingSystem
{
    public interface IOperatorService
    {
        Task<List<OperatorModel>> GetAllOperators();
        Task<PaginationRecord<OperatorModel>> GetOperators(int pageIndex, int pageSize);
        Task<OperatorModel> GetOperatorId(int operatorId);
        int AddOperator(OperatorModel operatorModel);
        bool UpdateOperator(OperatorModel ooperatorModel);
        bool DeleteOperator(int id);
    }
}
