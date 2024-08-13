using GestaoProjeto.Infra.Database.Context;
using GestaoProjeto.Infra.Domain.Funcionarios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Infra.Database.Repositories
{
    public class FuncionarioRepository : BaseRepository<Funcionario>, IFuncionarioRepository
    {
        private readonly DataContext _dataContext;

        public FuncionarioRepository(DataContext dataContext) : base(dataContext) => _dataContext = dataContext;

        public async Task<bool> NameExist(string? nome)
        {
            return await _dataContext.Funcionarios.AnyAsync(x => x.Nome == nome);
        }

        //Apenas teste com metodo "AsNoTracking"
        //public async override Task<List<Funcionario>>? GetAll()
        //{
        //    return await _dataContext.Funcionarios.Include(x => x.Supervisor)
        //                                          .Include(x => x.Funcionarios).AsNoTracking().ToListAsync();
        //}

        public override async Task<Funcionario?> GetById(Guid id)
        {
            return await _dataContext.Funcionarios.Include(x => x.Supervisor)
                                                  .Include(x => x.Funcionarios).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
