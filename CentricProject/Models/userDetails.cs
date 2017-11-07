using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CentricProject.Models
{
    public class userDetails
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        [Display(Name = "Primary Phone")]
        [Phone]
        public string phoneNumber { get; set; }

        [Display(Name = "Business Unit")]
        public string office { get; set; }

        [Display(Name = "Skill areas")]
        public string currentRole { get; set; }

        [Display(Name = "Centric Anniversary")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime anniversary { get; set; }

        [Display(Name = "Number of years with Centric")]
        public int yearsWithCentric { get; set; }

        public string photo { get; set; }

        public string fullName { get { return lastName + ", " + firstName; } }

    }
}