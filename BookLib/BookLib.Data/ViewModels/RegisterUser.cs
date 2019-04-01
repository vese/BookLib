using System.ComponentModel.DataAnnotations;

namespace BookLib.Data.ViewModels
{
    public class RegisterUser
    {
        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Pass { get; set; }
    }
}
