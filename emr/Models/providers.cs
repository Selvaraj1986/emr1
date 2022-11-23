using System.ComponentModel.DataAnnotations;

namespace emr.Models
{
    public class providers
    {
        [Key]
        [Required]
        public int id { get; set; }
        public DateTime? created { get; set; }
        public int? creator_id { get; set; }
        public DateTime? modified { get; set; }
        public int? modifier_id { get; set; }
        public string? name { get; set; }
        public string? notes { get; set; }
        public int? course_id { get; set; }
        public bool active { get; set; }
    }
}
