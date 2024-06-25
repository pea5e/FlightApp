using FlightApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Data.Repos
{
    public class BaseRepo<Type> : IBaseRepo<Type> where Type : class
    {
        protected FlightDbContext _dbContext;
        public BaseRepo(FlightDbContext context) {
            _dbContext = context;
        }
        public Type getById(int id)
        {
            return _dbContext.Set<Type>().Find(id);
        }

        public IEnumerable<Type> getAll()
        {
            return _dbContext.Set<Type>().ToList();
        }
    }
}
