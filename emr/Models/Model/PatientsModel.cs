namespace emr.Models.Model
{
    public class PatientsModel
    {
        public int id { get; set; }
        public string? medical_number { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? dob { get; set; }
        public string? gender { get; set; }
        public int provider_id { get; set; }
        public string? provider { get; set; }
        public string? description { get; set; }
        public bool locks { get; set; }
        public DateTime? created { get; set; }
        public string? creator { get; set; }
        public DateTime? modified { get; set; }
        public string? modifier { get; set; }
        public int? creator_id { get; set; }
        public int modifier_id { get; set; }
        public AdmittingDiagnosesModel admittingDiagnosesModel { get; set; } = null!;
        public List<AdmittingDiagnosesModel> diagnosesModelAll { get; set; } = null!;
        public CodeStatusesModel codeStatusesModel { get; set; } = null!;
        public List<CodeStatusesModel> codeStatusesModelAll { get; set; } = null!;
        public PrecautionsModel precautionsModel { get; set; } = null!;
        public List<PrecautionsModel> precautionsModelAll { get; set; } = null!;
        public AllergyModel allergyModel { get; set; } = null!;
        public List<AllergyModel> allergyModelAll { get; set; } = null!;

        public PatientMedicationsModel patientMedicationsModel { get; set; } = null!;
        public List<PatientMedicationsModel> patientMedicationsModelAll { get; set; } = null!;
        public RoomsModel roomsModel { get; set; } = null!;
        public List<RoomsModel> roomsModelAll { get; set; } = null!;
        public HeightsModel heightsModel { get; set; } = null!;
        public List<HeightsModel> heightsModelAll { get; set; } = null!;
        public WeightsModel weightsModel { get; set; } = null!;
        public List<WeightsModel> weightsModelAll { get; set; } = null!;
        public DailyActivitiesModel dailyActivitiesModel { get; set; } = null!;
        public List<DailyActivitiesModel> dailyActivitiesModelAll { get; set; } = null!;

    }
}
