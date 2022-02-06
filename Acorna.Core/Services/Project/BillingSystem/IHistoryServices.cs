using Acorna.Core.Models.Project.BillingSystem;
using Acorna.Core.Sheard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Acorna.Core.Services.Project.BillingSystem
{
    public interface IHistoryServices
    {
        Task<List<HistoryModel>> GetAllHistory();
        Task<PaginationRecord<HistoryModel>> GetHistory(int pageIndex, int pageSize);
        Task<HistoryModel> GetHistoryId(int planId);
        int AddHistory(HistoryModel historyModel);
        bool UpdateHistory(HistoryModel historyModel);
        bool DeleteHistory(int id);
    }
}
