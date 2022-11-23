using emr.Models.Model;

namespace emr.Services
{
    public interface IAllergy
    {
        /// <summary>
        /// get Allergy by id  info 
        /// </summary>
        /// <returns></returns>
        AllergyModel GetAllergyById(int id);


        /// <summary>
        /// To update Allergy information
        /// </summary>
        /// <returns></returns>
        int UpdateAllergyInfo(AllergyModel model);

        /// <summary>
        /// delete a Allergy info 
        /// </summary>
        /// <returns></returns>
        AllergyModel DeleteAllergyById(int id, int userId);
    }
}

