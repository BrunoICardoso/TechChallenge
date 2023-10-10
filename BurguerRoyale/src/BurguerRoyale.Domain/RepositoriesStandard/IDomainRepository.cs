using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurguerRoyale.Domain.RepositoriesStandard
{
    public interface IDomainRepository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
    {
    }
}
