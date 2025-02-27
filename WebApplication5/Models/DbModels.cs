using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebApplication5.Models
{
    public enum PlayerGrade
    {
        G01 = 1, G02, G03, G04
    }

   
    public class Sports
    {
        public Sports()
        {
            this.Players = new List<Player>();
        }
        public int SportsId { get; set; }
        [Required, StringLength(50, ErrorMessage = "Sports name is required!!"), Display(Name = "Sports Name")]
        public string SportsName { get; set; }
        //nev
        public virtual ICollection<Player> Players { get; set; }

    }

    public class Player
    {
        public int PlayerId { get; set; }

        [Required, StringLength(50, ErrorMessage = "Player name is requred"),
            Display(Name = "Player Name")]
        public string PlayerName { get; set; }

        [Required, Column(TypeName = "date"),Display(Name ="JOin date"),
            DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime JoinDate { get; set; }

        [EnumDataType(typeof(PlayerGrade))]
        public PlayerGrade? PlayerGrade { get; set; }

        public String PicturePath { get; set; }
        //Foreign key
        [ForeignKey("Sports")]
        public int SportsId { get; set; }
        //Navigation property
        public virtual Sports Sports { get; set; }


    }
    public class ClubDbContext : DbContext
    {
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    Database.SetInitializer(new DbInitializer());
        //} 
        public DbSet<Sports> Sports { get; set; }
        public DbSet<Player> Players { get; set; }
    }
    public class DbInitializer : DropCreateDatabaseIfModelChanges<ClubDbContext>
    {
        protected override void Seed(ClubDbContext context)
        {
            base.Seed(context);
            Sports s1 = new Sports { SportsName = "Cricket" };
            Sports s2 = new Sports { SportsName = "Football" };
            Sports s3 = new Sports { SportsName = "Hockey" };

            s1.Players.Add(new Player {PlayerName = "Sachin", 
                 JoinDate = DateTime.Now.AddYears(-16), PlayerGrade = PlayerGrade.G01, PicturePath = "~/Images/20211225_091012.jpg"
            });
            s1.Players.Add(new Player
            {
                PlayerName = "Rahul",
                JoinDate = DateTime.Now.AddYears(-15),
                PlayerGrade = PlayerGrade.G02,
                PicturePath = "~/Images/20211225_091012.jpg"
            });
            s1.Players.Add(new Player
            {
                PlayerName = "Massi",
                JoinDate = DateTime.Now.AddYears(-24),
                PlayerGrade = PlayerGrade.G03,
                PicturePath = "~/Images/20211225_091012.jpg"
            });
            context.Sports.AddRange(new Sports[] { s1, s2, s3 });
            context.SaveChanges();
        }
    }
}