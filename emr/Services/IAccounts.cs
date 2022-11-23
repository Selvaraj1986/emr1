using emr.Models;
using emr.Models.Model;

namespace emr.Services
{
    public interface IAccounts
    {
        /// <summary>
        /// To save course people information
        /// </summary>
        /// <returns></returns>
        int SaveCoursePeopleInfo(course_people model);
        /// <summary>
        /// check if CoursePeople is exist
        /// </summary>
        /// <returns></returns>
        course_people CheckCoursePeopleExist(int personId,int courseId,int roleId);
        /// <summary>
        /// To save courses information
        /// </summary>
        /// <returns></returns>
        int SaveCoursesInfo(courses model);
        /// <summary>
        /// check if Courses is exist
        /// </summary>
        /// <returns></returns>
        courses CheckCoursesExist(int id);
        /// <summary>
        /// To save people information
        /// </summary>
        /// <returns></returns>
        int SavePeopleInfo(people model);
        /// <summary>
        /// check if People is exist
        /// </summary>
        /// <returns></returns>
        people CheckPeopleExist(string name, string email);
        /// <summary>
        /// To save roles information
        /// </summary>
        /// <returns></returns>
        int SaveRolesInfo(roles model);
        /// <summary>
        /// check if Roles is exist
        /// </summary>
        /// <returns></returns>
        roles CheckRolesExist(string name);
        
       

    }
}
