namespace emr.Models.Model
{
    public class DailyActivitiesModel
    {
        public int id { get; set; }
        public DateTime created { get; set; }
        public int creator_id { get; set; }
        public DateTime modified { get; set; }
        public int modifier_id { get; set; }
        public string? record_date { get; set; }
        public int patient_id { get; set; }
        public int bathing { get; set; }
        public int dressing { get; set; }
        public int grooming { get; set; }
        public int oral_care { get; set; }
        public int toileting { get; set; }
        public int transferring { get; set; }
        public int walking { get; set; }
        public string? notes { get; set; }
        public int eating { get; set; }
        public string? creator { get; set; }
        public string? modifier { get; set; }
        public string? bathingName { get; set; }
        public string? dressingName { get; set; }
        public string? groomingName { get; set; }
        public string? oral_careName { get; set; }
        public string? toiletingName { get; set; }
        public string? transferringName { get; set; }
        public string? walkingName { get; set; }
        public string? eatingName { get; set; }
    }
}
