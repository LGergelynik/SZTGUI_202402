using Moq;
using NUnit.Framework;
using QV596H_HFT_2023241.Logic;
using QV596H_HFT_2023241.Models;
using QV596H_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using static QV596H_HFT_2023241.Logic.CarLogic;

namespace QV596H_HFT_2023241.Test
{
    [TestFixture]
    public class CarLogicTest
    {
        CarLogic logic;
        Mock<IRepository<Car>> mockCarRepo;
        [SetUp]
        public void Init()
        {
            mockCarRepo = new Mock<IRepository<Car>>();
            mockCarRepo.Setup(m => m.ReadAll()).Returns(new List<Car>()
            {
                 new Car { CarId = 1, Model = "CarA", Brand = new Brands { BrandName = "BrandA" }, RentalEvents = new List<Rental> { new Rental { RentalDate = DateTime.Now.AddMonths(-1) } } },
                 new Car { CarId = 2, Model = "CarB", Brand = new Brands { BrandName = "BrandB" }, RentalEvents = new List<Rental> { new Rental { RentalDate = DateTime.Now.AddMonths(-2) } } },
                 new Car { CarId = 3, Model = "CarC", Brand = new Brands { BrandName = "BrandA" }, RentalEvents = new List<Rental> { new Rental { RentalDate = DateTime.Now.AddMonths(-3) } } },
            }.AsQueryable());
            logic = new CarLogic(mockCarRepo.Object);
        }
        [Test]
        public void GetMostPopularBrandTest()
        {
            string mostPopularBrand = logic.GetMostPopularBrand();
            Assert.That(mostPopularBrand, Is.EqualTo("BrandA"));
        }
        [Test]
        public void CountRentalEventsTest()
        {
            mockCarRepo.Setup(m => m.ReadAll()).Returns(new List<Car>
            {
                 new Car { CarId = 1, Model = "CarA", RentalEvents = new List<Rental> { new Rental(), new Rental(), new Rental() } },
            }.AsQueryable());
            int rentalEventCount = logic.CountRentalEvents(1);
            Assert.That(rentalEventCount, Is.EqualTo(3));
        }
        [Test]
        public void CreateCarWithNullName()
        {
            var nullCar = new Car { Model = null };
            Assert.Throws<ArgumentException>(() => logic.Create(nullCar));
        }

    }
}
