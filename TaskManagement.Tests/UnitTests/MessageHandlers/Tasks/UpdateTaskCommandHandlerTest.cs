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
    public class UpdateTasksCommandHandlerTests
    {
        private Mock<IValidator<UpdateTaskCommand>>? _validatorMock;
        private Mock<ITaskRepository>? _repositoryMock;
        private Mock<ITaskFactory>? _factoryMock;

        private UpdateTaskCommandHandler? _handler;

        private UpdateTaskCommand? _command;

        private Domain.Models.Task? _entity;

        [SetUp]
        public void Setup()
        {
            _validatorMock = new Mock<IValidator<UpdateTaskCommand>>();
            _repositoryMock = new Mock<ITaskRepository>();
            _factoryMock = new Mock<ITaskFactory>();

            _command = new UpdateTaskCommand();
            _entity = new Domain.Models.Task();

            _handler = new UpdateTaskCommandHandler(_validatorMock.Object,
                                                    _factoryMock.Object,
                                                    _repositoryMock.Object);

            _validatorMock!.Setup(x => x.ValidateAsync(_command!, default))
                .ReturnsAsync(new ValidationResult());

            _factoryMock!.Setup(x => x.CreateTaskAsync(_command!, default))
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
        public async Task Handle_CommandValid_EntityUpdated()
        {
            await _handler!.Handle(_command!, default);

            _repositoryMock!.Verify(x => x.UpdateAsync(_entity!, default), Times.Once);
        }
    }
}