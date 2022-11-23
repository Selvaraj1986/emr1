using emr.Models.Model;

namespace emr.Services
{
    public interface IDailyActivities
    {
        /// <summary>
        /// get DailyActivities by id  info 
        /// </summary>
        /// <returns></returns>
        DailyActivitiesModel GetDailyActivitiesById(int id);


        /// <summary>
        /// To update DailyActivities information
        /// </summary>
        /// <returns></returns>
        int UpdateDailyActivitiesInfo(DailyActivitiesModel model);

        /// <summary>
        /// delete a DailyActivities info 
        /// </summary>
        /// <returns></returns>
        DailyActivitiesModel DeleteDailyActivitiesById(int id, int userId);
    }
}
