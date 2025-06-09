using System.ComponentModel.DataAnnotations;

namespace TH01.Models
{
    public class Contact
    {
        public int Id { get; set; }

        public string Name { get; set; }

       public string Email { get; set; }

        public string Message { get; set; }

        public DateTime SentDate { get; set; } = DateTime.Now;
    }
}
