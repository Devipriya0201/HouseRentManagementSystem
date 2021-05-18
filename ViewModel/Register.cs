using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace HomeRentManagementSystem.ViewModel
{
    public class Register
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        //Required attribute implements validation on Model item that this fields is mandatory for user
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail id is not valid")]
        [Remote("IsAlreadySignedUpStudent", "Account", ErrorMessage = "EmailId already exists in database.")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^([0]|\+91)?[6789]\d{9}$", ErrorMessage = "Phone Number is not valid")]
        public long PhoneNumber { get; set; }


        [Required]
        [RegularExpression(@"^[2-9]{1}[0-9]{11}$", ErrorMessage = "Aadhar Number is not valid")]
        public long Aadharno { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$", ErrorMessage = "Password should be 8 characters and one capital,one special symbol,one digit")]
        public string Password { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
