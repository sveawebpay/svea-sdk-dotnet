using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sample.AspNetCore.Models
{
    public class Order
    {
        [BindNever] public ICollection<CartLine> Lines { get; set; }

        [BindNever]
        public int OrderId { get; set; }

        [Key]
        public string SveaOrderId { get; set; }
    }
}