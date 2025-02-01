using Microsoft.EntityFrameworkCore;
using FRCSPreparationPortal.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRCSPreparationPortal.Repository
{
    public partial class DBContext : DbContext
    {
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<MockTest> MockTest { get; set; }
        public virtual DbSet<DemoTest> DemoTest { get; set; }
        public virtual DbSet<PrepTest> PrepTest { get; set; }
        public virtual DbSet<PrepTestConfig> PrepTestConfig { get; set; }
        public virtual DbSet<PrepTestConfigCategory> PrepTestConfigCategory { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public DBContext()
    {

    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        optionsBuilder.UseSqlServer(AppSettings.ConnectionStringDefault);
    //    }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

          
          
    }
}
}
