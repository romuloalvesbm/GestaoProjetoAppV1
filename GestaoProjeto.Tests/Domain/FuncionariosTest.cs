using Bogus;
using Bogus.DataSets;
using GestaoProjeto.Infra.Domain.Funcionarios;
using ExpectedObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoProjeto.Tests.Domain
{
    public class FuncionariosTest
    {

        [Fact]
        public void CriarFuncionarioSupervisor()
        {
            var faker = new Faker();

            var funcionarioEsperado = new
            {
                Id = faker.Random.Guid(),
                Nome = faker.Person.FullName,
            };

            var funcionario = Funcionario.Create(funcionarioEsperado.Id, funcionarioEsperado.Nome, null, null, null);

            Assert.Equal(funcionarioEsperado.Id, funcionario.Id);
            Assert.Equal(funcionarioEsperado.Nome, funcionario.Nome);
        }

        [Fact]
        public void NaoCriarFuncionarioComNomeEmBranco()
        {
            var faker = new Faker();

            var funcionarioEsperado = new
            {
                Id = faker.Random.Guid(),
                Nome = string.Empty,
            };

            var message = Assert.Throws<Exception>(() => Funcionario.Create(funcionarioEsperado.Id, funcionarioEsperado.Nome, null, null, null)).Message;

            Assert.Equal("Nome não encontrado.", message);
        }

        [Fact]
        public void CriarFuncionarioSubordinado()
        {
            var faker = new Faker();

            var funcionarioLideradoEsperado = new
            {
                Id = faker.Random.Guid(),
                Nome = faker.Person.FullName,
                SupervidorId = faker.Random.Guid(),
            };

            var funcionario = Funcionario.Create(funcionarioLideradoEsperado.Id, funcionarioLideradoEsperado.Nome, funcionarioLideradoEsperado.SupervidorId, null, null);

            Assert.Equal(funcionarioLideradoEsperado.Id, funcionario.Id);
            Assert.Equal(funcionarioLideradoEsperado.Nome, funcionario.Nome);
            Assert.Equal(funcionarioLideradoEsperado.SupervidorId, funcionario.SupervisorId);
        }

        [Fact]
        public void CriarFuncinarioSupervisorComSubordinados()
        {
            var faker = new Faker();

            var funcionarioEsperado = new
            {
                Id = faker.Random.Guid(),
                Nome = faker.Person.FullName,
            };

            var listLisderados = new List<Funcionario>
            {
               new (faker.Random.Guid(), faker.Person.FullName),
               new (faker.Random.Guid(), faker.Person.FullName)
            };

            var funcionario = Funcionario.Create(funcionarioEsperado.Id, funcionarioEsperado.Nome, null, listLisderados, null);

            Assert.Equal(funcionarioEsperado.Id, funcionario.Id);
            Assert.Equal(funcionarioEsperado.Nome, funcionario.Nome);
            Assert.Equal(listLisderados, funcionario.Funcionarios);
        }

        [Fact]
        public void AtualizarFuncionario()
        {
            var faker = new Faker();

            var funcionarioBanco = new
            {
                Id = faker.Random.Guid(),
                Nome = faker.Person.FullName,
            };

            var funcionarioNew = new
            {
                Nome = faker.Person.FullName,
                SupervisorId = faker.Random.Guid(),
            };

            var funcionario = Funcionario.Create(funcionarioBanco.Id, funcionarioBanco.Nome, null, null, null);

            funcionario.Update(funcionarioBanco.Id, funcionarioNew.Nome, funcionarioNew.SupervisorId, null, null);

            Assert.Equal(funcionarioBanco.Id, funcionario.Id);
            Assert.Equal(funcionarioNew.Nome, funcionario.Nome);
            Assert.Equal(funcionarioNew.SupervisorId, funcionario.SupervisorId);
        }

        [Fact]
        public void NaoAtualizarFuncionarioComNomeEmBranco()
        {
            var faker = new Faker();

            var funcionarioBanco = new
            {
                Id = faker.Random.Guid(),
                Nome = faker.Person.FullName,
            };

            var funcionarioNew = new
            {
                Nome = string.Empty,
                SupervisorId = faker.Random.Guid(),
            };

            var funcionario = Funcionario.Create(funcionarioBanco.Id, funcionarioBanco.Nome, null, null, null);

            var message = Assert.Throws<Exception>(() => funcionario.Update(funcionarioBanco.Id, funcionarioNew.Nome, funcionarioNew.SupervisorId, null, null)).Message;

            Assert.Equal("Nome não encontrado.", message);
        }

    }
}

