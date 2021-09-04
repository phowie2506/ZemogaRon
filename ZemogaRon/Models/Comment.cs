using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZemogaRon.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "PostId required")]
        public int PostId { get; set; }

        [Required(ErrorMessage = "comment required")]
        [StringLength(255, ErrorMessage = "{0} no lorger than {1} characters")]
        public string comment { get; set; }

        [Required(ErrorMessage = "User Id required")]
        public int userid { get; set; }
        public DateTime creation { get; set; }
    }
}
