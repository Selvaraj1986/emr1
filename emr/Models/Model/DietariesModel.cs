namespace emr.Models.Model
{
    public class DietariesModel
    {
        public int id { get; set; }
        public DateTime created { get; set; }
        public int creator_id { get; set; }
        public DateTime modified { get; set; }
        public int modifier_id { get; set; }
        public string? record_date { get; set; }
        public int patient_id { get; set; }
        public string? type { get; set; }
        public string? notes { get; set; }
        public string? creator { get; set; }
        public string? modifier { get; set; }
    }
}
