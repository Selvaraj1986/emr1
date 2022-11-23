using System.ComponentModel.DataAnnotations;

namespace emr.Models
{
    public class course_people
    {
        [Key]
        [Required]
        public int id { get; set; }
        public int person_id { get; set; }
        public int course_id { get; set; }
        public int role_id { get; set; }
        public DateTime last_login { get; set; }
        public bool active { get; set; }
        public DateTime created { get; set; }
        public int creator_id { get; set; }
        public DateTime modified { get; set; }
        public int modifier_id { get; set; }
    }
}
