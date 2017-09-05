using MatOrderingService.Service.Storage;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatOrderingService.Controllers
{
    [Route("api/[controller]")]
    public class ProductController:Controller
    {
        IProductList _productList;
        public ProductController(IProductList productList)
        {
            _productList = productList;
        }
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_productList.GetAll());
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_productList.GetById(id));
        }
    }
}
