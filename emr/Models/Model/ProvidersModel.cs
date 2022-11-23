using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace emr.Models.Model
{
    public class ProvidersModel
    {
        public int id { get; set; }
        public DateTime? created { get; set; }
        public int? creator_id { get; set; }
        public DateTime? modified { get; set; }
        public int? modifier_id { get; set; }
        [Required(ErrorMessage = "Please enter Name")]
        [DisplayName("Name")]
        public string? name { get; set; }
        [Required(ErrorMessage = "Please enter Notes")]
        [DisplayName("Notes")]
        public string? notes { get; set; }
        public string? creator { get; set; }
        public string? modifier { get; set; }
    }
    public class ProvidersSaveModel
    {
        [Required(ErrorMessage = "Please enter Name")]
        [DisplayName("Name")]
        public string? name { get; set; }
        [Required(ErrorMessage = "Please enter Notes")]
        [DisplayName("Notes")]
        public string? notes { get; set; }
    }
}
