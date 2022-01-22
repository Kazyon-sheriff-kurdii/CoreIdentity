using System.ComponentModel.DataAnnotations;

namespace CoreIdentity.Models
{
    public class LoginVM
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
