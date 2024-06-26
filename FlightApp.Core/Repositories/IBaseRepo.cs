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
        public Type findById(int id, string[] matches = null);

        public IEnumerable<Type> findAll(string[] matches = null);

        public IEnumerable<Type> findAllBy(Expression<Func<Type, bool>> match, string[] matches=null);

        public Type find(Expression<Func<Type, bool>> match, string[] matches = null);

        public Type Add(Type type);

        public Type Remove(Type type);

    }
}
