using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace emr.Models.Model
{
    public class MedicationsModel
    {
        public int id { get; set; }
        public DateTime? created { get; set; }
        public int? creator_id { get; set; }
        public DateTime? modified { get; set; }
        public int? modifier_id { get; set; }
        [Required(ErrorMessage = "Please enter Generic")]
        [DisplayName("Generic")]
        public string? generic { get; set; }
        [Required(ErrorMessage = "Please enter Brand")]
        [DisplayName("Brand")]
        public string? brand { get; set; }
        [Required(ErrorMessage = "Please enter Classification")]
        [DisplayName("Classification")]
        public string? classification { get; set; }
        public string? creator { get; set; }
        public string? modifier { get; set; }
        public bool active { get; set; }
        public dosages dosages { get; set; } = null!;
        public List<DosagesModel> dosageModel { get; set; }=null!;

    }
    public class MedicationsSaveModel
    {
        [Required(ErrorMessage = "Please enter Generic")]
        [DisplayName("Generic")]
        public string? generic { get; set; }
        [Required(ErrorMessage = "Please enter Brand")]
        [DisplayName("Brand")]
        public string? brand { get; set; }
        [Required(ErrorMessage = "Please enter Classification")]
        [DisplayName("Classification")]
        public string? classification { get; set; }
    }
    }
