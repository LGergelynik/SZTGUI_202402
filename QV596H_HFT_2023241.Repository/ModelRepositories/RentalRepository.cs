using QV596H_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QV596H_HFT_2023241.Repository
{
    public class RentalRepository : Repository<Rental>, IRepository<Rental>
    {
        public RentalRepository(CarDbContext ctx) : base(ctx)
        {
        }

        public override Rental Read(int id)
        {
            return ctx.Rentals.FirstOrDefault(r => r.CarId == id);
        }

        public override void Update(Rental item)
        {
            var old = Read(item.RentalId);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }

            }
            ctx.SaveChanges();
        }
    }
}
