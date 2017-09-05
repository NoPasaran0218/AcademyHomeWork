using MatOrderingService.Domain;
using MatOrderingService.Service.Storage.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatOrderingService.Service.Storage
{
    public interface IOrderList
    {
        OrderInfo[] GetAll();
        OrderInfo Get(int id);
        Task<OrderInfo> Create(NewOrder order);
        OrderInfo Update(int id, EditOrder order);
        bool Delete(int id);
        Task<OrderStatisticItem[]> GetStatistics();
    }
}
