using GestaoProjeto.Infra.Domain.Common;
using GestaoProjeto.Infra.Domain.Departamentos;
using GestaoProjeto.Infra.Domain.Funcionarios;
using GestaoProjeto.Infra.Domain.Projetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Infra.Domain.Participacoes
{
    public class Participacao : EntityBase
    {
        public Guid DepartamentoId { get; private set; }
        public Guid ProjetoId { get; private set; }
        public Guid FuncionarioId { get; private set; }
        public Departamento? Departamento { get; private set; }
        public Projeto? Projeto { get; private set; }
        public Funcionario? Funcionario { get; private set; }

        public Participacao()
        {

        }

        private Participacao(Guid departamentoId, Guid projetoId, Guid funcionarioId)
        {
            DepartamentoId = departamentoId;
            ProjetoId = projetoId;
            FuncionarioId = funcionarioId;
        }

        public static Participacao Create(Guid departamentoId, Guid projetoId, Guid funcionarioId)
        {
            return new Participacao(departamentoId, projetoId, funcionarioId);
        }

        public void Update(Guid id, Guid departamentoId, Guid projetoId, Guid funcionarioId)
        {
            Id = id;
            DepartamentoId = departamentoId;
            ProjetoId = projetoId;
            FuncionarioId = funcionarioId;
        }
    }
}
