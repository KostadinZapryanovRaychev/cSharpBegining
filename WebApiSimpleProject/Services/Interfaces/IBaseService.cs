using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSimpleProject.Services.Interfaces
{
    public interface IBaseService<T>
    {
        public Task<List<T>> GetAll();

        public Task<T> Get(int id);

        public Task<bool> Update(T element);

        public Task<T> Add(T element);

        public Task<bool> Delete(int id);

        public Task<bool> Exist(int id);
    }
}
