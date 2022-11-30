using emr.Models.Model;

namespace emr.Services
{
    public interface INotes
    {
        /// <summary>
        /// get Notes by id  info 
        /// </summary>
        /// <returns></returns>
        NotesModel GetNotesById(int id);

        /// <summary>
        /// To update Notes information
        /// </summary>
        /// <returns></returns>
        int UpdateNotesInfo(NotesModel model);

        /// <summary>
        /// delete a Notes info 
        /// </summary>
        /// <returns></returns>
        NotesModel DeleteNotesById(int id, int userId);
    }
}
