using AppSemTemplate.Models;
using Microsoft.EntityFrameworkCore;

namespace AppSemTemplate.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Produto> Produtos { get; set; }
    }
}
