using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Acorna.Core.Entity;
using Acorna.Core.Repository;
using Acorna.Core.Sheard;
using Acorna.Repository.DataContext;

namespace Acorna.Repository.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AcornaContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private bool _disposed;

        public Repository(IDbFactory dbFactory, IUnitOfWork unitOfWork)
        {
            try
            {
                _context = dbFactory.GetDataContext;
                _unitOfWork = unitOfWork;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<T> GetAll()
        {
            try
            {
                return _context.Set<T>().ToList();
            }
            catch (Exception)
            {

                throw new Exception("Error When Get All");
            }
        }

        public Task<List<T>> GetAllAsync()
        {
            try
            {
                return _context.Set<T>().ToListAsync();
            }
            catch (Exception)
            {
                throw new Exception("Error When Get All Async");
            }
        }

        public PaginationRecord<T> GetAll(int pageIndex, int pageSize)
        {
            try
            {
                return GetAll(pageIndex, pageSize, x => x.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PaginationRecord<T> GetAll(int pageIndex, int pageSize, Expression<Func<T, int>> keySelector, OrderBy orderBy = OrderBy.Ascending)
        {
            return GetAll(pageIndex, pageSize, keySelector, null, orderBy);
        }

        public PaginationRecord<T> GetAll(int pageIndex, int pageSize, Expression<Func<T, int>> keySelector, Expression<Func<T, bool>>[] predicate, OrderBy orderBy, params Expression<Func<T, object>>[] includeProperties)
        {
            return this.GetAllAsync(pageIndex, pageSize, keySelector, predicate, orderBy, includeProperties).Result;
        }

        public Task<T> GetSingleAsync(int id)
        {
            return _context.Set<T>().FirstOrDefaultAsync(t => t.Id == id);
        }

        public Task<PaginationRecord<T>> GetAllAsync(int pageIndex, int pageSize)
        {
            try
            {
                return GetAllAsync(pageIndex, pageSize, x => x.Id);
            }
            catch (Exception)
            {
                throw new Exception("Error When Get All Async");
            }
        }

        public Task<PaginationRecord<T>> GetAllAsync(int pageIndex, int pageSize, Expression<Func<T, int>> keySelector, OrderBy orderBy = OrderBy.Ascending)
        {
            return GetAllAsync(pageIndex, pageSize, keySelector, null, orderBy);
        }

        public async Task<PaginationRecord<T>> GetAllAsync(int pageIndex, int pageSize, Expression<Func<T, int>> keySelector,
           Expression<Func<T, bool>>[] predicate, OrderBy orderBy, params Expression<Func<T, object>>[] includeProperties)
        {
            var entities = FilterQuery(keySelector, predicate, orderBy, includeProperties);
            var total = await entities.CountAsync();// entities.CountAsync() is different than pageSize
            entities = entities.Paginate(pageIndex, pageSize);
            var list = await entities.ToListAsync();

            PaginationRecord<T> paginationRecordModel = new PaginationRecord<T>
            {
                DataRecord = list.ToPaginatedList(pageIndex, pageSize, total),
                CountRecord = total,
            };

            return paginationRecordModel;
        }

        private IQueryable<T> FilterQuery(Expression<Func<T, int>> keySelector, Expression<Func<T, bool>>[] predicate, OrderBy orderBy, Expression<Func<T, object>>[] includeProperties)
        {
            var entities = GetAllWhere(includeProperties);
            entities = (predicate != null) ? PredicateProperties(predicate) : entities;
            entities = (orderBy == OrderBy.Ascending) ? entities.OrderBy(keySelector) : entities.OrderByDescending(keySelector);

            return entities;
        }

        private IQueryable<T> PredicateProperties(Expression<Func<T, bool>>[] predicateProperties)
        {
            try
            {
                IQueryable<T> entities = _context.Set<T>();
                foreach (var predicate in predicateProperties)
                {
                    entities = entities.Where(predicate);
                }
                return entities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private IQueryable<T> GetAllWhere(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> entities = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                entities = entities.Include(includeProperty);
            }
            return entities;
        }

        public T GetSingle(int id)
        {
            try
            {
                return _context.Set<T>().AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
            }
            catch (Exception)
            {

                throw new Exception("Entity is Null Please Check on Your Data");
            }
        }
        public T FirstOrDefault(Expression<Func<T, bool>> predicate) => _context.Set<T>().AsNoTracking().FirstOrDefault(predicate);
        public IEnumerable<T> GetWhere(Expression<Func<T, bool>> predicate) => _context.Set<T>().AsNoTracking().Where(predicate).AsEnumerable();

        public int Insert(T entity)
        {
            try
            {
                if (entity != null)
                {
                    _context.Set<T>().Add(entity);
                    _unitOfWork.SaveChanges();

                    return entity.Id;
                }
                else
                {
                    throw new Exception("Entity is Null Please Check on Your Data");
                }
            }
            catch (Exception)
            {
                throw new ArgumentNullException("Entity is Null Please Check on Your Data");
            }
        }
        public bool InsertRange(List<T> listEntities)
        {
            try
            {
                if (listEntities != null)
                {
                    _context.Set<T>().AddRange(listEntities);
                    _unitOfWork.SaveChanges();

                    return true;
                }
                else
                {
                    throw new Exception("Entity is Null Please Check on Your Data");
                }
            }
            catch (Exception)
            {
                throw new ArgumentNullException("Entity is Null Please Check on Your Data");
            }
        }

        public bool Update(T entity)
        {
            try
            {
                if (entity != null)
                {
                    _context.Set<T>().Attach(entity);
                    _context.Entry(entity).State = EntityState.Modified;
                    _unitOfWork.SaveChanges();

                    return true;
                }
                else
                {
                    throw new ArgumentNullException("Entity is Null Please Check on Your Data");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error When Update Record" + ex);
            }
        }

        public bool UpdateRange(List<T> entity)
        {
            try
            {
                if (entity != null)
                {
                    _context.Set<T>().AttachRange(entity);
                    _context.UpdateRange(entity);
                    _unitOfWork.SaveChanges();

                    return true;
                }
                else
                {
                    throw new ArgumentNullException("Entity is Null Please Check on Your Data");
                }
            }
            catch (Exception)
            {
                throw new Exception("Error When Update Range Record");
            }
        }

        public bool Delete(T entity)
        {
            try
            {
                if (entity != null)
                {
                    _context.Set<T>().Remove(entity);
                    _unitOfWork.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw new Exception("Error When Delete Record");
            }
        }
        public bool DeleteRange(List<T> listEntities)
        {
            try
            {
                if (listEntities != null)
                {
                    _context.Set<T>().RemoveRange(listEntities);
                    _unitOfWork.SaveChanges();

                    return true;
                }
                else
                {
                    throw new Exception("Entity is Null Please Check on Your Data");
                }
            }
            catch (Exception)
            {
                throw new ArgumentNullException("Entity is Null Please Check on Your Data");
            }
        }

        public int GetTotalCount()
        {
            try
            {
                return _context.Set<T>().Count();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsContains(Expression<Func<T, bool>>[] predicateProperties)
        {
            try
            {
                IQueryable<T> entities = _context.Set<T>();

                foreach (var predicate in predicateProperties)
                {
                    entities = entities.Where(predicate);
                }

                return entities.Count() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            try
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            catch (Exception)
            {
                throw new Exception("Error When Dispose");
            }
        }

        public virtual void Dispose(bool disposing)
        {
            try
            {
                if (!_disposed && disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
            catch (Exception)
            {
                throw new Exception("Error When Dispose");
            }
        }

        public Task<List<T>> GetAllIncludingAsync(Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> entities = GetAllWhere(includeProperties);
            return entities.ToListAsync();
        }

        public List<T> GetAllIncludingWithPredicate(Expression<Func<T, object>>[] includeProperties, Expression<Func<T, bool>>[] predicateProperties)
        {
            IQueryable<T> entities = GetAllWhere(includeProperties);

            foreach (var predicate in predicateProperties)
            {
                entities = entities.Where(predicate);
            }
            return entities.ToList();
        }

        public void BeginTransaction()
        {
            _unitOfWork.BeginTransaction();
        }

        public void RollBackTransaction()
        {
            _unitOfWork.RollBackTransaction();
        }

        public void CommitTransaction()
        {
            _unitOfWork.CommitTransaction();
        }
    }
}