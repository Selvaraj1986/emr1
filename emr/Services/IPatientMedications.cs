using emr.Models.Model;

namespace emr.Services
{
    public interface IPatientMedications
    {
        /// <summary>
        /// get PatientMedications by id  info 
        /// </summary>
        /// <returns></returns>
        PatientMedicationsModel GetPatientMedicationsById(int id);


        /// <summary>
        /// To update PatientMedications information
        /// </summary>
        /// <returns></returns>
        int UpdatePatientMedicationsInfo(PatientMedicationsModel model);

        /// <summary>
        /// delete a PatientMedications info 
        /// </summary>
        /// <returns></returns>
        PatientMedicationsModel DeletePatientMedicationsById(int id, int userId);
    }
}
