using emr.Models.Model;

namespace emr.Services
{
    public interface IConsults
    {
        /// <summary>
        /// get Consults by id  info 
        /// </summary>
        /// <returns></returns>
        ConsultsModel GetConsultsById(int id);

        /// <summary>
        /// To update Consults information
        /// </summary>
        /// <returns></returns>
        int UpdateConsultsInfo(ConsultsModel model);

        /// <summary>
        /// delete a Consults info 
        /// </summary>
        /// <returns></returns>
        ConsultsModel DeleteConsultsById(int id, int userId);
    }
}
