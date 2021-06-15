using System;
using System.Collections.Generic;
using System.Text;

namespace Acorna.Core.Services.Project.BillingSystem
{
    public interface IBillService
    {
        bool UploadMTNBills(List<DocumentModel> filesUploaded, int currentUserId);
        bool UploadSyriaTelBills(List<DocumentModel> filesUploaded, int currentUserId);
    }
}
