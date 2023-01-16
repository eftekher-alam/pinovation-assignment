using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PinovationAssignment.Models
{
    public class TblCity
    {
        [Key]
        public int cityId { get; set; }

        [Required]
        [Display(Name ="City")]
        public string cityName { get; set; }

        [Display(Name ="Country")]
        public int countryId { get; set; }
        [ForeignKey("countryId")]

        public TblCountry? TblCountry { get; set; }
        public ICollection<TblUsers>? TblUsers { get; set; }
    }
}
