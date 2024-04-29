using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.ConstrainedExecution;
using System.Text.Json.Serialization;

namespace QV596H_HFT_2023241.Models
{
    public class Brands
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        [JsonIgnore]
        public virtual List<Car> Cars { get; set; }

    }
}
