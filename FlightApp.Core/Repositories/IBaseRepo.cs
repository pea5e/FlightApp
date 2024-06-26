using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Core.Repositories
{
    public interface IBaseRepo<Type> where Type : class
    {
        public Type findById(int id);

        public IEnumerable<Type> findAll();

        public IEnumerable<Type> findAllBy(Expression<Func<Type, bool>> match);

        public Type find(Expression<Func<Type, bool>> match);

        public Type Add(Type type);

    }
}
