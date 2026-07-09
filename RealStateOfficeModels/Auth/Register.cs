using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateOfficeModels.Auth
{
    public class RegisterModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]

        public string Email { get; set; }

        [Required]

        public string Phone { get; set; }

        [Required]
     //   [MinLength(8)]

        public string Password { get; set; }
    }
}
