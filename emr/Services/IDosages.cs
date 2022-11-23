using emr.Models;
using emr.Models.Model;

namespace emr.Services
{
    public interface IDosages
    {
        /// <summary>
        /// get dosage by id  info 
        /// </summary>
        /// <returns></returns>
        dosages GetDosageById(int id);


        /// <summary>
        /// To update dosage information
        /// </summary>
        /// <returns></returns>
        int UpdateDosagesInfo(dosages model);

        /// <summary>
        /// delete a dosage info 
        /// </summary>
        /// <returns></returns>
        DosagesModel DeleteDosagesById(int id, int userId);
    }
}
