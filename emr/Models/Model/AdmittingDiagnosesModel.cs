using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace emr.Models.Model
{
    public class AdmittingDiagnosesModel
    {
        public int id { get; set; }
        //[Required(ErrorMessage = "Please enter Record Date")]
        //[DisplayName("Record Date")]
        public DateTime record_date { get; set; }
        //[Required(ErrorMessage = "Please enter Diagnosis")]
        //[DisplayName("Diagnosis")]
        public int patient_id { get; set; }
        public string? diagnosis { get; set; }
        
        public DateTime? created { get; set; }
        public int creator_id { get; set; }
        public DateTime? modified { get; set; }
        public int modifier_id { get; set; }
        public string? creator { get; set; }
        public string? modifier { get; set; }
    }
}
