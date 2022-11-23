using emr.Models;
using emr.Models.Model;
using emr.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace emr.Controllers
{
    public class ProvidersController : Controller
    {
        private readonly IProviders _providers;

        public ProvidersController(IProviders providers)
        {
            _providers = providers;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetAll()
        {
            int courseId= Convert.ToInt32(HttpContext.Session.GetString("courseId"));
            var result = _providers.GetProvidersAll(courseId);
            return Json(result);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ProvidersModel model)
        {
            if (ModelState.IsValid)
            {
                var providers = new providers()
                {
                    name = model.name,
                    notes = model.notes
                };
                providers.creator_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
                providers.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
                providers.course_id = Convert.ToInt32(HttpContext.Session.GetString("courseId"));

                _providers.SaveProvidersInfo(providers);
                return RedirectToAction("Index", "Providers");
            }

            return View();

        }
        // [Route("{id?}")]
        public IActionResult View(int id)
        {
            var result = _providers.GetProvidersById(id);
            return View(result);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ProvidersModel model = _providers.GetProvidersById(id);
            if (model != null)
            {
                return View(model);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ProvidersModel model)
        {
            //if (ModelState.IsValid)
            //{

            model.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            _providers.UpdateProvidersInfo(model);
            // }
            return RedirectToAction("Index", "Providers");
        }

        public IActionResult Delete(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            bool status = _providers.DeleteProvidersInfoById(id, userId);
            if (status)
            {
                return RedirectToAction("Index", "Providers");
            }
            return View();
        }
    }
}
