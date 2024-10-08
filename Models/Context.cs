using Microsoft.EntityFrameworkCore;

namespace ETicaretDemo.Models
{
    public class Context :DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=MSI\\SQLEXPRESS;Database=ETicaretDemoDb;Integrated Security=true");
        }

        public DbSet<Urun> Urunler { get; set; }    
        public DbSet<Sepet> Sepetim { get; set; }
    }
}
