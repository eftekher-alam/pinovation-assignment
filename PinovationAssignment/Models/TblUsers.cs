using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PinovationAssignment.Models
{
    public class TblUsers
    {
        [Key]
        public int userId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        public string fName { get; set; }

        [StringLength(100)]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        public string? lName { get; set; }

        [Display(Name ="Name")]
        public string fullName 
        { 
            get 
            {
                return $"{fName} {lName}";
            }
        }

        [Required]
        [Display(Name = "Phone")]
        public long phoneNo { get; set; }

        [Required]
        [Display(Name = "Email")]
        [StringLength(250)]
        public string emailNo { get; set; }

        [StringLength(100)]
        [Display(Name ="Image")]
        public string? userImg { get; set; }

        [StringLength(100)]
        [Display(Name = "CV Upload")]
        public string? userCV { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Required]
        [Display(Name = "Date Of Birth"), DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime dob { get; set; }


        [Display(Name = "City")]
        public int cityId { get; set; }
        [ForeignKey("cityId")]

        public TblCity? TblCity { get; set; }
    }
}
