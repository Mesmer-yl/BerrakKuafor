using DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repos
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly AppDbContext _appDbContext;
        internal DbSet<T> dbSet;

        public GenericRepo(AppDbContext context)
        {
            _appDbContext = context;
            this.dbSet = _appDbContext.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public IEnumerable<T> GetAll(string? includeProps = null)
        {
            IQueryable<T> request = dbSet;
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var prop in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    request = request.Include(prop);
                }
            }
            return request.ToList();
        }

        public IEnumerable<T> GetAllByCondition(Expression<Func<T, bool>> filter, string? includeProps = null)
        {
            IQueryable<T> request = dbSet;
            request = request.Where(filter);
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var prop in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    request = request.Include(prop);
                }
            }
            return request.ToList();
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public T GetByIdWithProps(Expression<Func<T, bool>> filter, string? includeProps = null)
        {
            IQueryable<T> request = dbSet;
            request = request.Where(filter);
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var prop in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    request = request.Include(prop);
                }
            }
            return request.Single();
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            _appDbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
