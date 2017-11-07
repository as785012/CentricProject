using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CentricProject.Models
{
    public class recognition
    {
        [Required]
        [Key]
        public int recognitionID { get; set; }

        [Required]
        [Display(Name = "Person giving the recognition")]
        public Guid recognizer { get; set; }

        [ForeignKey("recognizer")]
        public virtual userDetails Giver { get; set; }

        [Required]
        [Display(Name = "Person recieving the recognition")]
        public Guid recognizee { get; set; }

        [Display(Name = "Core value recognized")]
        public coreValue recognitionCoreValue { get; set; }

        public enum coreValue
        {
            Excellence = 1,
            Integrity = 2,
            Stewardship = 3,
            Innovate = 4,
            Balance = 5
        }

        [Required]
        [Display(Name = "Description of recognition")]
        public String description { get; set; }

        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dateTime { get; set; }

        [ForeignKey("recognizee")]
        public virtual userDetails userDetails { get; set; }
    }
}