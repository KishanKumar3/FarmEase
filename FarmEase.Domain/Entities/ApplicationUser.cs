using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace FarmEase.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [JsonIgnore]
        public ICollection<Booking>? Bookings { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
