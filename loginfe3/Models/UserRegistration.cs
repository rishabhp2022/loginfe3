using System.ComponentModel.DataAnnotations;

namespace loginfe3.Models
{
    public class UserRegistration
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string Password { get; set; }

        //public string Confirmpassword { get; set; }  

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int IsActive { get; set; }
    
    }
}
