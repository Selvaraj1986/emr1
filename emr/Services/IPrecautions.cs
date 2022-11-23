using emr.Models.Model;

namespace emr.Services
{
    public interface IPrecautions
    {
        /// <summary>
        /// get Precautions by id  info 
        /// </summary>
        /// <returns></returns>
        PrecautionsModel GetPrecautionsById(int id);

        /// <summary>
        /// To update Precautions information
        /// </summary>
        /// <returns></returns>
        int UpdatePrecautionsInfo(PrecautionsModel model);

        /// <summary>
        /// delete a Precautions info 
        /// </summary>
        /// <returns></returns>
        PrecautionsModel DeletePrecautionsById(int id, int userId);
    }
}
