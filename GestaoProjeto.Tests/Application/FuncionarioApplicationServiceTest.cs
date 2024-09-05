using AutoMapper;
using Bogus;
using GestaoProjeto.Application.Services;
using GestaoProjeto.Infra.Domain.Funcionarios;
using GestaoProjeto.Presentation.Contracts;
using GestaoProjeto.Presentation.Contracts.v1.Models.Funcionarios.Request;
using Moq;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus.DataSets;
using System.Collections.ObjectModel;
using GestaoProjeto.Presentation.Contracts.v1.Models.Funcionarios.Response;

namespace GestaoProjeto.Tests.Application
{
    public class FuncionarioApplicationServiceTest
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IFuncionarioRepository> _funcionarioRepositoryMock;
        private readonly FuncionarioApplicationService _funcionarioApplicationService;
        private readonly FuncionarioCadastroRequest _funcionarioCadastroRequest;
        private readonly FuncionarioCadastroRequest _funcionarioSupervisorCadastroRequest;
        private readonly List<Funcionario> _listSubordinados;
        private readonly FuncionarioExclusaoRequest _funcionarioExclusaoRequest;
        private readonly FuncionarioEdicaoRequest _funcionarioEdicaoRequest;
        private readonly Funcionario _funcionario;
        private readonly FuncionarioResponse _funcionarioDto;
        private readonly List<FuncionarioResponse> _listFuncionarioDto;
        private readonly List<Funcionario> _listFuncionario;
        private readonly Faker _faker = new Faker();

        public FuncionarioApplicationServiceTest()
        {
            _funcionarioRepositoryMock = new Mock<IFuncionarioRepository>();
            _mapperMock = new Mock<IMapper>();
            _funcionarioApplicationService = new FuncionarioApplicationService(_funcionarioRepositoryMock.Object, _mapperMock.Object);                     

            _funcionarioCadastroRequest = new FuncionarioCadastroRequest
            {
                Nome = _faker.Person.FullName
            };

            _funcionarioSupervisorCadastroRequest = new FuncionarioCadastroRequest
            {
                Nome = _faker.Person.FullName,
                SupervisorId = _faker.Random.Guid()
            };

            _funcionarioEdicaoRequest = new FuncionarioEdicaoRequest
            {
                FuncionarioId = _faker.Random.Guid(),
                Nome = _faker.Person.FullName,
                SupervisorId = _faker.Random.Guid()
            };

            _funcionarioExclusaoRequest = new FuncionarioExclusaoRequest
            {
                FuncionarioId = _faker.Random.Guid()
            };

            _listSubordinados = new Faker<Funcionario>("pt_BR")
                                .RuleFor(x => x.Id, f => f.Random.Guid())
                                .RuleFor(x => x.Nome, f => f.Name.FullName())                               
                                .Generate(2);            

            _funcionario = Funcionario.Create(Guid.NewGuid(), _funcionarioCadastroRequest.Nome, null, null, null);           

            _listFuncionario = new Faker<Funcionario>("pt_BR")
                                .RuleFor(x => x.Nome, f => f.Name.FullName())
                                .RuleFor(x => x.Id, f => f.Random.Guid())
                                .Generate(2);           

            _listFuncionarioDto = _listFuncionario.Select(x => new FuncionarioResponse
            {
                FuncionarioId = x.Id,
                Nome = x.Nome
            }).ToList();

            _funcionarioDto = _listFuncionarioDto.FirstOrDefault() ?? new FuncionarioResponse();
        }


        [Fact]
        public async Task CadastrarFuncionarioSupervisor()
        {
            _mapperMock.Setup(mapper => mapper.Map<Funcionario>(_funcionarioCadastroRequest)).Returns(_funcionario);         

            // Configuração do mock para capturar o valor passado e retornar false
            _funcionarioRepositoryMock.Setup(x => x.NameExist(It.IsAny<string>())).ReturnsAsync(false);
            _funcionarioRepositoryMock.Setup(x => x.Add(_funcionario));
            _funcionarioRepositoryMock.Setup(x => x.SaveChanges());

            var result = await _funcionarioApplicationService.Create(_funcionarioCadastroRequest);

            _funcionarioRepositoryMock.Verify(x => x.Add(It.Is<Funcionario>(x => x.Nome == _funcionarioCadastroRequest.Nome)), Times.Once());           
            Assert.Equal("Funcionário cadastrado com sucesso", result.Message);
        }


        [Fact]
        public async Task NaoCadastrarFuncionario()
        {
            _mapperMock.Setup(mapper => mapper.Map<Funcionario>(_funcionarioCadastroRequest)).Returns(_funcionario);
            _funcionarioRepositoryMock.Setup(x => x.NameExist(It.IsAny<string>())).ReturnsAsync(true);
            _funcionarioRepositoryMock.Setup(x => x.Add(_funcionario));
            _funcionarioRepositoryMock.Setup(x => x.SaveChanges());

            var result = await _funcionarioApplicationService.Create(_funcionarioCadastroRequest);

            _funcionarioRepositoryMock.Verify(r => r.Add(It.IsAny<Funcionario>()), Times.Never);
            _funcionarioRepositoryMock.Verify(r => r.SaveChanges(), Times.Never);
            Assert.Equal("Funcionário já cadastrado", result.Message);
        }

        [Fact]
        public async Task CadastrarFuncionarioSubordinado()
        {
            var funcionario = Funcionario.Create(Guid.NewGuid(), _funcionarioSupervisorCadastroRequest.Nome, _funcionarioSupervisorCadastroRequest.SupervisorId, null, null);

            _mapperMock.Setup(mapper => mapper.Map<Funcionario>(It.IsAny<FuncionarioCadastroRequest>())).Returns(funcionario);
            _funcionarioRepositoryMock.Setup(x => x.NameExist(It.IsAny<string>())).ReturnsAsync(false);
            _funcionarioRepositoryMock.Setup(x => x.Add(funcionario));
            _funcionarioRepositoryMock.Setup(x => x.SaveChanges());

            var result = await _funcionarioApplicationService.Create(_funcionarioSupervisorCadastroRequest);

            //_funcionarioRepositoryMock.Verify(r => r.SaveChanges(), Times.Once);
            _funcionarioRepositoryMock.Verify(x => x.Add(It.Is<Funcionario>(x => x.Nome == _funcionarioSupervisorCadastroRequest.Nome && x.SupervisorId == _funcionarioSupervisorCadastroRequest.SupervisorId )), Times.Once());
            Assert.Equal("Funcionário cadastrado com sucesso", result.Message);
        }
        

        [Fact]
        public async Task CadastrarFuncionarioSupervisorComSubordinado()
        {
            var funcionario = Funcionario.Create(Guid.NewGuid(), _funcionarioCadastroRequest.Nome, null, _listSubordinados, null);

            _mapperMock.Setup(mapper => mapper.Map<Funcionario>(It.IsAny<FuncionarioCadastroRequest>())).Returns(funcionario);
            _funcionarioRepositoryMock.Setup(x => x.NameExist(It.IsAny<string>())).ReturnsAsync(false);
            _funcionarioRepositoryMock.Setup(x => x.Add(funcionario));
            _funcionarioRepositoryMock.Setup(x => x.SaveChanges());

            var result = await _funcionarioApplicationService.Create(_funcionarioCadastroRequest);

            _funcionarioRepositoryMock.Verify(x => x.Add(It.Is<Funcionario>(x => x.Id == funcionario.Id && x.Nome == funcionario.Nome)), Times.Once); //Mas uma validação exemplo
            _funcionarioRepositoryMock.Verify(r => r.SaveChanges(), Times.Once);
            Assert.Equal("Funcionário cadastrado com sucesso", result.Message);
        }       

        [Fact]
        public async Task DeletarFuncionario()
        {
            var funcionarioSalvo = Funcionario.Create(_funcionarioExclusaoRequest.FuncionarioId, _funcionarioCadastroRequest.Nome, null, _listSubordinados, null);

            _funcionarioRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(funcionarioSalvo);
            _funcionarioRepositoryMock.Setup(x => x.Delete(funcionarioSalvo));
            _funcionarioRepositoryMock.Setup(x => x.SaveChanges());

            var result = await _funcionarioApplicationService.Delete(_funcionarioExclusaoRequest);

            _funcionarioRepositoryMock.Verify(x => x.Delete(It.Is<Funcionario>(x => x.Id == funcionarioSalvo.Id && x.Nome == funcionarioSalvo.Nome)), Times.Once); //Mas uma validação exemplo
            _funcionarioRepositoryMock.Verify(r => r.SaveChanges(), Times.Once);
            Assert.Equal("Funcionário excluído com sucesso", result.Message);
        }


        [Fact]
        public async Task AtualizarFuncionario()
        {
            var funcionarioSalvo = Funcionario.Create(_funcionarioEdicaoRequest.FuncionarioId, _funcionarioEdicaoRequest.Nome, _funcionarioEdicaoRequest.FuncionarioId, null, null);

            _funcionarioRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(funcionarioSalvo);
            _funcionarioRepositoryMock.Setup(x => x.Update(funcionarioSalvo));
            _funcionarioRepositoryMock.Setup(x => x.SaveChanges());

            var result = await _funcionarioApplicationService.Update(_funcionarioEdicaoRequest);

            _funcionarioRepositoryMock.Verify(x => x.Update(It.Is<Funcionario>(x => x.Id == funcionarioSalvo.Id && x.Nome == funcionarioSalvo.Nome)), Times.Once); //Mas uma validação exemplo
            _funcionarioRepositoryMock.Verify(r => r.SaveChanges(), Times.Once);
            Assert.Equal("Funcionário atualizado com sucesso", result.Message);
        }


        [Fact]
        public async Task DeletarFuncionarioRetornoNulo()
        {
            Funcionario? funcionario = null;           

            _funcionarioRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(funcionario);

            var result = await _funcionarioApplicationService.Delete(_funcionarioExclusaoRequest);

            _funcionarioRepositoryMock.Verify(r => r.SaveChanges(), Times.Never);
            Assert.Equal("Funcionário não encontrado", result.Message);
        }

        [Fact]
        public async Task ObterFuncionarioPorId()
        {
            var funcionarioId = _funcionario.Id;

            _funcionarioRepositoryMock.Setup(x => x.GetById(funcionarioId)).ReturnsAsync(_funcionario);
            _mapperMock.Setup(mapper => mapper.Map<FuncionarioResponse>(_funcionario)).Returns(_funcionarioDto);

            var result = await _funcionarioApplicationService.GetById(funcionarioId);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(_funcionarioDto, result.Value);
        }

        [Fact]
        public async Task ObterFuncionarioPorIdReturnNulo()
        {
            var funcionarioId = _funcionario.Id;                      
            
            var result = await _funcionarioApplicationService.GetById(funcionarioId);

            // Assert
            Assert.Equal("Funcionário não encontrado.", result.Message);
            Assert.Null(result.Value);
        }


        [Fact]
        public async Task ObterTodosFuncionario()
        {
            var funcionarioGrid = new FuncionarioGridRequest();            

            _funcionarioRepositoryMock.Setup(repo => repo.GetAll(It.IsAny<Expression<Func<Funcionario, bool>>>())).ReturnsAsync(_listFuncionario);           
            _mapperMock.Setup(mapper => mapper.Map<ICollection<FuncionarioResponse>>(It.IsAny<IQueryable<Funcionario>>())).Returns(_listFuncionarioDto);

            var result = await _funcionarioApplicationService.GetAll(funcionarioGrid);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(2, result.Value.Count);
            _funcionarioRepositoryMock.Verify(repo => repo.GetAll(It.IsAny<Expression<Func<Funcionario, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task ObterTodosFuncionarioReturnNulo()
        {
            var funcionarioGrid = new FuncionarioGridRequest();
            funcionarioGrid.Nome = _faker.Person.FullName;
            _funcionarioRepositoryMock.Setup(repo => repo.GetAll(It.IsAny<Expression<Func<Funcionario, bool>>>())).ReturnsAsync(new List<Funcionario>());
            _mapperMock.Setup(mapper => mapper.Map<ICollection<FuncionarioResponse>>(It.IsAny<IQueryable<Funcionario>>())).Returns(new List<FuncionarioResponse>());

            var result = await _funcionarioApplicationService.GetAll(funcionarioGrid);

            // Assert
            Assert.True(result.Success);
            Assert.Empty(result.Value);
            _funcionarioRepositoryMock.Verify(repo => repo.GetAll(It.IsAny<Expression<Func<Funcionario, bool>>>()), Times.Once);
        }
    }
}
