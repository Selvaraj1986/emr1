using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace emr.Models.Model
{
    public class DosagesModel
    {
        public int id { get; set; }
        public DateTime? created { get; set; }
        public int? creator_id { get; set; }
        public DateTime? modified { get; set; }
        public int? modifier_id { get; set; }
        public int? medication_id { get; set; }
        [Required(ErrorMessage = "Please enter Name")]
        [DisplayName("Name")]
        public string? name { get; set; }
        [Required(ErrorMessage = "Please enter Ingredient")]
        [DisplayName("Ingredient")]
        public string? ingredient { get; set; }
        [Required(ErrorMessage = "Please enter Form")]
        [DisplayName("Form")]
        public string? form { get; set; }
        [Required(ErrorMessage = "Please enter Dosage")]
        [DisplayName("Dosage")]
        public string? dosage { get; set; }
    }
    public class DosagesSaveModel
    {
        [Required(ErrorMessage = "Please enter Name")]
        [DisplayName("Name")]
        public string? name { get; set; }
        [Required(ErrorMessage = "Please enter Ingredient")]
        [DisplayName("Ingredient")]
        public string? ingredient { get; set; }
        [Required(ErrorMessage = "Please enter Form")]
        [DisplayName("Form")]
        public string? form { get; set; }
        [Required(ErrorMessage = "Please enter Dosage")]
        [DisplayName("Dosage")]
        public string? dosage { get; set; }
    }
}