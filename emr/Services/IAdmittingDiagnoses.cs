using emr.Models;
using emr.Models.Model;

namespace emr.Services
{
    public interface IAdmittingDiagnoses
    {

        /// <summary>
        /// get Diagnosis by id  info 
        /// </summary>
        /// <returns></returns>
        AdmittingDiagnosesModel GetDiagnosisById(int id);


        /// <summary>
        /// To update Diagnosis information
        /// </summary>
        /// <returns></returns>
        int UpdateDiagnosisInfo(AdmittingDiagnosesModel model);

        /// <summary>
        /// delete a Diagnosis info 
        /// </summary>
        /// <returns></returns>
        AdmittingDiagnosesModel DeleteDiagnosisById(int id, int userId);
    }
}
