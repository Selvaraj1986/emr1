using emr.Models.DBContext;
using emr.Support;
using LtiLibrary.NetCore.Lis.v2;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using System.Net;
using System.Text;

namespace emr.Models.Access_Logs
{
    public class LoggingFilterAttribute : ActionFilterAttribute
    {
        private readonly EmrDbContext _dbContext;
        private IConfiguration _iconfiguration;
        public LoggingFilterAttribute(EmrDbContext dbContext, IConfiguration iconfiguration)
        {
            _dbContext = dbContext;
            _iconfiguration = iconfiguration;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                //var request = filterContext.HttpContext.Request;
                //string requestBody;
                //using (StreamReader reader = new StreamReader(request.Body))
                //{
                //    requestBody = Task.Run(reader.ReadToEndAsync).Result;
                //}
                string hostName = Dns.GetHostName();
                Console.WriteLine(hostName);
                // Get the IP
                // string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
                var logs = new access_logs();

                var userId = filterContext.HttpContext.Session.GetString("userId");
                var courseId = filterContext.HttpContext.Session.GetString("courseId");
                if (userId != null)
                {
                    logs.user_id = Convert.ToInt32(userId);
                }
                if (courseId != null)
                {
                    logs.course_id = Convert.ToInt32(courseId);
                }

                StringBuilder record = new StringBuilder();
                foreach (var parameter in filterContext.ActionArguments)
                {
                    record.Append(parameter.Value);
                }
                logs.controller = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)filterContext.ActionDescriptor).ControllerName;
                logs.action = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)filterContext.ActionDescriptor).ActionName;
                logs.record = record.ToString();
                //if (logs.controller == Level.Account.ToString() && logs.action == Level.Login.ToString())
                //{
                //    logs.data = filterContext.HttpContext.Session.GetString("payLoad");
                //}
                logs.page = null;
                logs.created = DateTime.Now;
                logs.ip_address = myIP;
                logs.ip_address_proxy = null;
                _dbContext.Add(logs);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());
            }
        }
        enum Level
        {
            Account,    // 0
            Login,   // 1

        }
    }

}



