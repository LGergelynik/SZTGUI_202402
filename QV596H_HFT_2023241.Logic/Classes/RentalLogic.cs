using QV596H_HFT_2023241.Models;
using QV596H_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QV596H_HFT_2023241.Logic
{
    public class RentalLogic : IRentalLogic
    {
        IRepository<Rental> repo;

        public RentalLogic(IRepository<Rental> repo)
        {
            this.repo = repo;
        }

        public void Create(Rental item)
        {
            if (item.RentalDate == DateTime.MinValue)
                throw new ArgumentException("Not a valid date");
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Rental Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Rental> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Rental item)
        {
            this.repo.Update(item);
        }
        //noncrud

        public bool IsThereAnOngoingRental()
        {
            return this.repo.ReadAll().Any(c => c.RentalDate > DateTime.Now);
        }
    }
}

