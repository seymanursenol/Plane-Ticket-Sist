using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ng_server.Entity;

namespace ng_server.ApplicationContext
{
    public class IdentityContext: IdentityDbContext<Users>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options): base(options)
        {

        }
        public IdentityContext() 
        {
        }

        public DbSet<Planes> IPlanes { get;set;}
        public DbSet<Cart> Carts { get;set;}
        public DbSet<CartItem> CartItems { get;set;}
        public DbSet<Rezervation> Rezervations { get;set;}
        public DbSet<RezervationItem> RezervationItems { get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-1B5I5TA;Initial Catalog=TicketDatabase;Integrated Security=false;Trusted_Connection=True;TrustServerCertificate=True;");
        }

    }
}