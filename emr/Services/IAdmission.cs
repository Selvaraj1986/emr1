using emr.Models.Model;
using emr.Models;

namespace emr.Services
{
    public interface IAdmission
    {
        /// <summary>
        /// To save Admission information
        /// </summary>
        /// <returns></returns>
        int SaveAdmissionInfo(AdmissionsModel model);

        /// <summary>
        /// To save Admission information
        /// </summary>
        /// <returns></returns>
        int UpdateAdmissionInfo(AdmissionsModel model);

        /// <summary>
        /// get Admission information
        /// </summary>
        /// <returns></returns>
        List<AdmissionsModel> GetAdmissionAll(int pId);

        /// <summary>
        /// get patients information
        /// </summary>
        /// <returns></returns>

        PatientsModel GetpatientsById(int id);
        /// <summary>
        /// get Admission info 
        /// </summary>
        /// <returns></returns>
        AdmissionsModel GetAdmissionById(int Admission);

        /// <summary>
        /// get Admission info 
        /// </summary>
        /// <returns></returns>
        AdmissionsModel ViewAdmissionById(int Admission);

        /// <summary>
        /// delete a Admission info 
        /// </summary>
        /// <returns></returns>
        AdmissionsModel DeleteAdmissionById(int id, int userId);
    }
}
