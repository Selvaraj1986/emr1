using emr.Models.Model;

namespace emr.Services
{
    public interface ICodeStatuses
    {
        /// <summary>
        /// get CodeStatuses by id  info 
        /// </summary>
        /// <returns></returns>
        CodeStatusesModel GetCodeStatusesById(int id);


        /// <summary>
        /// To update CodeStatuses information
        /// </summary>
        /// <returns></returns>
        int UpdateCodeStatusesInfo(CodeStatusesModel model);

        /// <summary>
        /// delete a CodeStatuses info 
        /// </summary>
        /// <returns></returns>
        CodeStatusesModel DeleteCodeStatusesById(int id, int userId);
    }
}
