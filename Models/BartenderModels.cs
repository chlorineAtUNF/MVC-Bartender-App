using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BartenderApp.Models
{
    public class Cocktail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public int CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
        public string PatronName { get; set; }
        public string Status { get; set; }
    }

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Cocktail> Cocktails { get; set; }
        public DbSet<Order> Orders { get; set; }
    }

    public class BartenderLogic
    {
        private readonly AppDbContext _context;
        public BartenderLogic(AppDbContext context) { _context = context; }
        
        public List<Cocktail> GetMenu() => _context.Cocktails.ToList();
        
        public void PlaceOrder(int cocktailId, string patronName)
        {
            _context.Orders.Add(new Order { CocktailId = cocktailId, PatronName = patronName, Status = "Queued" });
            _context.SaveChanges();
        }

        public List<Order> GetOrderQueue() => _context.Orders.Include(o => o.Cocktail).Where(o => o.Status == "Queued").ToList();

        public void SetOrderPrepared(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                order.Status = "Prepared";
                _context.SaveChanges();
            }
        }
    }
}