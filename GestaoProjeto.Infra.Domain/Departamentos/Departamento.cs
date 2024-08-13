using GestaoProjeto.Infra.Domain.Common;
using GestaoProjeto.Infra.Domain.Participacoes;
using GestaoProjeto.Infra.Domain.Projetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Infra.Domain.Departamentos
{
    public class Departamento : EntityBase
    {
        public string? Nome { get; private set; }
        public ICollection<Projeto>? Projetos { get; private set; }
        public ICollection<Participacao>? Participacoes { get; private set; }

        public Departamento()
        {

        }

        private Departamento(string? nome, ICollection<Projeto>? projetos, ICollection<Participacao>? participacoes)
        {
            Nome = nome;
            Projetos = projetos ?? new List<Projeto>();
            Participacoes = participacoes ?? new List<Participacao>();
        }

        public static Departamento Create(string? nome, ICollection<Projeto>? projetos = null, ICollection<Participacao>? participacoes = null)
        {
            return new Departamento(nome, projetos, participacoes);
        }

        public void Update(Guid id, string? nome, ICollection<Projeto>? projetos = null, ICollection<Participacao>? participacoes = null)
        {
            Id = id;
            Nome = nome;
            if (projetos != null) { Projetos = projetos; }
            if (participacoes != null) { Participacoes = participacoes; }
        }

    }
}
