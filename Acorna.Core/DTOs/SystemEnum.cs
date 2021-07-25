namespace Acorna.Core.DTOs
{
    public class SystemEnum
    {
        public enum NotificationType
        {
            Chatting = 10,
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
            Available = 1,
            Submit = 2,
            InprogressToApproved = 3,
            Approved = 4,
            Rejected = 5,
        }

        public enum RolesType
        {
            SuperAdmin = 1,
            Admin = 2,
            AdminGroup = 3,
            Employee = 4,
            Guest = 5,
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
    }
}
