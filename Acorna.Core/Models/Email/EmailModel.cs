namespace Acorna.Core.Models.Email
{
    public class EmailModel : BaseModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public string CC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string DisplayNameEmail { get; set; }
        public string HostServer { get; set; }
        public bool EnableSsl { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public bool IsRequiresAuthentication { get; set; }
        public int Port { get; set; }
    }
}
