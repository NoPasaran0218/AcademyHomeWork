using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatOrderingService.Domain;

namespace MatOrderingService.Service.Storage.Impl
{
    public class ProductList : IProductList
    {
        IOrdersDbContext dbContext;
        public ProductList(IOrdersDbContext dbcontext)
        {
            dbContext = dbcontext;
        }
        public Product[] GetAll()
        {
            return dbContext.Products.ToArray();
        }

        public Product GetById(int id)
        {
            return dbContext.Products.FirstOrDefault(p => p.Id == id);
        }
    }
}
