using System;
using System.Linq;
using HanoiTower.Domain.Commands.Handlers;
using HanoiTower.Domain.Commands.Inputs.Hanoi;
using HanoiTower.Domain.Entities;
using HanoiTower.Domain.Interfaces.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HanoiTower.Domain.Tests
{
    [TestClass]
    public class CreateHanoiTest
    {
        private Mock<IMoveRepository> _moveMockedRepository;
        private Mock<IHanoiRepository> _hanoiMockedRepository;
        private HanoiCommandsHandler _hanoiCommandsHandler;

        /// <summary>
        /// Mocking
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _moveMockedRepository = new Mock<IMoveRepository>();
            _hanoiMockedRepository = new Mock<IHanoiRepository>();
            _hanoiCommandsHandler = new HanoiCommandsHandler(_hanoiMockedRepository.Object, _moveMockedRepository.Object);
        }

        /// <summary>
        /// Validate if Hanoi Entity is correctly validating itself
        /// </summary>
        [TestMethod]
        public void GivenCreateHanoiCommandWithTotalDisksLessThanOneShouldReturnNull()
        {
            var createHanoi = new CreateHanoiCommand{ DiskCount = 1 };

            var result =_hanoiCommandsHandler.Handle(createHanoi);

            Assert.IsNull(result);
            Assert.AreEqual(_hanoiCommandsHandler.Notifications.First(x => x.Property == "TotalDisks").Message, "The Hanoi Tower needs at least 2 disks to be played");
        }


        /// <summary>
        /// Validate if CreateHanoiCommandsHandler is correctly creating a new Hanoi
        /// </summary>
        [TestMethod]
        public void GivenCreateHanoiCommandWithTotalDisksGreaterThanOneShouldReturnNewHanoiId()
        {
            _hanoiMockedRepository.Setup(x => x.NoTransactionAdd(It.IsAny<Hanoi>())).Returns(new Hanoi(3));
            var createHanoi = new CreateHanoiCommand { DiskCount = 3 };
            var result = _hanoiCommandsHandler.Handle(createHanoi);

            Assert.IsNotNull(result);
            Assert.IsFalse(string.IsNullOrEmpty(result.Id));
        }
    }
}
