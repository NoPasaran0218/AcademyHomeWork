using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MatOrderingService.Domain
{
    public class NewOrder
    {
        [Required]
        [MaxLength(200)]
        public string OrderDetails { get; set; }
        [MaxLength(50)]
        [Required]
        public string CreatorId { get; set; }
    }
}
