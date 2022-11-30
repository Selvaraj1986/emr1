using emr.Models.Model;

namespace emr.Services
{
    public interface ITreatments
    {
        /// <summary>
        /// get Treatments by id  info 
        /// </summary>
        /// <returns></returns>
        TreatmentsModel GetTreatmentsById(int id);

        /// <summary>
        /// To update Treatments information
        /// </summary>
        /// <returns></returns>
        int UpdateTreatmentsInfo(TreatmentsModel model);

        /// <summary>
        /// delete a Treatments info 
        /// </summary>
        /// <returns></returns>
        TreatmentsModel DeleteTreatmentsById(int id, int userId);
    }
}
