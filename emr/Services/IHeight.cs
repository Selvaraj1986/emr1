using emr.Models.Model;
using emr.Models;

namespace emr.Services
{
    public interface IHeight
    {
        /// <summary>
        /// get Height by id  info 
        /// </summary>
        /// <returns></returns>
        HeightsModel GetHeightById(int id);

        /// <summary>
        /// To update Heights information
        /// </summary>
        /// <returns></returns>
        int UpdateHeightInfo(HeightsModel model);

        /// <summary>
        /// delete a Heights info 
        /// </summary>
        /// <returns></returns>
        HeightsModel DeleteHeightById(int id, int userId);
    }
}
