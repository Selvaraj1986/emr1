using System.ComponentModel.DataAnnotations;

namespace emr.Models
{
    public class roles
    {
        [Key]
        [Required]
        public int id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public bool active { get; set; }
        public bool account_id { get; set; }
    }
}
