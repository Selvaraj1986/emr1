using System.ComponentModel.DataAnnotations;

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
        public string? height { get; set; }
        public string? weight { get; set; }
        public string? rooms { get; set; }
        public string? code { get; set; }
        public string? diagosis { get; set; }
        public string? allergy { get; set; }
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
        public TreatmentsModel treatmentsModel { get; set; } = null!;
        public List<TreatmentsModel> treatmentsModelAll { get; set; } = null!;
        public ConsultsModel consultsModel { get; set; } = null!;
        public List<ConsultsModel> consultsModelAll { get; set; } = null!;
        public DietariesModel dietariesModel { get; set; } = null!;
        public List<DietariesModel> dietariesModelAll { get; set; } = null!;
        public ProviderOrdersModel providerOrdersModel { get; set; } = null!;
        public List<ProviderOrdersModel> providerOrdersModelAll { get; set; } = null!;
        public NotesModel notesModel { get; set; } = null!;
        public List<NotesModel> notesModelAll { get; set; } = null!;

        public AdmissionsModel admissionsModel { get; set; } = null!;
        public List<AdmissionsModel> admissionsModelAll { get; set; } = null!;
        public ObAdmissionsModel obAdmissionsModel { get; set; } = null!;
        public List<ObAdmissionsModel> obAdmissionsModelAll { get; set; } = null!;



    }
}
