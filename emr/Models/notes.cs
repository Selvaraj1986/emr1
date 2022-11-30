namespace emr.Models
{
    public class notes
    {
        public int id { get; set; }
        public DateTime created { get; set; }
        public int creator_id { get; set; }
        public DateTime modified { get; set; }
        public int modifier_id { get; set; }
        public DateTime record_date { get; set; }
        public int patient_id { get; set; }
        public string? note { get; set; }
        public bool active { get; set; }
    }
}
