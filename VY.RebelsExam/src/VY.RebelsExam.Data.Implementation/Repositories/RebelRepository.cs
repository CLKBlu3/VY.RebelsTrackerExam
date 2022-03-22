using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using VY.RebelsExam.Data.Contracts.Entities;
using VY.RebelsExam.Data.Contracts.Repositories;
using VY.RebelsExam.Infrastructure.Contracts.Domain;

namespace VY.RebelsExam.Data.Implementation.Repositories
{
    public class RebelRepository : IRebelRepository
    {
        private readonly string _path;

        public RebelRepository(string path)
        {
            _path = path;
        }

        public bool ExistsRepository()
        {
            return File.Exists(_path);
        }

        public async Task<OperationResult> Add(IEnumerable<Rebel> rebel)
        {
            OperationResult res = new OperationResult();
            try
            {
                var toAdd = JsonSerializer.Serialize(rebel);
                await File.WriteAllTextAsync(_path, toAdd); //OVERWRITES content with updated values
            }
            catch(Exception ex)
            {
               res.AddException(ex);
            }
            return res;
        }

        public async Task<OperationResult<IEnumerable<Rebel>>> GetAll()
        {
            OperationResult<IEnumerable<Rebel>> res = new OperationResult<IEnumerable<Rebel>>();
            if (!ExistsRepository())
            {
                res.SetResult(new List<Rebel>());
            }
            else
            {
                var toReturn = JsonSerializer.Deserialize<IEnumerable<Rebel>>(
                                                        await File.ReadAllTextAsync(_path)
                                                        );
                res.SetResult(toReturn);
            }
            return res;
        }
    }
}
