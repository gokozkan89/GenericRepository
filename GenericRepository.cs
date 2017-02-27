using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
namespace GenericRepository
{
    public abstract class GenericRepository<CEntity, TEntity> : IGenericRepository<TEntity> where TEntity : class where CEntity : DbContext, new()
    {
        private CEntity _entities = new CEntity();
        public CEntity Context
        {
            get { return _entities;}
            set { _entities = value;}
        }
        

        public void Add(TEntity entity)
        {
            _entities.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public void Edit(TEntity entity)
        {
            _entities.Entry(entity).State=EntityState.Modified;
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = _entities.Set<TEntity>().Where(predicate);
            return query;
        }

        public IQueryable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = _entities.Set<TEntity>();
            return query;
        }

        public void Save()
        {
            _entities.SaveChanges();
        }
    }
}