using System.ComponentModel.DataAnnotations.Schema;

namespace TimeZone.Entities
{
    public class Guest
    {
        public Guid Id { get; set; }
        public Title Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumbers { get; set; }
    }

    public enum Title
    {
        Mr,
        Mrs,
        Miss,
    }
}
