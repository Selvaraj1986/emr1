using emr.Models.Model;
using emr.Models;

namespace emr.Services
{
    public interface IPatients
    {
        /// <summary>
        /// To save patients information
        /// </summary>
        /// <returns></returns>
        int SavepatientsInfo(patients model);

        /// <summary>
        /// To save update information
        /// </summary>
        /// <returns></returns>
        int UpdatepatientsInfo(PatientsModel model);

        /// <summary>
        /// get patients information
        /// </summary>
        /// <returns></returns>
        object GetpatientsAll(int courseId);

        /// <summary>
        /// get gender info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<GenderModel> GetGender();

        /// <summary>
        /// save patients
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void PatientsInsertDefault(int courseId);

        /// <summary>
        /// lock the patient 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void ToggleLock(int id);

        /// <summary>
        /// get medications by id
        /// </summary>
        /// <returns></returns>
        PatientsModel GetpatientsById(int id);

        /// <summary>
        /// delete a Patient info 
        /// </summary>
        /// <returns></returns>
        int DeletePatientsById(int id, int userId);

        /// store the diagnoses info 
        /// </summary>
        /// <param name="diagnoses"></param>
        /// <returns></returns>
        int SaveDiangnosesInfo(AdmittingDiagnosesModel model);

        /// <summary>
        /// get diagnosis info 
        /// </summary>
        /// <returns></returns>
        List<AdmittingDiagnosesModel> GetDiagnosisAll(int id);

        /// <summary>
        /// get CodeStatus info 
        /// </summary>
        /// <returns></returns>
        int SaveCodeStatusInfo(CodeStatusesModel model);

        /// <summary>
        /// get CodeStatus info 
        /// </summary>
        /// <returns></returns>
        List<CodeStatusesModel> GetCodeStatusAll(int id);

        /// <summary>
        /// get Precautions info 
        /// </summary>
        /// <returns></returns>
        int SavePrecautionsInfo(PrecautionsModel model);

        /// <summary>
        /// get Precautions info 
        /// </summary>
        /// <returns></returns>
        List<PrecautionsModel> GetPrecautionsAll(int id);

        /// <summary>
        /// get Allergy info 
        /// </summary>
        /// <returns></returns>
        int SaveAllergyInfo(AllergyModel model);

        /// <summary>
        /// get Allergy info 
        /// </summary>
        /// <returns></returns>
        List<AllergyModel> GetAllergyAll(int id);

        /// <summary>
        /// get PatientMedications info 
        /// </summary>
        /// <returns></returns>
        int SavePatientMedicationsInfo(PatientMedicationsModel model);

        /// <summary>
        /// get PatientMedications info 
        /// </summary>
        /// <returns></returns>
        List<PatientMedicationsModel> GetPatientMedicationsAll(int id);


        /// <summary>
        /// get Rooms info 
        /// </summary>  
        /// <returns></returns>
        int SaveRoomsInfo(RoomsModel model);

        /// <summary>
        /// get Rooms info 
        /// </summary>
        /// <returns></returns>
        List<RoomsModel> GetRoomsAll(int id);

        /// <summary>
        /// get Height info 
        /// </summary>  
        /// <returns></returns>
        int SaveHeightInfo(HeightsModel model);

        /// <summary>
        /// get Height info 
        /// </summary>
        /// <returns></returns>
        List<HeightsModel> GetHeightAll(int id);

        /// <summary>
        /// get Weights info 
        /// </summary>  
        /// <returns></returns>
        int SaveWeightsInfo(WeightsModel model);

        /// <summary>
        /// get Weights info 
        /// </summary>
        /// <returns></returns>
        List<WeightsModel> GetWeightsAll(int id);

        /// <summary>
        /// get DailyActivities info 
        /// </summary>  
        /// <returns></returns>
        int SaveDailyActivitiesInfo(DailyActivitiesModel model);

        /// <summary>
        /// get DailyActivities info 
        /// </summary>
        /// <returns></returns>
        List<DailyActivitiesModel> GetDailyActivitiesAll(int id);

        /// <summary>
        /// get Treatments info 
        /// </summary>  
        /// <returns></returns>
        int SaveTreatmentsInfo(TreatmentsModel model);

        /// <summary>
        /// get Treatments info 
        /// </summary>
        /// <returns></returns>
        List<TreatmentsModel> GetTreatmentsAll(int id);

        /// <summary>
        /// get Consults info 
        /// </summary>  
        /// <returns></returns>
        int SaveConsultsInfo(ConsultsModel model);

        /// <summary>
        /// get Consults info 
        /// </summary>
        /// <returns></returns>
        List<ConsultsModel> GetConsultsAll(int id);

        /// <summary>
        /// get Dietaries info 
        /// </summary>  
        /// <returns></returns>
        int SaveDietariesInfo(DietariesModel model);

        /// <summary>
        /// get Dietaries info 
        /// </summary>
        /// <returns></returns>
        List<DietariesModel> GetDietariesAll(int id);

        /// <summary>
        /// get ProviderOrders info 
        /// </summary>  
        /// <returns></returns>
        int SaveProviderOrdersInfo(ProviderOrdersModel model);

        /// <summary>
        /// get ProviderOrders info 
        /// </summary>
        /// <returns></returns>
        List<ProviderOrdersModel> GetProviderOrdersAll(int id);

        /// <summary>
        /// get Notes info 
        /// </summary>  
        /// <returns></returns>
        int SaveNotesInfo(NotesModel model);

        /// <summary>
        /// get Notes info 
        /// </summary>
        /// <returns></returns>
        List<NotesModel> GetNotesAll(int id);

    }
}
