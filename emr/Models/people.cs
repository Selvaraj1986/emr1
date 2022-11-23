using System.ComponentModel.DataAnnotations;

namespace emr.Models
{
    public class people
    {
        [Key]
        [Required]
        public int id { get; set; }
        public int canvas_id { get; set; }
        public string? sis_id { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? username { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? note { get; set; }
        public DateTime created { get; set; }
        public DateTime modified { get; set; }
        public int creator_id { get; set; }
        public int modifier_id { get; set; }
        public bool active { get; set; }
        public int user_id { get; set; }
        public int account_id { get; set; }
    }
}
