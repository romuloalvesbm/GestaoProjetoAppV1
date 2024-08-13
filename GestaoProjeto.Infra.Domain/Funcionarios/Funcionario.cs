using GestaoProjeto.Infra.Domain.Common;
using GestaoProjeto.Infra.Domain.Participacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Infra.Domain.Funcionarios
{
    public class Funcionario : EntityBase
    {
        public string? Nome { get; private set; }
        public Guid? SupervisorId { get; private set; }
        public Funcionario? Supervisor { get; private set; }
        public ICollection<Funcionario>? Funcionarios { get; private set; }
        public ICollection<Participacao>? Participacoes { get; set; }

        public Funcionario()
        {

        }

        public Funcionario(Guid idFuncionario, string? nome)
        {
            Id = idFuncionario;
            Nome = nome;

            Validate();
        }

        private Funcionario(Guid id, string? nome, Guid? supervisorId, ICollection<Funcionario>? funcionarios, ICollection<Participacao>? participacoes)
        {
            Id = id;
            Nome = nome;
            SupervisorId = supervisorId;
            Funcionarios = funcionarios ?? new List<Funcionario>();
            Participacoes = participacoes ?? new List<Participacao>();

            Validate();
        }

        public static Funcionario Create(Guid id, string? nome, Guid? supervisorId, ICollection<Funcionario>? funcionarios = null, ICollection<Participacao>? participacoes = null)
        {
            return new Funcionario(id, nome, supervisorId, funcionarios, participacoes);
        }

        public void Update(Guid id, string? nome, Guid? supervisorId = null,
                           ICollection<Funcionario>? funcionarios = null, ICollection<Participacao>? participacoes = null)
        {
            Id = id;
            Nome = nome;
            SupervisorId = supervisorId;
            if (funcionarios != null) { Funcionarios = funcionarios; }
            if (participacoes != null) { Participacoes = participacoes; }

            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(Nome))
            {
                throw new Exception("Nome não encontrado.");
            }
        }

    }
}
