using System.ComponentModel.DataAnnotations;

namespace TTDesign.API.Resources
{
    public class RevokeTokenResource
    {
        [Required]
        public string Token { get; set; }
    }
}
