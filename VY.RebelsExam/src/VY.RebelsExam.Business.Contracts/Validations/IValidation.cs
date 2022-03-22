using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VY.RebelsExam.Business.Contracts.Validations
{
    public interface IValidation<T> where T : class
    {
        public bool Validate(IEnumerable<T> Entity);
    }
}
