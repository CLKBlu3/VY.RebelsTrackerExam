using System.Collections.Generic;
using System.Linq;
using VY.RebelsExam.Business.Contracts.Validations;
using VY.RebelsExam.Dtos.Domain.V1;

namespace VY.RebelsExam.Business.Implementation.Validations
{
    public class RebelValidation : IValidation<RebelDto>
    {
        public bool Validate(IEnumerable<RebelDto> rebels)
        {
            //Returns false if validations misses!
            for(int i = 0; i < rebels.Count(); i++)
            {
                if(rebels.ElementAt(i) == null ||
                   rebels.ElementAt(i).Name == null || 
                   rebels.ElementAt(i).Planet == null ||
                   rebels.ElementAt(i).Date == System.DateTime.MinValue) //Unassigned Dates have DateTime.MinValue by default!
                {
                    return false;
                }
            }
            return true;
        }
    }
}
