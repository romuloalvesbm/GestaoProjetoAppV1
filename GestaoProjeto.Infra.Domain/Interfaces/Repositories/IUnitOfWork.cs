using GestaoProjeto.Infra.Domain.Departamentos;
using GestaoProjeto.Infra.Domain.Funcionarios;
using GestaoProjeto.Infra.Domain.Participacoes;
using GestaoProjeto.Infra.Domain.Projetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Infra.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task BeginTransaction();
        Task Commit();
        Task Rollback();
        Task SaveChanges();

        IDepartamentoRepository DepartamentoRepository { get; }
        IFuncionarioRepository FuncionarioRepository { get; }
        IParticipacaoRepository ParticipacaoRepository { get; }
        IProjetoRepository ProjetoRepository { get; }

    }
}
