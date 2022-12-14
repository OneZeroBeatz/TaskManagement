using FluentValidation;
using FluentValidation.Results;
using Moq;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.MessageHandlers.DailyLists;
using TaskManagement.Application.Messages.DailyLists;
using TaskManagement.Application.Repositories;
using TaskManagement.Domain.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManagement.Tests.UnitTests.MessageHandlers.DailyLists
{
    public class UpdateDailyListCommandHandlerTests
    {
        private Mock<IValidator<UpdateDailyListCommand>>? _validatorMock;
        private Mock<IDailyListRepository>? _repositoryMock;
        private Mock<IDailyListFactory>? _factoryMock;

        private UpdateDailyListCommandHandler? _handler;

        private UpdateDailyListCommand? _command;

        private DailyList? _entity;

        [SetUp]
        public void Setup()
        {
            _validatorMock = new Mock<IValidator<UpdateDailyListCommand>>();
            _repositoryMock = new Mock<IDailyListRepository>();
            _factoryMock = new Mock<IDailyListFactory>();

            _command = new UpdateDailyListCommand();
            _entity = new DailyList();

            _handler = new UpdateDailyListCommandHandler(_validatorMock.Object,
                                                        _repositoryMock.Object,
                                                        _factoryMock.Object);

            _validatorMock!.Setup(x => x.ValidateAsync(_command!, default))
                .ReturnsAsync(new ValidationResult());

            _factoryMock!.Setup(x => x.CreateDailyList(_command!))
                .Returns(_entity);
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