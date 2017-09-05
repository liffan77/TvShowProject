using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tvShowProject.Models.VM
{
    public class RegisterVM
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [EmailAddress(ErrorMessage ="Please insert a correct Email-Address")]
        public string Email { get; set; }
        //Kommenterar så att vi kan pusha!
    }
}
