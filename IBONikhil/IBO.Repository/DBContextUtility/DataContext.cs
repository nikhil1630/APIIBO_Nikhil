﻿using IBO.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IBO.Repository.DBContextUtility
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Student> Students { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Logger> Loggers { get; set; }
        public DbSet<User> Users { get; set; }

    }

}
