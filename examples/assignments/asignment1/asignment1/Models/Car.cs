using System;
using System.ComponentModel.DataAnnotations;

namespace asignment1.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Color { get; set; }

        public int Year { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Purchase Date")]
        public DateTime PurchaseDate{ get; set; }

        [Range(0, 999_999)]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int Kilometers { get; set; }
    }
}
