using QV596H_HFT_2023241.Models;
using System.Linq;

namespace QV596H_HFT_2023241.Logic
{
    public interface IBrandsLogic
    {
        int CountCarsForBrand(int brandId);
        void Create(Brands item);
        void Delete(int id);
        BrandsLogic.BrandWithCarCount FindBrandWithMostCars();
        Brands Read(int id);
        IQueryable<Brands> ReadAll();
        void Update(Brands item);
    }
}