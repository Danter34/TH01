using System.ComponentModel.DataAnnotations.Schema;

namespace TH01.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; } 
        public string? Phone { get; set; }
        public string? Address { get; set; }

        public decimal OrderTotal { get; set; }

        public DateTime OrderPlaced { get; set; }

        public string Status { get; set; } = "Pending";

        public List<OrderDetail>? OrderDetails { get; set; }
    }
}
