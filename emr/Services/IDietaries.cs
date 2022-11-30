using emr.Models.Model;

namespace emr.Services
{
    public interface IDietaries
    {
        /// <summary>
        /// get Dietaries by id  info 
        /// </summary>
        /// <returns></returns>
        DietariesModel GetDietariesById(int id);

        /// <summary>
        /// To update Dietaries information
        /// </summary>
        /// <returns></returns>
        int UpdateDietariesInfo(DietariesModel model);

        /// <summary>
        /// delete a Dietaries info 
        /// </summary>
        /// <returns></returns>
        DietariesModel DeleteDietariesById(int id, int userId);
    }
}
