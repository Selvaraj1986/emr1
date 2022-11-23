using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace emr.Models
{
    public class medications
    {
        [Key]
        [Required]
        public int id { get; set; }
        public DateTime? created { get; set; }
        public int? creator_id { get; set; }
        public DateTime? modified { get; set; }
        public int? modifier_id { get; set; }
        // [Required(ErrorMessage = "Please enter Generic")]
        //[DisplayName("Generic")]
        public string? generic { get; set; }
        // [Required(ErrorMessage = "Please enter Brand")]
        //[DisplayName("Brand")]
        public string? brand { get; set; }
        //[Required(ErrorMessage = "Please enter Classification")]
        //[DisplayName("Classification")]
        public string? classification { get; set; }
        public bool active { get; set; }
    }
}
