using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsTracker.Models.DbModel
{
    [Table("Group")]
    public class Group
    {
        public int Id { get; set; }
        [Required]
        public string Groupname { get; set; }
        public int UserId { get; set; }
    }
}