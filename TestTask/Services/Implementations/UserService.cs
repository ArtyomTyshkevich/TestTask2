using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        public readonly ApplicationDbContext db;
        public UserService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<User> GetUser()
        {

            return await db.Users
                            .Include(u => u.Orders)
                            .OrderByDescending(u => u.Orders
                                .Where(o => o.CreatedAt.Year == 2003 && o.Status == Enums.OrderStatus.Delivered)
                                .Sum(o => o.Price*o.Quantity))
                            .FirstAsync();
        }

        public async Task<List<User>> GetUsers()
        {
            return await db.Users
                            .Include(u => u.Orders)
                            .Where(u => u.Orders
                                .Any(o => o.CreatedAt.Year == 2010 && o.Status == Enums.OrderStatus.Paid))
                            .ToListAsync();
        }
    }
}
