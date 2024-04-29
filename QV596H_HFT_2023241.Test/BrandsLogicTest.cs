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
    public class BrandsLogicTest
    {
        BrandsLogic logic;
        Mock<IRepository<Brands>> mockBrandRepo;

        [SetUp]
        public void Init()
        {
            mockBrandRepo = new Mock<IRepository<Brands>>();
            mockBrandRepo.Setup(m => m.ReadAll()).Returns(new List<Brands>
            {
                new Brands { BrandId = 1, BrandName = "BrandA", Cars = new List<Car> { new Car(), new Car() } },
                new Brands { BrandId = 2, BrandName = "BrandB", Cars = new List<Car> { new Car() } },
                new Brands { BrandId = 3, BrandName = "BrandC", Cars = new List<Car> { new Car(), new Car(), new Car() } },
            }.AsQueryable());
            logic = new BrandsLogic(mockBrandRepo.Object);
        }
        [Test]
        public void CountCarsForBrandTest()
        {
            int carCount = logic.CountCarsForBrand(1);
            Assert.That(carCount, Is.EqualTo(2));
        }
        [Test]
        public void FindBrandWithMostCarsTest()
        {
            var brandWithMostCars = logic.FindBrandWithMostCars();
            Assert.NotNull(brandWithMostCars);
            Assert.That(brandWithMostCars.BrandName, Is.EqualTo("BrandC"));
            Assert.That(brandWithMostCars.CarCount, Is.EqualTo(3));
        }
        [Test]
        public void CreateBrandTest()
        {
            var brand = new Brands { BrandName = "BrandD" };
            logic.Create(brand);
            mockBrandRepo.Verify(r => r.Create(brand), Times.Once);
        }
        [Test]
        public void DeleteBrandTest()
        {
            int brandId = 2;
            logic.Delete(brandId);
            mockBrandRepo.Verify(r => r.Delete(brandId), Times.Once);
        }
    }
}

