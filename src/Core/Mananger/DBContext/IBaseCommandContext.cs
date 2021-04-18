using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Mananger.DBContext
{
    public interface IBaseCommandContext : IBaseQueryContext
    {
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
