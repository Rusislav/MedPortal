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
            var model = await context.CartProducts.FirstOrDefaultAsync(c => c.PharamcyId == pharmacyId && c.ProductId == productId && c.CartId == cartId);
            if (model == null)
            {
                CartProduct cartProduct = new CartProduct()
                {
                    PharamcyId = pharmacyId,
                    ProductId = productId,
                    CartId = cartId,
                    Quantity = 1,
                };

                await repository.AddAsync(cartProduct);                
            }
            else
            {
                model.Quantity++;
            }
            await repository.SaveChangesAsync();
        }

        public async Task<Cart> GetUserCartAsync(string userId)
        {
            var model =  await context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
            if(model == null)
            {
                throw new NullReferenceException("Invalid User Id");
            }

            return model;
        }
        public async Task<IEnumerable<CartProductViewModel>> GetAllAsync(string userId)
        {
            var cart = await context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
          

            var products = await context.CartProducts.Where(p => p.CartId == cart.Id)
                .Include(c => c.Product)
                .Include(c => c.Pharmacy)
                .Include(m => m.Product.Manufacturer)
                .Include(c => c.Product.Category)

                .ToListAsync();

            var model =  products.Select(p => new CartProductViewModel()
            {
                
                Id = p.Product.Id,
                Name = p.Product.Name,
                Description = p.Product.Description,
                Prescription = p.Product.Prescription,
                ImageUrl = p.Product.ImageUrl,
                Price = p.Product.Price *  p.Quantity,
                ManifactureName = p.Product.Manufacturer.Name,
                ManifactureId = p.Product.ManufacturerId,
                CategoryName = p.Product.Category.Name,
                CategoryId = p.Product.CategotyId,
                Quantity = p.Quantity,
                CartProductId = p.Id,
                PharmacyName = p.Pharmacy.Name
                
            });

            return model;

        }

        public async Task DeleteProductFromCartAsync(int cartId)
        {            
            var model =   await context.CartProducts.FirstOrDefaultAsync(c => c.Id == cartId);
            if (model == null)
            {
                throw new NullReferenceException("Invalid CartProduct Id");
            }
            else
            {
                if(model.Quantity > 1)
                {
                    model.Quantity--;
                    context.Update(model);
                   await  context.SaveChangesAsync();
                }                
                 else if(model.Quantity <= 1)
                {
                    context.Remove(model);
                    await context.SaveChangesAsync();
                }                            
            }
            
        }

        public CheckoutViewModel GetTotalPrice(string userId)
        {
            var model = GetAllAsync(userId);
            var products = model.Result.ToList();

            decimal totalPrice = 0;

            foreach (var item in products)
            {
                totalPrice += item.Price;
            }

            var data = new CheckoutViewModel()
            { TotalPrice = totalPrice};

            return data;

        }

        public async Task RemoveProductsFromCartAftersuccessfulOrderAsync(string userId)
        {
            var cart = await context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
          

            if (cart == null)
            {
                throw new NullReferenceException("Invalid User Id");
            }
            var model = await context.CartProducts.Where(c => c.CartId == cart.Id).ToListAsync();

            if (model == null)
            {
                throw new NullReferenceException("Invalid Cart Id");
            }
            context.RemoveRange(model);
          await   context.SaveChangesAsync();
            

        }
    }
}
