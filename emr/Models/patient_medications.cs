namespace emr.Models
{
    public class patient_medications
    {
        public int id { get; set; }
        public DateTime created { get; set; }
        public int creator_id { get; set; }
        public DateTime modified { get; set; }
        public int modifier_id { get; set; }
        public int patient_id { get; set; }
        public DateTime record_date { get; set; }
        public string? medication { get; set; }
        public string? dose { get; set; }
        public string? frequency { get; set; }
        public bool taken_today { get; set; }
        public bool brought_with { get; set; }
        public bool active { get; set; }
    }
}
