using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using VY.RebelsExam.Business.Contracts.Services;
using VY.RebelsExam.Business.Contracts.Validations;
using VY.RebelsExam.Data.Contracts.Entities;
using VY.RebelsExam.Data.Contracts.Repositories;
using VY.RebelsExam.Dtos.Domain.V1;
using VY.RebelsExam.Infrastructure.Contracts.Domain;

[assembly: InternalsVisibleTo("VY.RebelsExam.Business.UnitTesting")]
namespace VY.RebelsExam.Business.Implementation.Services
{
    public class RebelService : IRebelService
    {
        private readonly IRebelRepository _rebelRepository;
        private readonly IValidation<RebelDto> _validations;
        private readonly IMapper _mapper;
        private readonly ILogger<RebelService> _logger;

        public RebelService(IMapper mapper,
                            ILogger<RebelService> logger,
                            IRebelRepository rebelRepository,
                            IValidation<RebelDto> validation)
        {
            _mapper = mapper;
            _logger = logger;
            _rebelRepository = rebelRepository;
            _validations = validation;
        }

        public async Task<OperationResult<bool>> AddRebel(IEnumerable<RebelDto> rebels)
        {
            OperationResult<bool> res = new OperationResult<bool>();
            try
            {
                if (!_validations.Validate(rebels))
                {
                    res.AddError(101, "The given rebel(s) has(ve) invalid field(s)!");
                    res.SetResult(false);
                }
                else
                {
                    IEnumerable<Rebel> rebelEntities = _mapper.Map<IEnumerable<RebelDto>, IEnumerable<Rebel>>(rebels);
                    var registeredRebels = await _rebelRepository.GetAll();
                    if (registeredRebels.HasErrors())
                    {
                        res.AddErrors(registeredRebels.Errors);
                        return res;
                    }
                    IEnumerable<Rebel> updatedRebels = UpdateRebels(registeredRebels.Result, rebelEntities);
                    OperationResult aux = await _rebelRepository.Add(updatedRebels);
                    if (aux.HasErrors())
                    {
                        _logger.LogError("Couldn't add the given rebel(s) to the repository.");
                        res.AddErrors(aux.Errors);
                    }
                    else
                    {
                        _logger.LogInformation("A Rebel (or more) were added to the repository!");
                        res.SetResult(true);
                    }
                }
                
            }
            catch(Exception ex)
            {
                res.AddException(ex);
                res.SetResult(false);
            }
            return res;
        }

        internal List<Rebel> UpdateRebels(IEnumerable<Rebel> ogList, IEnumerable<Rebel> newRebels)
        {
            _logger.LogInformation("Will update existing rebels (if any)");
            List<Rebel> toReturn = ogList.ToList();
            for(int i = 0; i < newRebels.Count(); ++i)
            {
                Rebel rebel = toReturn.Where(x => 
                                       x.Name.Equals(newRebels.ElementAt(i).Name))
                                       .FirstOrDefault();
                if(rebel != null)
                {
                    //Update existing rebel
                    rebel.Name = newRebels.ElementAt(i).Name;
                    rebel.Date = newRebels.ElementAt(i).Date;
                    rebel.Planet = newRebels.ElementAt(i).Planet;
                }
                else
                {
                    //Add non-registered rebel to the list!
                    toReturn.Add(newRebels.ElementAt(i));
                }
            }
            return toReturn;
        }
    }
}
