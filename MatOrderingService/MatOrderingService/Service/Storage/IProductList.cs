using MatOrderingService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatOrderingService.Service.Storage
{
    public interface IProductList
    {
        Product GetById(int id);
        Product[] GetAll();
    }
}
