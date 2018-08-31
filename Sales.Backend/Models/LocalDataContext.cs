using Sales.Domain.Models;

namespace Sales.Backend.Models
{
    using Domain.Models;
    using Sales.Common.Models;
    using System.Data.Entity;

    public class LocalDataContext : DataContext
    {
        public DbSet<Product> Products1 { get; set; }
    }
}