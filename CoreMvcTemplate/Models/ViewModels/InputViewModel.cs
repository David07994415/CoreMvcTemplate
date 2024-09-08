using System.ComponentModel.DataAnnotations;

namespace CoreMvcTemplate.Models.ViewModels
{
    public class InputViewModel
    {
        [Required]
        public required string inputString { get; set; }

        [Required]
        public int inputInt { get; set; }
    }
}
