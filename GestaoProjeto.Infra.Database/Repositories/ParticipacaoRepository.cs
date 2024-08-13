using GestaoProjeto.Infra.Database.Context;
using GestaoProjeto.Infra.Domain.Participacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Infra.Database.Repositories
{
    public class ParticipacaoRepository : BaseRepository<Participacao>, IParticipacaoRepository
    {
        private readonly DataContext _dataContext;

        public ParticipacaoRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
