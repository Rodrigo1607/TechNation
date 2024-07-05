using Microsoft.EntityFrameworkCore;
using TechNation.Models;

namespace TechNation.Context
{
    public class AddDbContext : DbContext
    {

        public AddDbContext(DbContextOptions<AddDbContext> options) : base(options) 
        {
        }
    public DbSet<NotaFiscal> NotaEmitida {  get; set; }
    }
}
