using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using TimeZone.Entities;

namespace TimeZone.Requests
{
    public class GuestRequest
    {
        [Required]
        public Title Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        [Required]
        public List<string> PhoneNumber { get; set; }
        //public List<PhoneRequest> PhoneNumbers { get; set; }
    }
}
