using QV596H_HFT_2023241.Models;
using System.Linq;

namespace QV596H_HFT_2023241.Logic
{
    public interface IRentalLogic
    {
        void Create(Rental item);
        void Delete(int id);
        bool IsThereAnOngoingRental();
        Rental Read(int id);
        IQueryable<Rental> ReadAll();
        void Update(Rental item);
    }
}