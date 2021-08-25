using Acorna.Core.Models.Project.BillingSystem;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acorna.Core.Services.Project.BillingSystem
{
    public interface ITypePhoneNumberService
    {
        Task<List<TypePhoneNumberModel>> GetAllTypesPhoneNumber();
        Task<List<TypePhoneNumberModel>> GetAllTypesWithoutFreeType();
    }
}
