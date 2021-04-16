using Acorna.Core.Models.Email;
using System.Threading.Tasks;

namespace Acorna.Core.Services.Email
{
    public interface IEmailService
    {
        Task<bool> SendTestEmail();
    }
}
