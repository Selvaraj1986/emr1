using emr.Models.Model;

namespace emr.Services
{
    public interface IWeights
    {
        /// <summary>
        /// get Weights by id  info 
        /// </summary>
        /// <returns></returns>
        WeightsModel GetWeightsById(int id);

        /// <summary>
        /// To update Weights information
        /// </summary>
        /// <returns></returns>
        int UpdateWeightsInfo(WeightsModel model);

        /// <summary>
        /// delete a Weights info 
        /// </summary>
        /// <returns></returns>
        WeightsModel DeleteWeightsById(int id, int userId);
    }
}
