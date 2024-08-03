using HealthMed.Domain.Entities;
using HealthMed.Web.Models;
using AutoMapper;

namespace HealthMed.Web.MappingProfile;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<AtivoViewModel, Ativo>().ReverseMap();

        CreateMap<AgendaHorarioMedicoViewModel, AgendaHorarioMedico>().ReverseMap();
        CreateMap<MedicoViewModel,ProfissionalMedico>().ReverseMap();

        CreateMap<UsuarioPacienteViewModel, Usuario>().ReverseMap();
        CreateMap<UsuarioProfissionalViewModel, Usuario>().ReverseMap();
        CreateMap<Usuario, Usuario>();
       
        CreateMap<RespostaBrapiViewModel, RespostaBrapi>().ReverseMap();

        CreateMap<PortfolioViewModel, Portfolio>().ReverseMap();
        CreateMap<Portfolio, Portfolio>();

        CreateMap<PortfolioAtivoViewModel, PortfolioAtivo>().ReverseMap();
        CreateMap<PortfolioAtivo, PortfolioAtivo>()
            .ForMember(dest => dest.Ativo, opt => opt.Ignore()); 

        CreateMap<TransacaoViewModel,  Transacao>().ReverseMap();
        CreateMap<Transacao, Transacao>();

    }
}
