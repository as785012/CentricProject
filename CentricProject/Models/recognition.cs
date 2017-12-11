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

        [Required]
        [Display(Name = "Core value recognized")]
        public coreValue recognitionCoreValue { get; set; }

        public enum coreValue
        {
            [Display(Name ="Excellence in delivery")]
            Excellence = 1,
            [Display(Name = "Embraced integrity and openness")]
            Integrity = 2,
            [Display(Name = "Practiced responsible stewardship")]
            Stewardship = 3,
            [Display(Name = "Strived to innovate")]
            Innovate = 4,
            [Display(Name = "Ignited passion for the greater good")]
            Passion = 5,
            [Display(Name = "Living a balanced life")]
            Balance = 6,
            [Display(Name = "Invested in an exceptional culture")]
            Culture = 7
        }

        [Required]
        [Display(Name = "Description of recognition")]
        public String description { get; set; }

        [Required]
        [Display(Name = "Stars")]
        public int starPoints { get; set; }


        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dateTime { get; set; }

        [ForeignKey("recognizee")]
        public virtual userDetails userDetails { get; set; }
    }
}