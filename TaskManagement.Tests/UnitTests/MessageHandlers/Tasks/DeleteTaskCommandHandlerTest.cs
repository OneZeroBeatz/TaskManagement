using FluentValidation;
using FluentValidation.Results;
using Moq;
using TaskManagement.Application.MessageHandlers.Tasks;
using TaskManagement.Application.Messages.Tasks;
using TaskManagement.Application.Repositories;
using Task = System.Threading.Tasks.Task;

namespace TaskManagement.Tests.UnitTests.MessageHandlers.Tasks
{
    public class DeleteTaskCommandHandlerTests
    {
        private Mock<IValidator<DeleteTaskCommand>>? _validatorMock;
        private Mock<ITaskRepository>? _repositoryMock;

        private DeleteTaskCommandHandler? _handler;

        private DeleteTaskCommand? _command;


        [SetUp]
        public void Setup()
        {
            _validatorMock = new Mock<IValidator<DeleteTaskCommand>>();
            _repositoryMock = new Mock<ITaskRepository>();

            _command = new DeleteTaskCommand();

            _handler = new DeleteTaskCommandHandler(_validatorMock.Object,
                                                    _repositoryMock.Object);


            _validatorMock!.Setup(x => x.ValidateAsync(_command!, default))
                .ReturnsAsync(new ValidationResult());
        }

        [Test]
        public async Task Handle_CommandNotValid_ErrorResult()
        {
            var validationResult = new ValidationResult(new List<ValidationFailure> {
                    new ValidationFailure("Date", "Error1")
            });

            _validatorMock!.Setup(x => x.ValidateAsync(_command!, default))
                .ReturnsAsync(validationResult);

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
        public async Task Handle_CommandValid_EntityDeleted()
        {
            _command!.TaskId = 1;

            await _handler!.Handle(_command!, default);

            _repositoryMock!.Verify(x => x.DeleteAsync(1, default), Times.Once);
        }
    }
}