using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatOrderingService.Domain
{
    public class OrderInfo
    {
        public int Id { get; set; }
        public string OrderDetails { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatorId { get; set; }
        public string OrderCode { get; set; }
        public ICollection<OrderDetailItem> OrderItems { get; set; }
    }
}
