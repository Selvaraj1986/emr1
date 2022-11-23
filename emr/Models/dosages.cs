using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace emr.Models
{
    public class dosages
    {
        public int id { get; set; }
        public DateTime? created { get; set; }
        public int? creator_id { get; set; }
        public DateTime? modified { get; set; }
        public int? modifier_id { get; set; }
        public int? medication_id { get; set; }
        //[Required(ErrorMessage = "Please enter Name")]
        //[DisplayName("Name")]
        public string? name { get; set; }
        //[Required(ErrorMessage = "Please enter Ingredient")]
        //[DisplayName("Ingredient")]
        public string? ingredient { get; set; }
        public string? form { get; set; }
        //[Required(ErrorMessage = "Please enter Dosage")]
        //[DisplayName("Dosage")]
        public string? dosage { get; set; }
        public string? default_dose { get; set; }
        public string? default_dose_unit { get; set; }
        public string? default_frequency { get; set; }
        public string? default_rate { get; set; }
        public string? default_rate_unit { get; set; }
        public string? default_route { get; set; }
        public string? default_schedule { get; set; }
        public string? default_times { get; set; }
        public string? default_times_unit { get; set; }
        public string? notes { get; set; }
        public bool active { get; set; }

    }
}
