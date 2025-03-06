using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication5.Models.ViewModel
{
    public class PlayerViewModel
    {

        public int PlayerId { get; set; }

        [Required, StringLength(50, ErrorMessage = "Player name is requred"),
            Display(Name = "Player Name")]
        public string PlayerName { get; set; }

        [Required, Column(TypeName = "date"), Display(Name = "JOin date"),
            DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime JoinDate { get; set; }

        [EnumDataType(typeof(PlayerGrade))]
        public PlayerGrade? PlayerGrade { get; set; }

        public String PicturePath { get; set; }
        public HttpPostedFileBase Picture { get; set; }
        //Foreign key
        [ForeignKey("Sports")]
        public int SportsId { get; set; }
        //Navigation property
        public virtual Sports Sports { get; set; }
    }
}