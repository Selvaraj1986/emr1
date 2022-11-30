using emr.Models;
using emr.Models.Model;

namespace emr.Services
{
    public interface IObAdmission
    {
        /// <summary>
        /// To save patients information
        /// </summary>
        /// <returns></returns>
        long SaveobAdmissionInfo(ObAdmissionsModel model);

        /// <summary>
        /// To save update information
        /// </summary>
        /// <returns></returns>
        long UpdateobAdmissionInfo(ObAdmissionsModel model);

        /// <summary>
        /// get ob Admissions information
        /// </summary>
        /// <returns></returns>
        List<ObAdmissionsModel> GetobAdmissionsAll(int id);

        /// <summary>
        /// view ob Admissions information
        /// </summary>
        /// <returns></returns>
        ObAdmissionsModel ViewobAdmissionsById(int id);

        /// <summary>
        ///  get ob Admissions information by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ObAdmissionsModel GetobAdmissionsById(int id);

        /// <summary>
        ///  delete ob Admissions information by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteObAdmissionById(int id, int userId);
    }
}
