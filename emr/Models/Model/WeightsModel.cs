namespace emr.Models.Model
{
    public class WeightsModel
    {
        public int id { get; set; }
        public DateTime created { get; set; }
        public int creator_id { get; set; }
        public DateTime modified { get; set; }
        public int modifier_id { get; set; }
        public DateTime record_date { get; set; }
        public int patient_id { get; set; }
        public double weight { get; set; }
        public string? weight_unit { get; set; }
        public string? creator { get; set; }
        public string? modifier { get; set; }
    }
}
