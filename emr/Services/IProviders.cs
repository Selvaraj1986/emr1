using emr.Models.Model;
using emr.Models;

namespace emr.Services
{
    public interface IProviders
    {
        /// <summary>
        /// To save providers information
        /// </summary>
        /// <returns></returns>
        int SaveProvidersInfo(providers model);

        /// <summary>
        /// To save update information
        /// </summary>
        /// <returns></returns>
        int UpdateProvidersInfo(ProvidersModel model);

        /// <summary>
        /// get providers information
        /// </summary>
        /// <returns></returns>
        object GetProvidersAll(int courseId);

        /// <summary>
        /// get providers info 
        /// </summary>
        /// <returns></returns>
        ProvidersModel GetProvidersById(int id);

        /// <summary>
        /// delete a providers info 
        /// </summary>
        /// <returns></returns>
        bool DeleteProvidersInfoById(int id, int userId);
        /// <summary>
        /// save providers information
        /// </summary>
        /// <returns></returns>
        void ProvidersInsertDefault(int courseId);
        /// <summary>
        /// get providers information
        /// </summary>
        /// <returns></returns>
        List<providers> GetProviders(int courseId);
        
        
    }
}
