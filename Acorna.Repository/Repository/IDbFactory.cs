using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Acorna.Repository.DataContext;

namespace Acorna.Repository.Repository
{
    public interface IDbFactory
    {
        AcornaContext GetDataContext { get; }
    }
}
