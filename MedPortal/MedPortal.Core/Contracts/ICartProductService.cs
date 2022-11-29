using MedPortal.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedPortal.Core.Contracts
{
    public interface ICartProductService
    {
        public Task AddProductToCart(int pharmacyId, int productId , int cartId);

        public Task<Cart> GetUserCart(string userId);
    }
}
