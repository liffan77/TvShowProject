using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tvShowProject.Models.VM
{
    public class LoggedInUserVM
    {
        /// <summary>
        /// AspNetID
        /// </summary>
        public string AspNetId { get; set; }
        public int Id { get; set; }
        public string Username { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please insert a correct Email-Address")]
        public string Email { get; set; }

    }
}
