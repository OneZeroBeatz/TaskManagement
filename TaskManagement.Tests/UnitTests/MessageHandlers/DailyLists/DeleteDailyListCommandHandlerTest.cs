using FluentValidation;
using FluentValidation.Results;
using Moq;
using TaskManagement.Application.MessageHandlers.DailyLists;
using TaskManagement.Application.Messages.DailyLists;
using TaskManagement.Application.Repositories;
using Task = System.Threading.Tasks.Task;

namespace TaskManagement.Tests.UnitTests.MessageHandlers.DailyLists
{
    public class DeleteDailyListCommandHandlerTests
    {
        private Mock<IValidator<DeleteDailyListCommand>>? _validatorMock;
        private Mock<IDailyListRepository>? _repositoryMock;

        private DeleteDailyListCommandHandler? _handler;

        private DeleteDailyListCommand? _command;


        [SetUp]
        public void Setup()
        {
            _validatorMock = new Mock<IValidator<DeleteDailyListCommand>>();
            _repositoryMock = new Mock<IDailyListRepository>();

            _command = new DeleteDailyListCommand();

            _handler = new DeleteDailyListCommandHandler(_validatorMock.Object,
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
        public async Task Handle_CommandValid_EntityDeleted()
        {
            _command!.DailyListId = 1;

            await _handler!.Handle(_command!, default);

            _repositoryMock!.Verify(x => x.DeleteAsync(1, default), Times.Once);
        }
    }
}