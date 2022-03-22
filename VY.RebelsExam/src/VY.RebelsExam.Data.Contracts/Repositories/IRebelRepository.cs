using System.Collections.Generic;
using System.Threading.Tasks;
using VY.RebelsExam.Data.Contracts.Entities;
using VY.RebelsExam.Infrastructure.Contracts.Domain;

namespace VY.RebelsExam.Data.Contracts.Repositories
{
    public interface IRebelRepository
    {
        public bool ExistsRepository();
        public Task<OperationResult> Add(IEnumerable<Rebel> rebel);
        public Task<OperationResult<IEnumerable<Rebel>>> GetAll();
    }
}
