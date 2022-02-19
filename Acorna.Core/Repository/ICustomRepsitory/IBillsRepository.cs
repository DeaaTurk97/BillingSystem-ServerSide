using Acorna.Core.Entity.Project.BillingSystem;
using Acorna.Core.Models.Notification;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Core.Repository.ICustomRepsitory
{
    public interface IBillsRepository : IRepository<Bill>
    {

        #region Lebanon....
        Task<Tuple<List<string>, List<NotificationItemModel>, List<NotificationItemModel>, List<NotificationItemModel>>> GetbillsGreaterThanServicesPrices();
        #endregion
    }
}
