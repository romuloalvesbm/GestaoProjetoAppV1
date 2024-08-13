﻿using GestaoProjeto.Infra.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Infra.Domain.Departamentos
{
    public interface IDepartamentoRepository : IBaseRepository<Departamento>
    {
        Task<Departamento?> GetByNome(string nome);
    }
}
