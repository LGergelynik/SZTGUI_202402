using QV596H_HFT_2023241.Models;
using QV596H_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QV596H_HFT_2023241.Logic
{
    public class BrandsLogic : IBrandsLogic
    {
        IRepository<Brands> repo;

        public BrandsLogic(IRepository<Brands> repo)
        {
            this.repo = repo;
        }

        public void Create(Brands item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Brands Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Brands> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Brands item)
        {
            repo.Update(item);
        }

        //noncrud
        public int CountCarsForBrand(int brandId)
        {
            return repo.ReadAll()
                .Where(b => b.BrandId == brandId)
                .SelectMany(b => b.Cars)
                .Count();
        }
        public class BrandWithCarCount
        {
            public string BrandName { get; set; }
            public int CarCount { get; set; }
        }
        public BrandWithCarCount FindBrandWithMostCars()
        {
            var brandWithMostCars = repo.ReadAll()
                .AsEnumerable()
                .Select(b => new BrandWithCarCount
                {
                    BrandName = b.BrandName,
                    CarCount = (b.Cars != null) ? b.Cars.Count : 0
                })
                .OrderByDescending(b => b.CarCount)
                .FirstOrDefault();

            return brandWithMostCars;
        }


    }
}
