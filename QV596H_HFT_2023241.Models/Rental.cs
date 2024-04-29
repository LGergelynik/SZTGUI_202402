using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace QV596H_HFT_2023241.Models
{
    public class Rental
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RentalId { get; set; }
        public DateTime RentalDate { get; set; }

        public int CarId { get; set; }
        [JsonIgnore]
        public virtual Car Car { get; set; }

    }
}
