using HealthMed.Domain.Entities;
using HealthMed.Web.Models;
using AutoMapper;

namespace HealthMed.Web.MappingProfile;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<MedicoViewModel, ProfissionalMedico>().ReverseMap();

        CreateMap<AgendaHorarioMedicoViewModel, AgendaHorarioMedico>().ReverseMap();
        CreateMap<Usuario, Usuario>();

        
        //CreateMap<PortfolioAtivo, PortfolioAtivo>()
        //    .ForMember(dest => dest.Ativo, opt => opt.Ignore()); 

    }
}
