namespace emr.Models
{
    public class heights
    {
        public int id { get; set; }
        public DateTime created { get; set; }
        public int creator_id { get; set; }
        public DateTime modified { get; set; }
        public int modifier_id { get; set; }
        public DateTime record_date { get; set; }
        public int patient_id { get; set; }
        public double height { get; set; }
        public string? height_unit { get; set; }
        public bool active { get; set; }
    }
}
