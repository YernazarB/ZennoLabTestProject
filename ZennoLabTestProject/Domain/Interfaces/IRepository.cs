using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZennoLabTestProject.Domain.Interfaces
{
    public interface IRepository<T>
    {
        Task Save(T entity);
        Task<List<T>> Get();
    }
}