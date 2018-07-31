using System.Collections.Generic;

namespace No7.Solution
{
    public interface ITradeRepository<T>
    {
        void InsertMany(IEnumerable<T> entities);
    }
}
