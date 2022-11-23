using emr.Models.DBContext;
using emr.Models;
using emr.Support;
using System.Xml.Linq;
using emr.Models.Model;
using System.Text.Json;

namespace emr.Services
{
    public class Accounts : IAccounts
    {

        private readonly EmrDbContext _dbContext;
        private IConfiguration _iconfiguration;
        public Accounts(EmrDbContext dbContext, IConfiguration iconfiguration)
        {
            _dbContext = dbContext;
            _iconfiguration = iconfiguration;

        }
        /// <summary>
        /// store the course people 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveCoursePeopleInfo(course_people course_people)
        {
            int returnID = 0;
            try
            {
                _dbContext.Add(course_people);
                _dbContext.SaveChanges();
                returnID = course_people.id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }
        /// <summary>
        /// store the courses information 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveCoursesInfo(courses courses)
        {
            int returnID = 0;
            try
            {
                _dbContext.Add(courses);
                _dbContext.SaveChanges();

                returnID = courses.id;
            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());
            }
            return returnID;
        }

        /// <summary>
        /// store the course people 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SavePeopleInfo(people people)
        {
            int returnID = 0;
            try
            {
                _dbContext.Add(people);
                _dbContext.SaveChanges();
                people.user_id = people.id;
                _dbContext.SaveChanges();
                returnID = people.id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }

        /// <summary>
        /// store the course people 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveRolesInfo(roles role)
        {
            int returnID = 0;
            try
            {
                _dbContext.Add(role);
                _dbContext.SaveChanges();
                returnID = role.id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());
            }
            return returnID;
        }
        /// <summary>
        /// get exist course_people info 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public course_people CheckCoursePeopleExist(int personId, int courseId, int roleId)
        {
            var result = (from s in _dbContext.course_people where s.person_id == personId && s.course_id == courseId && s.role_id == roleId select s).FirstOrDefault();
            return result;
        }
        /// <summary>
        /// get exist people info 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public people CheckPeopleExist(string name, string email)
        {
            var result = (from s in _dbContext.people where s.username == name && s.email == email select s).FirstOrDefault();
            return result;
        }
        /// <summary>
        /// get exist courses info 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public courses CheckCoursesExist(int id)
        {
            var result = (from s in _dbContext.courses where s.canvas_id == id select s).FirstOrDefault();
            return result;
        }
        /// <summary>
        /// get exist roles info 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public roles CheckRolesExist(string name)
        {
            var result = (from s in _dbContext.roles where s.name == name select s).FirstOrDefault();
            return result;
        }
       
        
    }
}
