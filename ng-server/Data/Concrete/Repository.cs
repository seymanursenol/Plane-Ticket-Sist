using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ng_server.Data.Abstract;

namespace ng_server.Data.Concrete
{
    public class Repository<TEntity,TContext>: IRepository<TEntity>
    where TEntity: class
    where TContext: DbContext,new()
    {
        public TEntity GetById(int id)
        {
            using(var context= new TContext())
            {
                return context.Set<TEntity>().Find(id);
            }
        }
        public TEntity GetByEmail(string email)
        {
            using(var context= new TContext())
            {
                return context.Set<TEntity>().Find(email);
            }
        }

        public List<TEntity> GetAll()
        {
            using(var context= new TContext())
            {
                return context.Set<TEntity>().ToList();
            }
        }

        public void Create (TEntity entity)
        {
            using(var context= new TContext())
            {
                context.Set<TEntity>().Add(entity);
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using(var context= new TContext())
            {
                context.Set<TEntity>().Remove(entity);
                context.SaveChanges();
            }
        }

        public virtual void Update(TEntity entity)
        {
            using(var context= new TContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}