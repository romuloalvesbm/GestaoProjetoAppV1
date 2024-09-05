using AutoMapper;
using GestaoProjeto.Infra.Domain.Funcionarios;
using GestaoProjeto.Presentation.Contracts.v1.Models.Funcionarios.Request;
using GestaoProjeto.Presentation.Contracts.v1.Models.Funcionarios.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Funcionarios

            //CreateMap<FuncionarioCadastroRequest, Funcionario>()
            // .ConvertUsing(src => Funcionario.Create(Guid.NewGuid(), src.Nome, src.SupervisorId, null, null));

            CreateMap<FuncionarioCadastroRequest, Funcionario>()
           .ConvertUsing<FuncionarioTypeConverter>();

            CreateMap<FuncionarioEdicaoRequest, Funcionario>()
                 .AfterMap((src, dest) => dest.Update(src.FuncionarioId, src.Nome, src.SupervisorId, null, null));

            CreateMap<FuncionarioExclusaoRequest, FuncionarioResponse>();

            CreateMap<Funcionario, FuncionarioResponse>()
                .ForMember(dest => dest.FuncionarioId, map => map.MapFrom(src => src.Id))
                .ForMember(dest => dest.Supervisor, map => map.MapFrom(src => src.Supervisor))
                .ForMember(dest => dest.FuncionarioDtos, map => map.MapFrom(src => src.Funcionarios))
                .ReverseMap();

            #endregion
        }

    }
    public class FuncionarioTypeConverter : ITypeConverter<FuncionarioCadastroRequest, Funcionario>
    {
        public Funcionario Convert(FuncionarioCadastroRequest source, Funcionario destination, ResolutionContext context)
        {
            var funcionarios = source.Subordinados?.Select(dto =>
                new Funcionario(Guid.NewGuid(), dto.Nome)).ToList()
                ?? new List<Funcionario>();

            return Funcionario.Create(Guid.NewGuid(), source.Nome, source.SupervisorId, funcionarios, null);
        }
    }
}
