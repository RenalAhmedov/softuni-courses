using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Watchlist.Data.Models
{
    public class User : IdentityUser 
    {
        public List<UserMovie> UserMovies { get; set; } = new List<UserMovie>();
    }
}
