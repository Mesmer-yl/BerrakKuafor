using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concretes
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser AppUser { get; set; }
        public string ImageUrl { get; set; }
        public string Detail { get; set; }
        public int HairdresserId { get; set; }
        [ForeignKey("HairdresserId")]
        public Hairdresser Hairdresser { get; set; }
        public DateTime CurrentTime { get; set; }
    }
}
