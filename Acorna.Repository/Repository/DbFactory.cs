using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Acorna.Repository.DataContext;

namespace Acorna.Repository.Repository
{
    public class DbFactory : IDbFactory,IDisposable
    {
        private readonly AcornaDbContext _dbContext;

        public DbFactory(AcornaDbContext teamDataContext)
        {
            _dbContext = teamDataContext;
        }

        public AcornaDbContext GetDataContext
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
