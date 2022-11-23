namespace emr.Models.Model
{
    public class AllergyModel
    {
        public int id { get; set; }
        public DateTime created { get; set; }
        public int creator_id { get; set; }
        public DateTime modified { get; set; }
        public int modifier_id { get; set; }
        public DateTime record_date { get; set; }
        public int patient_id { get; set; }
        public string? allergy_type { get; set; }
        public string? allergy { get; set; }
        public string? creator { get; set; }
        public string? modifier { get; set; }
    }
}
