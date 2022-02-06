using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Sheard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Acorna.Core.Services.Project.BillingSystem
{
    public interface IPlanService
    {
        Task<List<PlanModel>> GetAllPlans();
        Task<PaginationRecord<PlanModel>> GetPlan(int pageIndex, int pageSize);
        Task<PlanModel> GetPlanId(int planId);
        int AddPlan(PlanModel planModel);
        bool UpdatePlan(PlanModel planModel);
        bool DeletePlan(int id);
    }
}
