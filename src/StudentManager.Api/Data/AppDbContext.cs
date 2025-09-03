﻿using Microsoft.EntityFrameworkCore;
using StudentManager.Api.Entities;
using System.Collections.Generic;

public class AppDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
