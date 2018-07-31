using System.Collections.Generic;

namespace No7.Solution
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        void InsertMany(IEnumerable<T> entities);

        void Save();
    }
}
