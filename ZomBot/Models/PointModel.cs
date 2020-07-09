using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ZomBot.Models
{
    class PointModel
    {
        [Key]
        public int Point_Id { get; set; }
        public long User_Id { get; set; }
        public string User { get; set; }
        public long Points { get; set; }
        public string Guild { get; set; }
    }
}
