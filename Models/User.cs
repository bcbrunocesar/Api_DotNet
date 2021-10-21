using System.ComponentModel.DataAnnotations;

namespace Dot.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Email { get; set; }
        public string Status { get; set; }
    }
}