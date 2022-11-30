using emr.Models;
using emr.Models.Model;
using emr.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace emr.Controllers
{
    public class ProviderOrdersController : Controller
    {
        private readonly IProviderOrders _providerOrders;
        private readonly IProviders _providers;
        private readonly IPatients _patients;
        public ProviderOrdersController(IProviderOrders providerOrders, IPatients patients, IProviders providers)
        {
            _providerOrders = providerOrders;
            _patients = patients;
            _providers = providers;       
        }
        public IActionResult Edit(int id)
        {
            PatientsModel model = _patients.GetpatientsById(id);
            int courseId = Convert.ToInt32(HttpContext.Session.GetString("courseId"));
            var providers = _providers.GetProviders(courseId);
            List<SelectListItem> provider = new();
            foreach (var item in providers)
            {
                var data = new SelectListItem()
                {
                    Value = item.id.ToString(),
                    Text = item.name
                };
                provider.Add(data);
            }
            ViewBag.provider = provider;
            model.providerOrdersModel = _providerOrders.GetProviderOrdersById(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ProviderOrdersModel providerOrdersModel)
        {
            providerOrdersModel.modifier_id = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var pId = _providerOrders.UpdateProviderOrdersInfo(providerOrdersModel);
            return Redirect("~/Patients/View/" + pId + "/#provider_orders");
        }
        public IActionResult Delete(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
            var status = _providerOrders.DeleteProviderOrdersById(id, userId);
            if (status != null)
            {
                return Redirect("~/Patients/View/" + status.patient_id + "/#provider_orders");

            }
            return RedirectToAction("View", "Patients", new { id });
        }
    }

}

