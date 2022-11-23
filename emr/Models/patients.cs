using System.ComponentModel.DataAnnotations;

namespace emr.Models
{
    public class patients
    {
        [Key]
        [Required]
        public int id { get; set; }
        public DateTime created { get; set; }
        public int creator_id { get; set; }
        public DateTime modified { get; set; }
        public int modifier_id { get; set; }
        public string? medical_number { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? dob { get; set; }
        public string? gender { get; set; }
        public string? description { get; set; }
        public int course_id { get; set; }
        public int provider_id { get; set; }
        public bool active { get; set; }
        public bool locks { get; set; }
    }
}
