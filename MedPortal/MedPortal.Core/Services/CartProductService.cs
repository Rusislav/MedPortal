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
using MedPortal.Core.Models;

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
        public async Task<IEnumerable<ProductViewModel>> GetAllAsync(string userId)
        {
            var cart = await context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);

            //var products =  context.CartProducts.Where(c => c.CartId == cart.Id).Select(cart => cart.Product).ToList();

            var products = await context.CartProducts.Where(p => p.CartId == cart.Id)
                .Include(c => c.Product)
                .Include(c => c.Pharmacy)
                .Include(m => m.Product.Manufacturer)
                .Include(c => c.Product.Category)

                .ToListAsync();

            var model =  products.Select(p => new ProductViewModel()
            {
                
                Id = p.Product.Id,
                Name = p.Product.Name,
                Description = p.Product.Description,
                Prescription = p.Product.Prescription,
                ImageUrl = p.Product.ImageUrl,
                Price = p.Product.Price,
                ManifactureName = p.Product.Manufacturer.Name,
                ManifactureId = p.Product.ManufacturerId,
                CategoryName = p.Product.Category.Name,
                CategoryId = p.Product.CategotyId
            });

            return model;

        }
    }
}
