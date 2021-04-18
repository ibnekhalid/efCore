using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public interface IRepository
    {
        Task<int> SaveChanges(CancellationToken cancellationToken = default);
    }
}
