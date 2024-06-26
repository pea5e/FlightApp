using FlightApp.Core.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FlightApp.Data.Repos
{
    public class BaseRepo<Type> : IBaseRepo<Type> where Type : class
    {
        protected FlightDbContext _dbContext;
        public BaseRepo(FlightDbContext context) {
            _dbContext = context;
        }
        public Type findById(int id)
        {
            return _dbContext.Set<Type>().Find(id);
        }

        public IEnumerable<Type> findAll()
        {
            return _dbContext.Set<Type>().ToList();
        }

        public IEnumerable<Type> findAllBy(Expression<Func<Type,bool>> match)
        {
            return _dbContext.Set<Type>().Where(match).ToList();
        }

        public Type find(Expression<Func<Type, bool>> match)
        {
            return _dbContext.Set<Type>().SingleOrDefault(match);
        }

        public Type Add(Type type)
        {
            return _dbContext.Set<Type>().Add(type).Entity;
        }
    }
}
