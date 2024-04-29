using QV596H_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QV596H_HFT_2023241.Repository
{
    public class BrandRepository : Repository<Brands>, IRepository<Brands>
    {
        public BrandRepository(CarDbContext ctx) : base(ctx)
        {
        }

        public override Brands Read(int id)
        {
            return ctx.Brands.FirstOrDefault(b => b.BrandId == id);
        }
        public override void Update(Brands item)
        {
            var old = Read(item.BrandId);
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
