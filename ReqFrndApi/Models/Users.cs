using LinqToTwitter;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ReqFrndApi.Models
{
    public class Users
    {

        [Required]

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        

        //public virtual FriendRequest FriendRequest { get; set; }

    }
}
