using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SportsTracker.Models.DbModel
{
    [Table("User")]
    public class User
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 200)]
        public int? Age { get; set; }

        [Required]
        [Range(1, 500)]
        
        public decimal? Weight { get; set; }

      
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Birthdate { get; set; }
        public int UserProfileId { get; set; }
        
        [Required]
        public string Firstname { get; set; }
        
        [Required]
        public string Lastname { get; set; }
        public string UserName { get; set; }
        public string ProfilePicturePath { get; set; }
    }
}