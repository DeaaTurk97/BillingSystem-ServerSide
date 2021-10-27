using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Core.Services.Project.BillingSystem
{
    public interface IBillService
    {
        #region Syria...

        List<string> UploadMTNBills(List<DocumentModel> filesUploaded, int currentUserId);
        List<string> UploadSyriaTelBills(List<DocumentModel> filesUploaded, int currentUserId);

        #endregion

        #region Lebanon...

        bool UploadCallsAndRoamingLebanon(List<DocumentModel> filesUploaded, string billType, int currentUserId);
        bool UploadDataRoamingLebanon(List<DocumentModel> filesUploaded, int currentUserId);
        bool UploadDataLebanon(List<DocumentModel> filesUploaded, int currentUserId);
        Task<List<string>> ReminderUsersAddedBills();

        #endregion

    }
}
