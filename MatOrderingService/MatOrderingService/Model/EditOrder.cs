using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MatOrderingService.Domain
{
    public class EditOrder
    {
        [Required]
        [MaxLength(200)]
        public ICollection<OrderItem> OrderItems{ get; set; }
    }
}
