using TestTask.Models;
using TestTask.Data;
using TestTask.Services.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        public readonly ApplicationDbContext db;
        public OrderService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<Order> GetOrder()
        {
            return await db.Orders
                            .Where(o => o.Quantity > 1)
                            .OrderByDescending(o => o.CreatedAt)
                            .FirstAsync();
        }

        public async Task<List<Order>> GetOrders()
        {
            return await db.Orders
                            .Include(o => o.User)
                            .Where(o => o.User.Status == Enums.UserStatus.Active)
                            .OrderByDescending(o => o.CreatedAt) 
                            .ToListAsync();
        }
    }
}
