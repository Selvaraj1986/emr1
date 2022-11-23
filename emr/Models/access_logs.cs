using System.ComponentModel.DataAnnotations;

namespace emr.Models
{
    public class access_logs
    {
        [Key]
        [Required]
        public int id { get; set; }
        public int user_id { get; set; }
        public int course_id { get; set; }
        public string? controller { get; set; }
        public string? action { get; set; }
        public string? record { get; set; }
        public string? data { get; set; }
        public string? page { get; set; }
        public DateTime created { get; set; }
        public string? ip_address { get; set; }
        public string? ip_address_proxy { get; set; }
    }
}