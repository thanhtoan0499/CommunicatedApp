using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.Models
{
    public class InvoiceDetail
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int CurrentPrice { get; set; }
        public int SubTotalMoney { get; set; }

        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}