using GestaoProjeto.Infra.Domain.Common;
using GestaoProjeto.Infra.Domain.Departamentos;
using GestaoProjeto.Infra.Domain.Participacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Infra.Domain.Projetos
{
    public class Projeto : EntityBase
    {
        public string? Descricao { get; private set; }
        public Guid DepartamentoId { get; private set; }
        public Departamento? Departamento { get; private set; }
        public ICollection<Participacao>? Participacoes { get; private set; }

        public Projeto()
        {

        }

        public Projeto(string? descricao, Guid departamentoId, ICollection<Participacao>? participacoes)
        {
            Descricao = descricao;
            DepartamentoId = departamentoId;
            Participacoes = participacoes ?? new List<Participacao>();
        }

        public static Projeto Create(string? descricao, Guid departamentoId, ICollection<Participacao>? participacoes) =>
        new(descricao, departamentoId, participacoes);

        public void Update(Guid id, string? descricao, Guid departamentoId, ICollection<Participacao>? participacoes)
        {
            Id = id;
            Descricao = descricao;
            DepartamentoId = departamentoId;
            if (participacoes != null) { Participacoes = participacoes; }
        }

    }
}
