using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Mappers
{
    public interface IUserMapper<T, U>
    {

        public Task<U> Cast(T t);

        public Task<T> Cast(U u);
    }
}
