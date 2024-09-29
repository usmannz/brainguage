﻿using Microsoft.EntityFrameworkCore;
using SampleProject.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Repository
{
    public partial class DBContext : DbContext
    {
        public virtual DbSet<Users> Users { get; set; }
          public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }


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