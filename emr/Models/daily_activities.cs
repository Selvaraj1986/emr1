namespace emr.Models
{
    public class daily_activities
    {
        public int id { get; set; }
        public DateTime created { get; set; }
        public int creator_id { get; set; }
        public DateTime modified { get; set; }
        public int modifier_id { get; set; }
        public DateTime record_date { get; set; }
        public int patient_id { get; set; }
        public int bathing { get; set; }
        public int dressing { get; set; }
        public int grooming { get; set; }
        public int oral_care { get; set; }
        public int toileting { get; set; }
        public int transferring { get; set; }
        public int walking { get; set; }
        public int eating { get; set; }
        public string? notes { get; set; }
        public bool active { get; set; }

    }
}
