using GestaoProjeto.Infra.Database.Context;
using GestaoProjeto.Infra.Domain.Departamentos;
using GestaoProjeto.Infra.Domain.Funcionarios;
using GestaoProjeto.Infra.Domain.Interfaces.Repositories;
using GestaoProjeto.Infra.Domain.Participacoes;
using GestaoProjeto.Infra.Domain.Projetos;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Infra.Database.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        //atributos
        private readonly DataContext context;
        private IDbContextTransaction? transaction;

        public UnitOfWork(DataContext context)
        {
            this.context = context;
        }

        public async Task BeginTransaction()
        {
            transaction = await context.Database.BeginTransactionAsync();
        }   

        public async Task Commit()
        {
            if(transaction != null)
            await transaction.CommitAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await context.DisposeAsync();
        }

        public async Task Rollback()
        {
            if (transaction != null)
                await transaction.RollbackAsync();
        }
        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public IDepartamentoRepository DepartamentoRepository => new DepartamentoRepository(context);

        public IFuncionarioRepository FuncionarioRepository => new FuncionarioRepository(context);

        public IParticipacaoRepository ParticipacaoRepository => new ParticipacaoRepository(context);

        public IProjetoRepository ProjetoRepository => new ProjetoRepository(context);
    }
}
