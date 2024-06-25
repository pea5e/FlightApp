using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Core.Repositories
{
    public interface IBaseRepo<Type> where Type : class
    {
        public Type getById(int id);

        public IEnumerable<Type> getAll();

    }
}
