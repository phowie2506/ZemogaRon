using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ZemogaRon.Filters;

namespace ZemogaRon.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "UserId required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Title required")]
        [StringLength(100,ErrorMessage = "{0} no lorger than {1} characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content required")]
        public string bodycontent{ get; set; }

        public DateTime creation { get; set; }

        public int state { get; set; }
        [ApiKeyAuth("writterpermissions")]
        public string comment { get; set; }
    }
}
