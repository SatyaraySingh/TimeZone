using Microsoft.AspNetCore.Http;
using TimeZone.Entities;

namespace TimeZone.Responses
{
    public class GuestResponse
    {
        public Guid Id { get; set; }
        public Title Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        //public List<PhoneResponse> PhoneNumbers { get; set; }
    }
}
