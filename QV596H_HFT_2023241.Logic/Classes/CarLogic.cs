using System;
using System.Collections.Generic;
using System.Linq;
using QV596H_HFT_2023241.Models;
using QV596H_HFT_2023241.Repository;


namespace QV596H_HFT_2023241.Logic
{
    public class CarLogic : ICarLogic
    {
        IRepository<Car> repo;
        public CarLogic(IRepository<Car> repo)
        {
            this.repo = repo;
        }
        public void Create(Car item)
        {
            if (item.Model is null)
                throw new ArgumentException("Name a model");
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Car Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Car> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Car item)
        {
            this.repo.Update(item);
        }


        //noncrud
        public string GetMostPopularBrand()
        {
            var mostPopularBrand = this.repo
                .ReadAll()
                .Where(c => c.RentalEvents != null && c.RentalEvents.Any())
                .GroupBy(c => c.Brand.BrandName)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            return mostPopularBrand;
        }

        public int CountRentalEvents(int carId)
        {
            var car = this.repo.ReadAll()
                .FirstOrDefault(c => c.CarId == carId);

            if (car != null && car.RentalEvents != null)
            {
                return car.RentalEvents.Count;
            }

            return 0;
        }



    }
}
