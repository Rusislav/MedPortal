using MedPortal.Core.Contracts;
using MedPortal.Infrastructure.Common;
using MedPortal.Infrastructure;
using MedPortal.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using MedPortal.Core.Models;

namespace MedPortal.Core.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext context;
        private readonly IRepository repository;

        public CartService(ApplicationDbContext _context, IRepository _repository)
        {
            this.context = _context;
            this.repository = _repository;
        }

      
        /// <summary>
        /// Тук правя количка на даден User 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task GetCartAsync(string userId)
        {
            var model =  context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
            var cart = model.Result;
            if(cart == null)
            {
                Cart userCart = new Cart()
                { 
                UserId = userId,
                };
                await repository.AddAsync(userCart);
               await repository.SaveChangesAsync();
            }

        }

      
    }
}
