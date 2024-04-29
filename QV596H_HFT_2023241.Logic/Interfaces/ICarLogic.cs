using QV596H_HFT_2023241.Models;
using System.Linq;

namespace QV596H_HFT_2023241.Logic
{
    public interface ICarLogic
    {
        int CountRentalEvents(int carId);
        void Create(Car item);
        void Delete(int id);
        string GetMostPopularBrand();
        Car Read(int id);
        IQueryable<Car> ReadAll();
        void Update(Car item);
    }
}