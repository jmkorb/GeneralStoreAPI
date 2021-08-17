using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GeneralStoreAPI.Models
{
    // DBContext looks at the db as we load it. if we delete the context but it's been saved, when we load up the context the saved changes will show up.
    public class StoreDbContext : DbContext
    {
        public StoreDbContext() : base("DefaultConnection")
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}