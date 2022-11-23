using emr.Models;
using emr.Models.Model;

namespace emr.Services
{
    public interface IMedications
    {
        /// <summary>
        /// To save medication information
        /// </summary>
        /// <returns></returns>
        int SaveMedicationInfo(medications model);

        /// <summary>
        /// To save update information
        /// </summary>
        /// <returns></returns>
        int UpdateMedicationInfo(medications model);

        /// <summary>
        /// get medication information
        /// </summary>
        /// <returns></returns>
        object GetMedicationsAll();

        /// <summary>
        /// get medications info 
        /// </summary>
        /// <returns></returns>
        MedicationsModel GetMedicationsById(int id);

        /// <summary>
        /// delete a medications info 
        /// </summary>
        /// <returns></returns>
        bool DeleteMedicationInfoById(int id, int userId);

        /// <summary>
        /// get form dosage info 
        /// </summary>
        /// <returns></returns>
        List<FormDosageModel> GetFormDosageAll();

        /// <summary>
        /// To save dosage information
        /// </summary>
        /// <returns></returns>
        int SaveDosageInfo(dosages model);

        /// <summary>
        /// get dosage information
        /// </summary>
        /// <returns></returns>
        List<DosagesModel> GetDosageAll(int id);
    }

}
