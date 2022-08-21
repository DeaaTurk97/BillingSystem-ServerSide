using Acorna.Repository.DataContext;
using System;

namespace Acorna.Repository.Repository
{
    public class DbFactory : IDbFactory, IDisposable
    {
        private readonly AcornaDbContext _dbContext;

        public DbFactory(AcornaDbContext acornaDataContext)
        {
            _dbContext = acornaDataContext;
        }

        public AcornaDbContext DataContext
        {
            get
            {
                return _dbContext;
            }
        }

        #region Dispose

        private bool isDisposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if(!isDisposed && disposing)
                {
                if(_dbContext != null)
                {
                    _dbContext.Dispose();
                }
            }

            isDisposed = true;
        }
        #endregion

    }
}
