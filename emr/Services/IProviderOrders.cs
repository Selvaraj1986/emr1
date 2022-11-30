using emr.Models.Model;

namespace emr.Services
{
    public interface IProviderOrders
    {
        /// <summary>
        /// get ProviderOrders by id  info 
        /// </summary>
        /// <returns></returns>
        ProviderOrdersModel GetProviderOrdersById(int id);

        /// <summary>
        /// To update ProviderOrders information
        /// </summary>
        /// <returns></returns>
        int UpdateProviderOrdersInfo(ProviderOrdersModel model);

        /// <summary>
        /// delete a ProviderOrders info 
        /// </summary>
        /// <returns></returns>
        ProviderOrdersModel DeleteProviderOrdersById(int id, int userId);
    }
}
