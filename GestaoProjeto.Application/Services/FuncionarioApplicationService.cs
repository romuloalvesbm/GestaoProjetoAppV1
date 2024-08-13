using AutoMapper;
using GestaoProjeto.Application.Extensions;
using GestaoProjeto.Application.Interfaces.Services;
using GestaoProjeto.Infra.Domain.Funcionarios;
using GestaoProjeto.Presentation.Contracts;
using GestaoProjeto.Presentation.Contracts.Dtos;
using GestaoProjeto.Presentation.Contracts.v1.Models.Funcionarios.Request;


namespace GestaoProjeto.Application.Services
{
    public class FuncionarioApplicationService : IFuncionarioApplicationService
    {
        private readonly IMapper _mapper;
        private readonly IFuncionarioRepository _funcionarioRepository;

        public FuncionarioApplicationService(IFuncionarioRepository funcionarioRepository, IMapper mapper)
        {
            _funcionarioRepository = funcionarioRepository;
            _mapper = mapper;
        }

        public async Task<Result> Create(FuncionarioCadastroRequest model)
        {
            if (!await _funcionarioRepository.NameExist(model.Nome))
            {
                var funcionario = _mapper.Map<Funcionario>(model);
                await _funcionarioRepository.Add(funcionario);
                await _funcionarioRepository.SaveChanges();

                return Result.Ok("Funcionário cadastrado com sucesso");
            }
            else
            {
                return Result.Fail("Funcionário já cadastrado");
            }
        }

        public async Task<Result> Delete(FuncionarioExclusaoRequest model)
        {
            var funcionario = await _funcionarioRepository.GetById(model.FuncionarioId);

            if (funcionario != null)
            {
                await _funcionarioRepository.Delete(funcionario);
                await _funcionarioRepository.SaveChanges();

                return Result.Ok("Funcionário excluído com sucesso");
            }
            else
            {
                return Result.Fail("Funcionário não encontrado");
            }

        }

        public async Task<Result> Update(FuncionarioEdicaoRequest model)
        {
            var funcionario = await _funcionarioRepository.GetById(model.FuncionarioId);

            if (funcionario != null)
            {
                funcionario.Update(model.FuncionarioId, model.Nome, model.SupervisorId);
                await _funcionarioRepository.Update(funcionario);
                await _funcionarioRepository.SaveChanges();

                return Result.Ok("Funcionário atualizado com sucesso");
            }
            else
            {
                return Result.Fail("Funcionário não encontrado");
            }
        }

        public async Task<Result<ICollection<FuncionarioDto>>> GetAll(FuncionarioGridRequest model)
        {
            var query = (await _funcionarioRepository.GetAll(x => string.IsNullOrEmpty(model.Nome) || (x.Nome != null && x.Nome.ToLower().StartsWith(model.Nome))))
                                          .AsQueryable()
                                           .LinqOrderBy(model.Sort, model.Order)
                                           .Skip(model.CurrentPage * model.PageSize)
                                           .Take(model.PageSize);

            ICollection<FuncionarioDto> dto = query.Select(x => new FuncionarioDto
            {
                FuncionarioId = x.Id,
                Nome = x.Nome,
                Supervisor = x.Supervisor == null ? null : new FuncionarioDto
                {
                    FuncionarioId = x.Supervisor.Id,
                    Nome = x.Supervisor.Nome
                },
                FuncionarioDtos = x.Funcionarios != null ? x.Funcionarios.Select(y => new FuncionarioDto
                {
                    FuncionarioId = y.Id,
                    Nome = y.Nome
                }).ToList() : new List<FuncionarioDto>()
            }).ToList();

            return Result.Ok(dto);

            //Exemplo com Automapper
            //return Result.Ok(_mapper.Map<ICollection<FuncionarioDto>>(query));
        }

        public async Task<Result<FuncionarioDto>> GetById(Guid id)
        {
            var funcionario = await _funcionarioRepository.GetById(id);

            if (funcionario == null)
            {
                return Result.Fail<FuncionarioDto>("Funcionário não encontrado.");
            }

            return Result.Ok(_mapper.Map<FuncionarioDto>(funcionario));
        }
    }
}
