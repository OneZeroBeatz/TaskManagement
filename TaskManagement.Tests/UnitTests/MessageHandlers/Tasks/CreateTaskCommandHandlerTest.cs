using FluentValidation;
using FluentValidation.Results;
using Moq;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.MessageHandlers.Tasks;
using TaskManagement.Application.Messages.Tasks;
using TaskManagement.Application.Repositories;
using Task = System.Threading.Tasks.Task;

namespace TaskManagement.Tests.UnitTests.MessageHandlers.Tasks
{
    public class CreateTaskCommandHandlerTests
    {
        private Mock<IValidator<CreateTaskCommand>>? _validatorMock;
        private Mock<ITaskRepository>? _repositoryMock;
        private Mock<ITaskFactory>? _factoryMock;

        private CreateTaskCommandHandler? _handler;

        private CreateTaskCommand? _command;

        private Domain.Models.Task? _entity;

        [SetUp]
        public void Setup()
        {
            _validatorMock = new Mock<IValidator<CreateTaskCommand>>();
            _repositoryMock = new Mock<ITaskRepository>();
            _factoryMock = new Mock<ITaskFactory>();

            _command = new CreateTaskCommand();
            _entity = new Domain.Models.Task();

            _handler = new CreateTaskCommandHandler(_validatorMock.Object,
                                                    _repositoryMock.Object,
                                                    _factoryMock.Object);

            _validatorMock!.Setup(x => x.ValidateAsync(_command!, default))
                .ReturnsAsync(new ValidationResult());

            _factoryMock!.Setup(x => x.CreateTaskAsync(_command!, default))
                .ReturnsAsync(_entity);

            _repositoryMock!.Setup(x => x.InsertAsync(_entity, default))
                .ReturnsAsync(_entity);
        }

        [Test]
        public async Task Handle_CommandNotValid_ErrorResult()
        {
            var validationResult = new ValidationResult(new List<ValidationFailure> {
                    new ValidationFailure("Date", "Error1")
            });

            _validatorMock!.Setup(x => x.ValidateAsync(_command!, default)).ReturnsAsync(validationResult);

            var result = await _handler!.Handle(_command!, default);

            Assert.IsFalse(result.Success);
        }

        [Test]
        public async Task Handle_CommandNotValid_ErrorMessage()
        {
            var validationResult = new ValidationResult(new List<ValidationFailure> {
                    new ValidationFailure("Date", "Error1")
            });

            _validatorMock!.Setup(x => x.ValidateAsync(_command!, default)).ReturnsAsync(validationResult);

            var result = await _handler!.Handle(_command!, default);

            Assert.AreEqual("Error1", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_CommandNotValidDueToMultipleErrors_ErrorMessage()
        {
            var validationResult = new ValidationResult(
                new List<ValidationFailure> {
                new ValidationFailure("Property1", "Error1"),
                new ValidationFailure("Property2", "Error2")
                });

            _validatorMock!.Setup(x => x.ValidateAsync(_command!, default))
                .ReturnsAsync(validationResult);

            var result = await _handler!.Handle(_command!, default);

            Assert.AreEqual("Error1\r\nError2", result.ErrorMessage);
        }

        [Test]
        public async Task Handle_CommandValid_ReturnsOkResult()
        {
            var result = await _handler!.Handle(_command!, default);

            Assert.IsTrue(result.Success);
        }

        [Test]
        public async Task Handle_CommandValid_ReturnsEntityId()
        {
            _entity!.Id = 10;

            var result = await _handler!.Handle(_command!, default);

            Assert.AreEqual(10, result.Value);
        }

        [Test]
        public async Task Handle_CommandValid_EntityInserted()
        {
            await _handler!.Handle(_command!, default);

            _repositoryMock!.Verify(x => x.InsertAsync(_entity!, default), Times.Once);
        }
    }
}