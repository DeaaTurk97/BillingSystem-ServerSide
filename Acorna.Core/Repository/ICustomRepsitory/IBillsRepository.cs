using Acorna.Core.Entity.Project.BillingSystem;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Core.Repository.ICustomRepsitory
{
    public interface IBillsRepository : IRepository<Bill>
    {

        #region Lebanon....
        Task<List<string>> GetbillsGreaterThanServicesPrices();
        #endregion
    }
}
