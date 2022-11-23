using emr.Models.Model;

namespace emr.Services
{
    public interface IRooms
    {
        /// <summary>
        /// get Rooms by id  info 
        /// </summary>
        /// <returns></returns>
        RoomsModel GetRoomsById(int id);

        /// <summary>
        /// To update Rooms information
        /// </summary>
        /// <returns></returns>
        int UpdateRoomsInfo(RoomsModel model);

        /// <summary>
        /// delete a Rooms info 
        /// </summary>
        /// <returns></returns>
        RoomsModel DeleteRoomsById(int id, int userId);
    }
}
