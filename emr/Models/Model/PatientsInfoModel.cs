namespace emr.Models.Model
{
    public class PatientsInfoModel
    {
        public int id { get; set; }
        public string? medical_number { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? dob { get; set; }
        public string? gender { get; set; }
        public int provider_id { get; set; }
        public string? provider { get; set; }
        public string? height { get; set; }
        public string? weight { get; set; }
        public string? rooms { get; set; }
        public string? code { get; set; }
        public string? diagosis { get; set; }
        public string? allergy { get; set; }
    }
}
