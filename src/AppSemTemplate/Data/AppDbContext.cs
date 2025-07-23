using AppSemTemplate.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppSemTemplate.Data
{
    // Herdando de IdentityDbContext para dar suporte ao Identity
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Produto> Produtos { get; set; }
    }
}
