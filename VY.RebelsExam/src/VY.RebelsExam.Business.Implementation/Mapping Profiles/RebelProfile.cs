using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VY.RebelsExam.Data.Contracts.Entities;
using VY.RebelsExam.Dtos.Domain.V1;

namespace VY.RebelsExam.Business.Implementation.Mapping_Profiles
{
    public class RebelProfile : Profile
    {
        public RebelProfile()
        {
            CreateMap<Rebel, RebelDto>()
                .ForMember(x => x.Name, opt => opt.MapFrom(y => y.Name))
                .ForMember(x => x.Planet, opt => opt.MapFrom(y => y.Planet))
                .ForMember(x => x.Date, opt => opt.MapFrom(y => y.Date))
                .ReverseMap();
        }
    }
}
