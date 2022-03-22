using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VY.RebelsExam.Dtos.Domain.V1;
using VY.RebelsExam.Infrastructure.Contracts.Domain;

namespace VY.RebelsExam.Business.Contracts.Services
{
    public interface IRebelService
    {
        public Task<OperationResult<bool>> AddRebel(IEnumerable<RebelDto> rebels);
    }
}
