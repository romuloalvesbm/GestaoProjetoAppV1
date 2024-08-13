using GestaoProjeto.Application.Interfaces.Services;
using GestaoProjeto.Presentation.Contracts.Dtos;
using GestaoProjeto.Presentation.Contracts;
using GestaoProjeto.Presentation.Contracts.v1.Models.Funcionarios.Request;
using GestaoProjeto.Presentation.Controllers.v1;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bogus;
using GestaoProjeto.Presentation.Contracts.v1.Common;
using GestaoProjeto.Infra.Domain.Funcionarios;


namespace GestaoProjeto.Tests.API
{
    public class FuncionarioEndpointTest
    {
        private readonly Mock<IFuncionarioApplicationService> _funcionarioApplicationServiceMock;
        private readonly FuncionarioController _controller;
        private readonly FuncionarioCadastroRequest _funcionarioCadastroRequestMock;
        private readonly FuncionarioEdicaoRequest _funcionarioEdicaoRequestMock;
        private readonly FuncionarioDto _funcionarioDtoMock;
        private readonly List<FuncionarioDto> _listFuncionarioDtoMock;
        private readonly Faker _faker = new Faker();

        public FuncionarioEndpointTest()
        {
            _funcionarioApplicationServiceMock = new Mock<IFuncionarioApplicationService>();
            _controller = new FuncionarioController(_funcionarioApplicationServiceMock.Object);

            _funcionarioCadastroRequestMock = new FuncionarioCadastroRequest
            {
                Nome = _faker.Person.FullName,
                SupervisorId = _faker.Random.Guid()
            };

            _funcionarioDtoMock = new FuncionarioDto
            {
                FuncionarioId = _faker.Random.Guid(),
                Nome = _faker.Person.FullName,
                SupervisorId = _faker.Random.Guid()
            };

            _listFuncionarioDtoMock = new Faker<FuncionarioDto>("pt_BR")
                              .RuleFor(x => x.Nome, f => f.Name.FullName())
                              .RuleFor(x => x.FuncionarioId, f => f.Random.Guid())
                              .Generate(2);

            _funcionarioEdicaoRequestMock = new FuncionarioEdicaoRequest
            {
                FuncionarioId = _faker.Random.Guid(),
                Nome = _faker.Person.FullName,
                SupervisorId = _faker.Random.Guid()
            };
            
        }

        [Fact]
        public async Task PostFuncionarioReturnsOk()
        {
            // Arrange
            var result = Result.Ok("Funcionário cadastrado com sucesso");

            // Act
            _funcionarioApplicationServiceMock.Setup(x => x.Create(_funcionarioCadastroRequestMock)).ReturnsAsync(result);

            var actionResult = await _controller.Post(_funcionarioCadastroRequestMock) as OkObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.Equal(StatusCodes.Status200OK, actionResult.StatusCode);

            var resultValue = actionResult.Value as Result;
            Assert.NotNull(resultValue);
            Assert.Equal(result.Message, resultValue.Message);
        }

        [Fact]
        public async Task PostFuncionarioReturnsBadRequest()
        {
            // Arrange
            var result = Result.Fail("Funcionário já cadastrado");

            _funcionarioApplicationServiceMock.Setup(service => service.Create(_funcionarioCadastroRequestMock)).ReturnsAsync(result);

            // Act
            var actionResult = await _controller.Post(_funcionarioCadastroRequestMock) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.Equal(StatusCodes.Status400BadRequest, actionResult.StatusCode);

            var resultValue = actionResult.Value as Result;
            Assert.NotNull(resultValue);
            Assert.Equal(result.Message, resultValue.Message);
        }


        [Fact]
        public async Task PostFuncionarioReturnsErroInternal()
        {
            // Arrange
            var exceptionMessage = "Erro interno";

            _funcionarioApplicationServiceMock.Setup(service => service.Create(_funcionarioCadastroRequestMock)).ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var actionResult = await _controller.Post(_funcionarioCadastroRequestMock) as ObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.Equal(StatusCodes.Status500InternalServerError, actionResult.StatusCode);

            var resultValue = actionResult.Value as Result;
            Assert.NotNull(resultValue);
            Assert.Equal(exceptionMessage, resultValue.Message);
        }

        [Fact]
        public async Task GetFuncionarioOk()
        {
            // Arrange
            var result = Result.Ok(_funcionarioDtoMock);

            _funcionarioApplicationServiceMock.Setup(service => service.GetById(_funcionarioDtoMock.FuncionarioId)).ReturnsAsync(result);

            // Act
            var actionResult = await _controller.GetFuncionario(_funcionarioDtoMock.FuncionarioId);

            var okResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

            var resultValue = okResult.Value as Result<FuncionarioDto>;
            Assert.NotNull(resultValue);
            Assert.True(resultValue.Success);
            Assert.Equal(_funcionarioDtoMock, resultValue.Value);
        }

        [Fact]
        public async Task GetFuncionarioBadRequest()
        {
            // Arrange
            var result = Result.Fail<FuncionarioDto>("Funcionário não encontrado.");

            _funcionarioApplicationServiceMock.Setup(service => service.GetById(_funcionarioDtoMock.FuncionarioId)).ReturnsAsync(result);

            // Act
            var actionResult = await _controller.GetFuncionario(_funcionarioDtoMock.FuncionarioId);

            var okResult = actionResult.Result as BadRequestObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status400BadRequest, okResult.StatusCode);

            var resultValue = okResult.Value as Result<FuncionarioDto>;
            Assert.NotNull(resultValue);
            Assert.False(resultValue.Success);
            Assert.Equal(result.Message, resultValue.Message);
        }

        [Fact]
        public async Task GetFuncionarioIntenalErro()
        {
            var exceptionMessage = "Erro interno";

            _funcionarioApplicationServiceMock.Setup(service => service.GetById(_funcionarioDtoMock.FuncionarioId)).ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var actionResult = await _controller.GetFuncionario(_funcionarioDtoMock.FuncionarioId);

            var okResult = actionResult.Result as ObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status500InternalServerError, okResult.StatusCode);

            var resultValue = okResult.Value as Result;
            Assert.NotNull(resultValue);
            Assert.False(resultValue.Success);
            Assert.Equal(exceptionMessage, resultValue.Message);
        }

        [Fact]
        public async Task GetAllFuncionarioOk()
        {
            var gridRequest = new FuncionarioGridRequest();
            // Arrange
            var result = Result.Ok<ICollection<FuncionarioDto>>(_listFuncionarioDtoMock);

            _funcionarioApplicationServiceMock.Setup(service => service.GetAll(It.IsAny<FuncionarioGridRequest>())).ReturnsAsync(result);

            // Act
            var actionResult = await _controller.GetAllFuncionario(gridRequest);

            var okResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

            var resultValue = okResult.Value as PagedList<FuncionarioDto>;
            Assert.NotNull(resultValue);
            Assert.Equal(resultValue.ResultSet.Count, _listFuncionarioDtoMock.Count);
            Assert.Equal(_listFuncionarioDtoMock, resultValue.ResultSet);
        }

        [Fact]
        public async Task GetAllFuncionarioOkVazio()
        {
            var gridRequest = new FuncionarioGridRequest();
            // Arrange
            var result = Result.Ok<ICollection<FuncionarioDto>>(new List<FuncionarioDto>());

            _funcionarioApplicationServiceMock.Setup(service => service.GetAll(It.IsAny<FuncionarioGridRequest>())).ReturnsAsync(result);

            // Act
            var actionResult = await _controller.GetAllFuncionario(gridRequest);

            var okResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

            var resultValue = okResult.Value as PagedList<FuncionarioDto>;
            Assert.NotNull(resultValue);
            Assert.Empty(resultValue.ResultSet);

        }

        [Fact]
        public async Task GetAllFuncionarioIntenalErro()
        {
            var gridRequest = new FuncionarioGridRequest();
            var exceptionMessage = "Erro interno";

            _funcionarioApplicationServiceMock.Setup(service => service.GetAll(It.IsAny<FuncionarioGridRequest>())).ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var actionResult = await _controller.GetAllFuncionario(gridRequest);

            var okResult = actionResult.Result as ObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status500InternalServerError, okResult.StatusCode);

            var resultValue = okResult.Value as Result;
            Assert.NotNull(resultValue);
            Assert.False(resultValue.Success);
            Assert.Equal(exceptionMessage, resultValue.Message);
        }

        [Fact]
        public async Task PutFuncionarioReturnsOk()
        {
            // Arrange
            var result = Result.Ok("Funcionário atualizado com sucesso");           
            // Act
            _funcionarioApplicationServiceMock.Setup(x => x.Update(_funcionarioEdicaoRequestMock)).ReturnsAsync(result);

            var actionResult = await _controller.Update(_funcionarioEdicaoRequestMock) as OkObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.Equal(StatusCodes.Status200OK, actionResult.StatusCode);

            var resultValue = actionResult.Value as Result;
            Assert.NotNull(resultValue);
            Assert.Equal(result.Message, resultValue.Message);
        }

        [Fact]
        public async Task PutFuncionarioReturnsBadRequest()
        {
            // Arrange
            var result = Result.Fail("Funcionário não encontrado");

            _funcionarioApplicationServiceMock.Setup(service => service.Update(_funcionarioEdicaoRequestMock)).ReturnsAsync(result);

            // Act
            var actionResult = await _controller.Update(_funcionarioEdicaoRequestMock) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.Equal(StatusCodes.Status400BadRequest, actionResult.StatusCode);

            var resultValue = actionResult.Value as Result;
            Assert.NotNull(resultValue);
            Assert.Equal(result.Message, resultValue.Message);
        }


        [Fact]
        public async Task PutFuncionarioReturnsErroInternal()
        {
            // Arrange
            var exceptionMessage = "Erro interno";

            _funcionarioApplicationServiceMock.Setup(service => service.Update(_funcionarioEdicaoRequestMock)).ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var actionResult = await _controller.Update(_funcionarioEdicaoRequestMock) as ObjectResult;

            // Assert
            Assert.NotNull(actionResult);
            Assert.Equal(StatusCodes.Status500InternalServerError, actionResult.StatusCode);

            var resultValue = actionResult.Value as Result;
            Assert.NotNull(resultValue);
            Assert.Equal(exceptionMessage, resultValue.Message);
        }

    }
}
