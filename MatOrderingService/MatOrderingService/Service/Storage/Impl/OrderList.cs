using AutoMapper;
using Dapper;
using MatOrderingService.Domain;
using MatOrderingService.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MatOrderingService.Service.Storage.Impl
{
    public class OrderList:IOrderList
    {
        private IOrdersDbContext _orderDbContext;
        private ICodeGeneratorService _codeGeneratorService;
        IMapper _mapper;
        private static int i = 2;

        public OrderStatus Promoted { get; private set; }

        public OrderList(IMapper mapper, ICodeGeneratorService codeGeneratorService, IOrdersDbContext orderDbContext)
        {
            _mapper = mapper;
            _orderDbContext = orderDbContext;
            _codeGeneratorService = codeGeneratorService;

         
        }

        public async Task<OrderInfo> Create(NewOrder order)
        {
            var newOrder = new Order()
            {
                //Id = ++i,
                CreateDate = DateTime.Now,
                Status = OrderStatus.New,
                IsDeleted = false,
                OrderDetails = order.OrderDetails,
                CreatorId = order.CreatorId
            };

            newOrder.OrderCode = await _codeGeneratorService.GetCode(newOrder.Id);
            _orderDbContext.Orders.Add(newOrder);
            _orderDbContext.SaveChangesAsync();
            return _mapper.Map<OrderInfo>(newOrder);
        }

        public bool Delete(int id)
        {
            _orderDbContext.Orders.FirstOrDefault(o => o.Id == id).IsDeleted = true;
            return true;
        }

        public OrderInfo Get(int id)
        {
            var obj = _orderDbContext.Orders.AsNoTracking().FirstOrDefault(o => o.Id == id && !o.IsDeleted);
            if (obj == null)
                throw new Exception();
            return _mapper.Map<OrderInfo>(obj);
        }

        public OrderInfo Update(int id, EditOrder order)
        {
            var selectedOrder = _orderDbContext.Orders.Include(o => o.OrderItems).FirstOrDefault(o => o.Id == id);
            if (selectedOrder!=null)
            {
                selectedOrder.CreateDate = DateTime.Now;
                selectedOrder.OrderItems.Clear();
                _orderDbContext.SaveChanges();
                foreach(var item in order.OrderItems)
                {
                    var oItem = new OrderItem()
                    {
                        OrderId = id,
                        ProductId = item.ProductId,
                        Count = item.Count
                    };
                    selectedOrder.OrderItems.Add(oItem);
                }
                _orderDbContext.SaveChanges();
                //var obj1 = _orderDbContext.Orders.Include(o=>o.OrderItems).ThenInclude(p=>p.Product).FirstOrDefault(o => o.Id == obj.Id);
                return _mapper.Map<OrderInfo>(selectedOrder);
            }
            else
                throw new ArgumentException();
        }

        public OrderInfo[] GetAll()
        {
            List<OrderInfo> orderInfoList = new List<OrderInfo>();
            var orders = _orderDbContext.Orders.Include(p => p.OrderItems).ThenInclude(p => p.Product);
            foreach (var item in orders)
            {

                //orderInfoList.Add(OrderToOrderInfo(item));
                orderInfoList.Add(_mapper.Map<OrderInfo>(item));
                
            }
            return orderInfoList.ToArray();
        }

        public async Task<OrderStatisticItem []> GetStatistics()
        {
            //var ordersStatisticItems = await _orderDbContext
            //    .Orders
            //    .AsNoTracking()
            //    .Where(p => !p.IsDeleted)
            //    .GroupBy(g => g.CreatorId)
            //    .Select(p => new OrderStatisticItem { CreatorId = p.Key, NumberOfOrders = p.Count() })
            //.ToArrayAsync();
            using (var connection = _orderDbContext.Database.GetDbConnection())
            {
                var ordersStatisticItems = await connection.QueryAsync<OrderStatisticItem>(@"
                    Select CreatorId, Count(*) as NumberOfOrders
                    From Orders
                    Group by CreatorId;
                ");
                return ordersStatisticItems.ToArray();
            }
        }

        private OrderInfo OrderToOrderInfo(Order order)
        {
            OrderInfo _orderInfo = new OrderInfo();
            _orderInfo.CreateDate = order.CreateDate;
            _orderInfo.CreatorId = order.CreatorId;
            _orderInfo.Id = order.Id;
            _orderInfo.OrderCode = order.OrderCode;
            _orderInfo.Status = order.Status.ToString();
            _orderInfo.OrderItems = new List<OrderDetailItem>();
            foreach(var item in order.OrderItems)
            {
                OrderDetailItem detail = new OrderDetailItem();
                detail.Info = item.Product.Name + "|Id:" + item.ProductId + "|Count:" + item.Count;
                _orderInfo.OrderItems.Add(detail);
            }

            return _orderInfo;
        }
    }
}
