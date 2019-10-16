using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace jklintan_minions.Models
{
    public class HighscoreContext : DbContext
    {
        static Regex connectionString = new Regex(@"^postgres://(.*):(.*)@(.*):(.*)/(.*)$");    // Hehe lets hope for something reasonable...

        public DbSet<Highscore> Highscores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            string connString = Environment.GetEnvironmentVariable("DATABASE_URL");
            if (string.IsNullOrEmpty(connString)) {
                connString = "Host=my_host;Database=my_db;Username=my_user;Password=my_pw";
            }

            var match = connectionString.Match(connString);
            if (match.Success) {
                connString = string.Format(
                    "Host={0};Database={1};Username={2};Password={3};Port={4}",
                    match.Groups[3],
                    match.Groups[5],
                    match.Groups[1],
                    match.Groups[4],
                    match.Groups[2]
                );
            }
            Console.WriteLine(connString);
            options.UseNpgsql(connString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Highscore>()
                .Property(x => x.AcquriedAt)
                .HasDefaultValueSql("now()");
        }
    }

    public class Highscore {
        
        public int Id { get; set; }

        [Required]
        public int Score { get; set; }

        [Required]
        public string Username { get; set; }

        public DateTime AcquriedAt { get; set; }

        public string IP { get; set; }
    }
}