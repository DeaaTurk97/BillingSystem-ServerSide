namespace Acorna.Core.DTOs
{
    public class SystemEnum
    {
        public enum NotificationType
        {
            PhoneNumbersSubmitted = 10,
            PhoneNumbersApproved = 20,
            PhoneNumbersInProgress = 30,
            PhoneNumbersRejected = 40,
            BillUploaded = 50,
            BillSubmitted = 60,
            BillApproved = 70,
            BillInProgress = 80,
            BillRejected = 90,
            BillPaid = 100,
            Chatting = 110,
            ServicesSubmitted = 120,
            ServicesApproved = 130,
            ServicesInProgress = 140,
            ServicesRejected = 150
        }

        public enum TypesPhoneNumber
        {
            Free = 1,
            Official = 2,
            Personal = 3,
            Unknown = 4,
        }

        public enum StatusCycleBills
        {
            Upload = 0,
            Submit = 1,
            InprogressToApproved = 2,
            Approved = 3,
            Rejected = 4,
        }

        public enum RolesType
        {
            SuperAdmin = 1,
            AdminGroup = 2,
            Employee = 3,
            Finance = 4,
        }
        public enum ReportNames
        {
            CallSummary,
            CallFinance,
            CallDetails
        }
        public enum Languages
        {
            ar,
            en
        }
        public enum SimType
        {
            Voice = 1,
            Data = 2,
        }
        public enum SimProfileType
        {
            Activated = 1,
            Deactivate = 2,
            Stock = 3,
        }
    }
}
