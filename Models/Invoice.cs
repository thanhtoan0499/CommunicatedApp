using DatingApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.Models
{
    public class Invoice
    {
        public Invoice()
        {
        }
        public int Id { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public int? TotalMoney { get; set; } = 0;

        public int UserId { get; set; }
        public AppUser AppUser { get; set; }

        public IList<InvoiceDetail> InvoiceDetails { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}
