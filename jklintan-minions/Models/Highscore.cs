using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace jklintan_minions.Models
{
    public class HighscoreContext : DbContext
    {
        public DbSet<Highscore> Highscores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            string connString = Environment.GetEnvironmentVariable("PSQL_CONNECTION_STRING");
            if (string.IsNullOrEmpty(connString)) {
                connString = "Host=my_host;Database=my_db;Username=my_user;Password=my_pw";
            }
            options.UseNpgsql(connString);
        }
    }

    public class Highscore {
        
        public int Id { get; set; }
        
        [Required]
        public int Score { get; set; }
        
        [Required]
        public string Username { get; set; }
        
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime AcquriedAt { get; set; }
        
        public string IP { get; set; }
    }
}