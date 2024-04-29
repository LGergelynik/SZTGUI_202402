using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QV596H_HFT_2023241.Models
{
    public class Car
    { 

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarId { get; set; }
        public string Model { get; set; }

        public int BrandId { get; set; }
        [JsonIgnore]
        public virtual Brands Brand { get; set; }
        [JsonIgnore]
        public virtual List<Rental> RentalEvents { get; set; }


    }
}