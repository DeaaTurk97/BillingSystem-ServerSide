using System;
using System.Collections.Generic;
using System.Text;

namespace Acorna.Core.Services.Project.BillingSystem
{
    public interface IBillService
    {
        List<string> UploadMTNBills(List<DocumentModel> filesUploaded, int currentUserId);
        List<string> UploadSyriaTelBills(List<DocumentModel> filesUploaded, int currentUserId);
    }
}
