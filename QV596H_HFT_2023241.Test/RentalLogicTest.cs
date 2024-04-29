using Moq;
using NUnit.Framework;
using QV596H_HFT_2023241.Logic;
using QV596H_HFT_2023241.Models;
using QV596H_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QV596H_HFT_2023241.Test
{
    [TestFixture]
    public class RentalLogicTest
    {
        RentalLogic logic;
        Mock<IRepository<Rental>> mockRentalRepo;

        [SetUp]
        public void Init()
        {
            mockRentalRepo = new Mock<IRepository<Rental>>();
            mockRentalRepo.Setup(m => m.ReadAll()).Returns(new List<Rental>()
            {
                new Rental { RentalId = 1, RentalDate = DateTime.Now.AddDays(-1) },
                new Rental { RentalId = 2, RentalDate = DateTime.Now.AddDays(1) },
                new Rental { RentalId = 3, RentalDate = DateTime.Now.AddDays(2) },
            }.AsQueryable());
            logic = new RentalLogic(mockRentalRepo.Object);
        }

        [Test]
        public void IsThereAnOngoingRentalTest()
        {
            bool hasOngoingRental = logic.IsThereAnOngoingRental();
            Assert.IsTrue(hasOngoingRental);
        }

        [Test]
        public void IsThereNoOngoingRentalTest()
        {
            mockRentalRepo.Setup(m => m.ReadAll()).Returns(new List<Rental>()
            {
                new Rental { RentalId = 1, RentalDate = DateTime.Now.AddDays(-2) },
                new Rental { RentalId = 2, RentalDate = DateTime.Now.AddDays(-3) },
            }.AsQueryable());

            bool hasOngoingRental = logic.IsThereAnOngoingRental();
            Assert.IsFalse(hasOngoingRental);
        }
        [Test]
        public void CreateRentalWithInvalidArgumentsTest()
        {
            var rentalWithInvalidArguments = new Rental { RentalId = 0, RentalDate = DateTime.Now.AddDays(2) };

            Assert.Throws<ArgumentException>(() => logic.Create(rentalWithInvalidArguments));
        }
    }
}
