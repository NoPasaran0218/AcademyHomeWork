using MatOrderingService.Domain;
using MatOrderingService.Filters;
using MatOrderingService.Service.Storage;
using MatOrderingService.Service.Storage.Impl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatOrderingService.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController:Controller
    {
        private static IOrderList _orderList;
        public OrderController(IOrderList orderList)
        {
            _orderList = orderList;
        }
        [HttpGet]
        public OrderInfo[] Get([FromServices]IOrderList orderList)
        {
            return orderList.GetAll().ToArray();
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(_orderList.Get(id));
            //var obj = _orderList.Get(id);
            //if (obj == null)
            //    return NotFound();
            //else
            //    return Ok(obj);
        }
        [HttpPost]
        [Route("Update/{id}")]
        public ActionResult Update(int id, [FromBody]EditOrder order)
        {
            if(ModelState.IsValid)
            {
                var obj = _orderList.Update(id, order);
                return Ok(obj);
            }
            else
            {
                return BadRequest(ModelState);
            }
            
        }
        /// <summary>
        /// </summary>
        /// <param name="Order"> which will added</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(typeof(OrderInfo),200)]
        [ProducesResponseType(typeof(void),404)]
        public async Task<ActionResult> Create([FromBody]NewOrder order)
        {
            if (ModelState.IsValid)
                return Ok(await _orderList.Create(order));
            else
                return BadRequest(ModelState);
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(_orderList.Delete(id));
        }

        [HttpGet("statistic")]
        [ProducesResponseType(typeof(OrderStatisticItem[]), 200)]
        public async Task<IActionResult> GetStatistics()
        {
            return Ok(_orderList.GetStatistics());
        }
    }
}
