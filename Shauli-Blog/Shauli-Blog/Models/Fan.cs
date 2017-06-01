using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Shauli_Blog.Models
{
    public enum Gender { Male, Female }
    public class Fan
    {
        public int FanId { get; set; }
        
        public string FName { get; set; }
        public string LName { get; set; }
        public Gender Genre { get; set; }
        public DateTime BDate { get; set; }
        public int Seniority { get; set; }

    }
}