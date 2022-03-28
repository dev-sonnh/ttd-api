using System.ComponentModel.DataAnnotations;

namespace TTDesign.API.Resources.Extended
{
    public class UserCredentialsResource
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(255)]
        public string UserName { get; set; }

        [Required]
        [StringLength(32)]
        public string Password { get; set; }
    }
}
