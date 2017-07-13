using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ZomBot.Models
{
    class LevelModel
    {
        [Key]
        public int Level_Id { get; set; }
        public string User { get; set; }
        public long Messages { get; set; }
        public int Level { get; set; }
        public string Guild { get; set; }
    }
}
