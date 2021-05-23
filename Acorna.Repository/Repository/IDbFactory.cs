using Acorna.Repository.DataContext;

namespace Acorna.Repository.Repository
{
    public interface IDbFactory
    {
        AcornaDbContext DataContext { get; }
    }
}
