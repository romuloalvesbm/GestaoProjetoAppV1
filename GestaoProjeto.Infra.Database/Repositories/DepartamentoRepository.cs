using GestaoProjeto.Infra.Database.Context;
using GestaoProjeto.Infra.Domain.Departamentos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Infra.Database.Repositories
{
    public class DepartamentoRepository : BaseRepository<Departamento>, IDepartamentoRepository
    {
        private readonly DataContext _dataContext;

        public DepartamentoRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Departamento?> GetByNome(string nome)
        {
            return await _dataContext.Departamentos.FirstOrDefaultAsync(x => x.Nome != null && x.Nome.StartsWith(nome));
        }
    }
}
