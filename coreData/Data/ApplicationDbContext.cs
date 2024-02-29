using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using coreModel;

namespace coreData.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Book>? Books { get; set; }
        public DbSet<Author>? Authors { get; set; }
        public DbSet<Publisher>? Publishers { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<Admin>? Admins { get; set; }
        public DbSet<Loan>? Loan { get; set; }
    }

}
