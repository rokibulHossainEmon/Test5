using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models.ViewModel
{
    public class SportsViewModel
    {
        public int SportsId { get; set; }
        [Required, StringLength(50, ErrorMessage = "Sports name is required!!"), Display(Name = "Sports Name")]
        public string SportsName { get; set; }
        public int  playerCount {get;set;}
    }
}