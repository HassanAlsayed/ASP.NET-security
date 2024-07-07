using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities
{
    public class Credential
    {   
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
       
    }
}
