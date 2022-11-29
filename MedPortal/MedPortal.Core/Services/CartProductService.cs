using MedPortal.Core.Contracts;
using MedPortal.Infrastructure.Common;
using MedPortal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedPortal.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;

namespace MedPortal.Core.Services
{
    public class CartProductService : ICartProductService
    {
        private readonly ApplicationDbContext context;
        private readonly IRepository repository;

        public CartProductService(ApplicationDbContext _context, IRepository _repository)
        {
            this.context = _context;
            this.repository = _repository;
        }

        public async Task AddProductToCart(int pharmacyId, int productId, int cartId)
        { 
            CartProduct cartProduct = new CartProduct()
            {
                PharamcyId = pharmacyId,
                ProductId = productId,
                CartId  = cartId,
            };

           await  repository.AddAsync(cartProduct);
           await repository.SaveChangesAsync();

        }

        public async Task<Cart> GetUserCart(string userId)
        {
            var model =  await context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
            if(model == null)
            {
                throw new ArgumentException("Invalid User Id");
            }

            return model;
        }
    }
}
