using Dlores.Ecommerce.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dlores.Ecommerce.Api.Data
{
    public partial class EcommerceDb : DbContext
    {
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }

        public EcommerceDb(DbContextOptions<EcommerceDb> options) : base(options)
        {
            
        }


    }
}
