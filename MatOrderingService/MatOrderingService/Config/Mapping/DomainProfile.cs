using AutoMapper;
using MatOrderingService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatOrderingService.Config.Mapping
{
    public class DomainProfile:Profile
    {
        public DomainProfile()
        {
            CreateMap<Order, OrderInfo>().ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()));
            CreateMap<NewOrder, Order>();
            CreateMap<EditOrder, Order>();
            CreateMap<OrderItem, OrderDetailItem>().ForMember(d => d.Info, c => c.MapFrom(s => s.Product.Id.ToString() + " | " + s.Product.Name + " |Count:" + s.Count));
        }
    }
}
