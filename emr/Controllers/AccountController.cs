using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using emr.Services;
using emr.Models;



namespace emr.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccounts _account;
        private readonly IProviders _providers;
        private readonly IPatients _patients;
        public AccountController(IAccounts account, IProviders providers, IPatients patients)
        {
            _account = account;
            _providers = providers;
            _patients = patients;
        }
        [AllowAnonymous]
        public IActionResult index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login()
        {
            Uri galenURL = new Uri("https://emr2.galencollege.dev/emr/login");
            string seckretToken = "a9e370e91200a5ae9c9eb1afcd2897a16b894849";

            var signedKey = LtiLibrary.NetCore.OAuth.OAuthRequest.GenerateSignature(Request.Method, galenURL, getForm(), seckretToken);
            var oauth_signature = Request.Form["oauth_signature"];

            if (signedKey == Request.Form["oauth_signature"].ToString())
            {

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }


        }
        public async Task<IActionResult> LogOut()
        {
            //SignOutAsync is Extension method for SignOut    
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //Redirect to home page    
            return LocalRedirect("/");
        }

        private NameValueCollection getForm()
        {
            var people = new people();
            var courses = new courses();
            var coursePeople = new course_people();
            var role = new roles();

            var oauth_nonce = Request.Form["oauth_nonce"];
            var oauth_timestamp = Request.Form["oauth_timestamp"];
            var oauth_version = Request.Form["oauth_version"];
            var oauth_consumer_key = Request.Form["oauth_consumer_key"];
            var oauth_signature_method = Request.Form["oauth_signature_method"];
            var context_id = Request.Form["context_id"];
            var context_label = Request.Form["context_label"];
            var context_title = Request.Form["context_title"];
            var custom_canvas_api_domain = Request.Form["custom_canvas_api_domain"];
            var custom_canvas_course_id = Request.Form["custom_canvas_course_id"];
            var custom_canvas_enrollment_state = Request.Form["custom_canvas_enrollment_state"];
            var custom_canvas_user_id = Request.Form["custom_canvas_user_id"];
            var custom_canvas_user_login_id = Request.Form["custom_canvas_user_login_id"];
            var custom_canvas_workflow_state = Request.Form["custom_canvas_workflow_state"];
            var ext_roles = Request.Form["ext_roles"];
            var launch_presentation_document_target = Request.Form["launch_presentation_document_target"];
            var launch_presentation_height = Request.Form["launch_presentation_height"];
            var launch_presentation_locale = Request.Form["launch_presentation_locale"];
            var launch_presentation_return_url = Request.Form["launch_presentation_return_url"];
            var launch_presentation_width = Request.Form["launch_presentation_width"];
            var lis_course_offering_sourcedid = Request.Form["lis_course_offering_sourcedid"];
            var lis_person_contact_email_primary = Request.Form["lis_person_contact_email_primary"];
            var lis_person_name_family = Request.Form["lis_person_name_family"];
            var lis_person_name_full = Request.Form["lis_person_name_full"];
            var lis_person_name_given = Request.Form["lis_person_name_given"];
            var lis_person_sourcedid = Request.Form["lis_person_sourcedid"];
            var lti_message_type = Request.Form["lti_message_type"];
            var lti_version = Request.Form["lti_version"];
            var oauth_callback = Request.Form["oauth_callback"];
            var resource_link_id = Request.Form["resource_link_id"];
            var resource_link_title = Request.Form["resource_link_title"];
            var roles = Request.Form["roles"];
            var tool_consumer_info_product_family_code = Request.Form["tool_consumer_info_product_family_code"];
            var tool_consumer_info_version = Request.Form["tool_consumer_info_version"];
            var tool_consumer_instance_contact_email = Request.Form["tool_consumer_instance_contact_email"];
            var tool_consumer_instance_guid = Request.Form["tool_consumer_instance_guid"];
            var tool_consumer_instance_name = Request.Form["tool_consumer_instance_name"];
            var user_id = Request.Form["user_id"];
            var user_image = Request.Form["user_image"];
            //People info
            people.canvas_id = Convert.ToInt32(custom_canvas_user_id);
            people.first_name = lis_person_name_given;
            people.last_name = lis_person_name_family;
            people.username = lis_person_name_given;
            people.email = lis_person_contact_email_primary;
            people.creator_id = 1;
            people.modifier_id = 1;
            people.created = DateTime.Now;
            people.modified = DateTime.Now;
            people.active = true;
            //check if people are exist
            var peopleInfo = _account.CheckPeopleExist(people.username, people.email);
            int peopleID = 0;
            if (peopleInfo == null)
            {
                peopleID = _account.SavePeopleInfo(people);
            }
            else
            {
                peopleID = peopleInfo.id;
            }
            //Courses info
            courses.canvas_id = Convert.ToInt32(custom_canvas_course_id);
            courses.name = context_title;
            courses.created = DateTime.Now;
            courses.modified = DateTime.Now;
            courses.active = true;
            int coursesID = 0;
            //check if courses are exist
            var coursesInfo = _account.CheckCoursesExist(courses.canvas_id);

            if (coursesInfo == null)
            {
                coursesID = _account.SaveCoursesInfo(courses);
                _providers.ProvidersInsertDefault(coursesID);
                _patients.PatientsInsertDefault(coursesID);
            }
            else
            {
                coursesID = coursesInfo.id;
            }
            //roles info
            string[] arrayRole = roles.ToString().Split(",");
            var roleName = arrayRole[0];
            role.name = roleName;
            role.active = true;
            role.account_id = false;
            //check if roles are exist
            var rolesInfo = _account.CheckRolesExist(role.name);
            int roleId = 0;
            if (rolesInfo == null)
            {
                roleId = _account.SaveRolesInfo(role);
            }
            else
            {
                roleId = rolesInfo.id;
            }
            //Course People info
            coursePeople.person_id = peopleID;
            coursePeople.course_id = coursesID;
            coursePeople.role_id = roleId;
            coursePeople.active = true;
            coursePeople.creator_id = 1;
            coursePeople.modifier_id = 1;
            coursePeople.last_login = DateTime.Now;
            coursePeople.created = DateTime.Now;
            coursePeople.modified = DateTime.Now;
            //check if coursepeople are exist
            var coursePeopleInfo = _account.CheckCoursePeopleExist(coursePeople.person_id, coursePeople.course_id, coursePeople.role_id);
            if (coursePeopleInfo == null)
            {
                _account.SaveCoursePeopleInfo(coursePeople);
            }

            //var claims = new List<Claim>() {
            //        new Claim(ClaimTypes.NameIdentifier, Convert.ToString(peopleID)),
            //            new Claim(ClaimTypes.Name, lis_person_name_given),
            //            new Claim(ClaimTypes.Role, role.name)
            //           // new Claim("FavoriteDrink", "Tea")
            //    };
            ////Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme    
            //var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ////Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
            //var principal = new ClaimsPrincipal(identity);
            ////SignInAsync is a Extension method for Sign in a principal for the specified scheme.    
            //HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
            //{
            //    IsPersistent = true
            //});
            NameValueCollection parametersIn = new NameValueCollection();
            parametersIn.Add("oauth_nonce", oauth_nonce);
            parametersIn.Add("oauth_timestamp", oauth_timestamp);
            parametersIn.Add("oauth_version", oauth_version);
            parametersIn.Add("oauth_consumer_key", oauth_consumer_key);
            parametersIn.Add("oauth_signature_method", oauth_signature_method);
            parametersIn.Add("context_id", context_id);

            parametersIn.Add("context_label", context_label);
            parametersIn.Add("context_title", context_title);
            parametersIn.Add("custom_canvas_api_domain", custom_canvas_api_domain);
            parametersIn.Add("custom_canvas_course_id", custom_canvas_course_id);

            parametersIn.Add("custom_canvas_enrollment_state", custom_canvas_enrollment_state);
            parametersIn.Add("custom_canvas_user_id", custom_canvas_user_id);
            parametersIn.Add("custom_canvas_user_login_id", custom_canvas_user_login_id);

            parametersIn.Add("custom_canvas_workflow_state", custom_canvas_workflow_state);
            parametersIn.Add("ext_roles", ext_roles);
            parametersIn.Add("launch_presentation_document_target", launch_presentation_document_target);
            parametersIn.Add("launch_presentation_height", launch_presentation_height);
            parametersIn.Add("launch_presentation_locale", launch_presentation_locale);
            parametersIn.Add("launch_presentation_return_url", launch_presentation_return_url);
            parametersIn.Add("launch_presentation_width", launch_presentation_width);
            parametersIn.Add("lis_course_offering_sourcedid", lis_course_offering_sourcedid);
            parametersIn.Add("lis_person_contact_email_primary", lis_person_contact_email_primary);
            parametersIn.Add("lis_person_name_family", lis_person_name_family);

            parametersIn.Add("lis_person_name_full", lis_person_name_full);
            parametersIn.Add("lis_person_name_given", lis_person_name_given);
            parametersIn.Add("lis_person_sourcedid", lis_person_sourcedid);
            parametersIn.Add("lti_message_type", lti_message_type);

            parametersIn.Add("lti_version", lti_version);
            parametersIn.Add("oauth_callback", oauth_callback);
            parametersIn.Add("resource_link_id", resource_link_id);
            parametersIn.Add("resource_link_title", resource_link_title);
            parametersIn.Add("roles", roles);
            parametersIn.Add("tool_consumer_info_product_family_code", tool_consumer_info_product_family_code);
            parametersIn.Add("tool_consumer_info_version", tool_consumer_info_version);
            parametersIn.Add("tool_consumer_instance_contact_email", tool_consumer_instance_contact_email);
            parametersIn.Add("tool_consumer_instance_guid", tool_consumer_instance_guid);
            parametersIn.Add("tool_consumer_instance_name", tool_consumer_instance_name);
            parametersIn.Add("user_id", user_id);
            parametersIn.Add("user_image", user_image);

            HttpContext.Session.SetString("personName", lis_person_name_full);
            HttpContext.Session.SetString("personEmail", lis_person_sourcedid);
            HttpContext.Session.SetString("userId", peopleID.ToString());
            HttpContext.Session.SetString("courseId", coursesID.ToString());
            return parametersIn;
        }
    }
}

