using System;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace API
{
	public class DataContext : DbContext
	{
        // obviously connection strings are kept hidden and sqlite dbms is not for prod
        const string _connectionStrings = "DataSource=webapp.db";

		public DbSet<UserEntity> Users { get; set; }
        public DbSet<LikeEntity> Likes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionStrings);
        }
    }
}

