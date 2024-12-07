using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WheelDeal.Models
{
    public class ApplicationUser: IdentityUser
    {
        [DisplayName("Office Phone")]
        public string PhoneNumber2 { get; set; }

        [NotMapped]
        public bool isAdmin { get; set; }
    }
}
