using System.ComponentModel.DataAnnotations;

namespace PinovationAssignment.Models
{
    public class TblCountry
    {
        [Key]
        public int countryId { get; set; }

        [Required]
        [Display(Name ="Country")]
        public string countryName{ get; set; }

        public ICollection<TblCity>? TblCities { get; set; }
    }
}
