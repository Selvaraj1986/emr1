using emr.Models.DBContext;
using emr.Models.Model;
using emr.Models;
using emr.Support;
using System.Globalization;
using AutoMapper;
using LtiLibrary.NetCore.Extensions;

namespace emr.Services
{
    public class Admission : IAdmission
    {

        private readonly EmrDbContext _dbContext;
        private IConfiguration _iconfiguration;
        public Admission(EmrDbContext dbContext, IConfiguration iconfiguration)
        {
            _dbContext = dbContext;
            _iconfiguration = iconfiguration;

        }

        /// <summary>
        /// get medications by id
        /// </summary>
        /// <returns></returns>
        public PatientsModel GetpatientsById(int id)
        {
            var result = new PatientsModel();
            try
            {
                result = (from p in _dbContext.patients
                          join pr in _dbContext.providers on p.provider_id equals pr.id into pp
                          from pr in pp
                          where p.id == id && p.active == true
                          orderby p.last_name ascending
                          select new PatientsModel
                          {
                              id = p.id,

                              medical_number = p.medical_number,
                              first_name = p.first_name,
                              last_name = p.last_name,
                              gender = p.gender,
                              dob = p.dob.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                              provider_id = p.provider_id,
                              provider = pr.name,
                          }).FirstOrDefault();
                var height = (from h in _dbContext.heights where h.patient_id == id select h).FirstOrDefault();
                var weight = (from h in _dbContext.weights where h.patient_id == id select h).FirstOrDefault();
                var code = (from h in _dbContext.code_statuses where h.patient_id == id select h).FirstOrDefault();
                var room = (from h in _dbContext.rooms where h.patient_id == id select h).FirstOrDefault();
                var diagnosis = (from h in _dbContext.admitting_diagnoses where h.patient_id == id select h).FirstOrDefault();
                var allergy = (from h in _dbContext.allergies where h.patient_id == id select h).FirstOrDefault();
                if (height != null)
                {
                    result.height = height.height + ' ' + height.height_unit;
                }
                if (weight != null)
                {
                    result.weight = weight.weight_unit + ' ' + weight.weight_unit;
                }
                if (code != null)
                {
                    result.code = code.code_status;
                }
                if (room != null)
                {
                    result.rooms = room.room;
                }
                if (diagnosis != null)
                {
                    result.diagosis = diagnosis.diagnosis;
                }
                if (allergy != null)
                {
                    result.allergy = allergy.allergy_type;
                }

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return result;
        }
        /// <summary>
        /// store the Admission info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveAdmissionInfo(AdmissionsModel model)
        {
            int returnID = 0;
            try
            {
                var admissions = new admissions()
                {
                    created = DateTime.Now,
                    creator_id = model.creator_id,
                    modified = DateTime.Now,
                    modifier_id = model.modifier_id,
                    record_date = Helpers.ToDateFormat(model.record_date),
                    patient_id = model.patient_id,
                    complaint = model.complaint,
                    lung_problems = model.lung_problems,
                    lung_problems_description = model.lung_problems_description,
                    stomach_problems = model.stomach_problems,
                    stomach_problems_description = model.stomach_problems_description,
                    thyroid_problems = model.thyroid_problems,
                    thyroid_problems_description = model.thyroid_problems_description,
                    neurological_problems = model.neurological_problems,
                    neurological_problems_description = model.neurological_problems_description,
                    heart_problems = model.heart_problems,
                    heart_problems_description = model.heart_problems_description,
                    liver_problems = model.liver_problems,
                    liver_problems_description = model.liver_problems_description,
                    vision_problems = model.vision_problems,
                    vision_problems_description = model.vision_problems_description,
                    kidney_problems = model.kidney_problems,
                    kidney_problems_description = model.kidney_problems_description,
                    arthritis_problems = model.arthritis_problems,
                    arthrities_problems_description = model.arthrities_problems_description,
                    diabetes_problems = model.diabetes_problems,
                    diabetes_problems_description = (model.diabetes_problems_description),
                    chronic_infection = model.chronic_infection,
                    chronic_infection_description = model.chronic_infection_description,
                    cancer = model.cancer,
                    cancer_description = model.cancer_description,
                    family_nsf = model.family_nsf,
                    family_heart_disease = model.family_heart_disease,
                    family_hypertension = model.family_hypertension,
                    family_diabetes = model.family_diabetes,
                    family_stroke = model.family_stroke,
                    family_seizures = model.family_seizures,
                    family_kidney_disease = model.family_kidney_disease,
                    family_liver_disease = model.family_liver_disease,
                    family_desc = model.family_desc,
                    other_history = model.other_history,
                    informant = model.informant,
                    interpretive = model.interpretive,
                    admitted_via = model.admitted_via,
                    admitted_from = model.admitted_from,
                    contact = model.contact,

                    valuables_disposition = model.valuables_disposition,
                    valuables_comments = model.valuables_comments,
                    living_will = model.living_will,
                    power_of_attorney = model.power_of_attorney,
                    organ_donor = model.organ_donor,
                    directives_comments = model.directives_comments,
                    no_past_med = model.no_past_med,
                    cardiac_past_med = model.cardiac_past_med,
                    respiratory_past_med = model.respiratory_past_med,
                    others_past_med = model.others_past_med,
                    past_comments = model.past_comments,
                    past_surg_history = model.past_surg_history,
                    tetanus = model.tetanus,
                    influenza = model.influenza,
                    pneumonia = model.pneumonia,
                    hepatitisb = model.hepatitisb,
                    dpt = model.dpt,
                    polio = model.polio,
                    chickenpox = model.chickenpox,
                    immu_comments = model.immu_comments,
                    patient_lives = model.patient_lives,
                    social_abuse = model.social_abuse,
                    social_observations = model.social_observations,
                    social_support_other_services = model.social_support_other_services,
                    social_support_services = model.social_support_services,

                    social_comments = model.social_comments,
                    drug_use_freq = model.drug_use_freq,
                    last_drug_use = model.last_drug_use,
                    alcohol_use_freq = (model.alcohol_use_freq),
                    last_alcohol_use = model.last_alcohol_use,
                    tabocco_use_freq = model.tabocco_use_freq,
                    last_tabocco_use = model.last_tabocco_use,
                    substance_use = model.substance_use,
                    impaired_hearing = model.impaired_hearing,
                    impaired_vision = model.impaired_vision,
                    can_perform_adl = model.can_perform_adl,
                    can_read = model.can_read,
                    can_write = model.can_write,
                    hearing_aid_left = model.hearing_aid_left,
                    hearing_aid_right = model.hearing_aid_right,
                    use_glasses = model.use_glasses,
                    use_contacts = model.use_contacts,
                    use_dentures_lower = model.use_dentures_lower,
                    use_dentures_upper = model.use_dentures_upper,
                    use_walker = model.use_walker,
                    use_crutches = model.use_crutches,
                    use_wheelchair = model.use_wheelchair,
                    use_cane = model.use_cane,
                    use_prosthesis = model.use_prosthesis,
                    comments = model.comments,
                    active = true,
                    gardasil = model.gardasil,
                };
                if (model.orientation != null)
                {
                    admissions.orientation = String.Join(",", model.orientation);
                }
                if (model.belongings != null)
                {
                    admissions.belongings = String.Join(",", model.belongings);
                }
                _dbContext.Add(admissions);
                _dbContext.SaveChanges();
                returnID = admissions.id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }
        /// <summary>
        /// update the Admission info 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateAdmissionInfo(AdmissionsModel model)
        {
            int returnID = 0;
            try
            {
                var admissions = new admissions();
                admissions = _dbContext.admissions.Where(id => id.id == model.id).FirstOrDefault();
                if (admissions != null)
                {
                    admissions.modified = DateTime.Now;
                    admissions.modifier_id = model.modifier_id;
                    admissions.record_date = Helpers.ToDateFormat(model.record_date);
                    admissions.complaint = model.complaint;
                    admissions.lung_problems = model.lung_problems;
                    admissions.lung_problems_description = model.lung_problems_description;
                    admissions.stomach_problems = model.stomach_problems;
                    admissions.stomach_problems_description = model.stomach_problems_description;
                    admissions.thyroid_problems = model.thyroid_problems;
                    admissions.thyroid_problems_description = model.thyroid_problems_description;
                    admissions.neurological_problems = model.neurological_problems;
                    admissions.neurological_problems_description = model.neurological_problems_description;
                    admissions.heart_problems = model.heart_problems;
                    admissions.heart_problems_description = model.heart_problems_description;
                    admissions.liver_problems = model.liver_problems;
                    admissions.liver_problems_description = model.liver_problems_description;
                    admissions.vision_problems = model.vision_problems;
                    admissions.vision_problems_description = model.vision_problems_description;
                    admissions.kidney_problems = model.kidney_problems;
                    admissions.kidney_problems_description = model.kidney_problems_description;
                    admissions.arthritis_problems = model.arthritis_problems;
                    admissions.arthrities_problems_description = model.arthrities_problems_description;
                    admissions.diabetes_problems = model.diabetes_problems;
                    admissions.diabetes_problems_description = (model.diabetes_problems_description);
                    admissions.chronic_infection = model.chronic_infection;
                    admissions.chronic_infection_description = model.chronic_infection_description;
                    admissions.cancer = model.cancer;
                    admissions.cancer_description = model.cancer_description;
                    admissions.family_nsf = model.family_nsf;
                    admissions.family_heart_disease = model.family_heart_disease;
                    admissions.family_hypertension = model.family_hypertension;
                    admissions.family_diabetes = model.family_diabetes;
                    admissions.family_stroke = model.family_stroke;
                    admissions.family_seizures = model.family_seizures;
                    admissions.family_kidney_disease = model.family_kidney_disease;
                    admissions.family_liver_disease = model.family_liver_disease;
                    admissions.family_desc = model.family_desc;
                    admissions.other_history = model.other_history;
                    admissions.informant = model.informant;
                    admissions.interpretive = model.interpretive;
                    admissions.admitted_via = model.admitted_via;
                    admissions.admitted_from = model.admitted_from;
                    admissions.contact = model.contact;

                    admissions.valuables_disposition = model.valuables_disposition;
                    admissions.valuables_comments = model.valuables_comments;
                    admissions.living_will = model.living_will;
                    admissions.power_of_attorney = model.power_of_attorney;
                    admissions.organ_donor = model.organ_donor;
                    admissions.directives_comments = model.directives_comments;
                    admissions.no_past_med = model.no_past_med;
                    admissions.cardiac_past_med = model.cardiac_past_med;
                    admissions.respiratory_past_med = model.respiratory_past_med;
                    admissions.others_past_med = model.others_past_med;
                    admissions.past_comments = model.past_comments;
                    admissions.past_surg_history = model.past_surg_history;
                    admissions.tetanus = model.tetanus;
                    admissions.influenza = model.influenza;
                    admissions.pneumonia = model.pneumonia;
                    admissions.hepatitisb = model.hepatitisb;
                    admissions.dpt = model.dpt;
                    admissions.polio = model.polio;
                    admissions.chickenpox = model.chickenpox;
                    admissions.immu_comments = model.immu_comments;
                    admissions.patient_lives = model.patient_lives;
                    admissions.social_abuse = model.social_abuse;
                    admissions.social_observations = model.social_observations;
                    admissions.social_support_other_services = model.social_support_other_services;
                    admissions.social_support_services = model.social_support_services;

                    admissions.social_comments = model.social_comments;
                    admissions.drug_use_freq = model.drug_use_freq;
                    admissions.last_drug_use = model.last_drug_use;
                    admissions.alcohol_use_freq = (model.alcohol_use_freq);
                    admissions.last_alcohol_use = model.last_alcohol_use;
                    admissions.tabocco_use_freq = model.tabocco_use_freq;
                    admissions.last_tabocco_use = model.last_tabocco_use;
                    admissions.substance_use = model.substance_use;
                    admissions.impaired_hearing = model.impaired_hearing;
                    admissions.impaired_vision = model.impaired_vision;
                    admissions.can_perform_adl = model.can_perform_adl;
                    admissions.can_read = model.can_read;
                    admissions.can_write = model.can_write;
                    admissions.hearing_aid_left = model.hearing_aid_left;
                    admissions.hearing_aid_right = model.hearing_aid_right;
                    admissions.use_glasses = model.use_glasses;
                    admissions.use_contacts = model.use_contacts;
                    admissions.use_dentures_lower = model.use_dentures_lower;
                    admissions.use_dentures_upper = model.use_dentures_upper;
                    admissions.use_walker = model.use_walker;
                    admissions.use_crutches = model.use_crutches;
                    admissions.use_wheelchair = model.use_wheelchair;
                    admissions.use_cane = model.use_cane;
                    admissions.use_prosthesis = model.use_prosthesis;
                    admissions.comments = model.comments;
                    admissions.active = true;
                    admissions.gardasil = model.gardasil;
                };
                if (model.orientation != null)
                {
                    admissions.orientation = String.Join(",", model.orientation);
                }
                if (model.belongings != null)
                {
                    admissions.belongings = String.Join(",", model.belongings);
                }
                _dbContext.Update(admissions);
                _dbContext.SaveChanges();
                returnID = admissions.id;

            }

            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }
        /// <summary>
        /// get Admission info 
        /// </summary>
        /// <returns></returns>
        public List<AdmissionsModel> GetAdmissionAll(int pId)
        {
            var result = new List<AdmissionsModel>();
            try
            {
                result = (from a in _dbContext.admissions
                          where a.patient_id == pId && a.active == true
                          orderby a.id
                          select new AdmissionsModel
                          {
                              id = a.id,
                              record_date = Helpers.ToDateString(a.record_date),
                              creator = (from p in _dbContext.people where p.id == a.creator_id select p).FirstOrDefault().username + ' ' + '@' + ' ' + Helpers.ToDateTimeFormat(a.created),
                              modified = (from p in _dbContext.people where p.id == a.modifier_id select p).FirstOrDefault().username + ' ' + '@' + ' ' + Helpers.ToDateTimeFormat(a.modified)

                          }).ToList();
            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return result;
        }
        /// <summary>
        /// get Admission info 
        /// </summary>
        /// <returns></returns>
        public AdmissionsModel GetAdmissionById(int id)
        {
            var result = new AdmissionsModel();
            try
            {

                result = (from model in _dbContext.admissions
                          where model.id == id
                          select new AdmissionsModel
                          {
                              id = model.id,
                              created = Helpers.ToDateTimeFormat(model.created),
                              creator_id = model.creator_id,
                              modified = Helpers.ToDateTimeFormat(model.modified),
                              modifier_id = model.modifier_id,
                              record_date = Helpers.ToDateString(model.record_date),
                              patient_id = model.patient_id,
                              complaint = model.complaint,
                              lung_problems = model.lung_problems,
                              lung_problems_description = model.lung_problems_description,
                              stomach_problems = model.stomach_problems,
                              stomach_problems_description = model.stomach_problems_description,
                              thyroid_problems = model.thyroid_problems,
                              thyroid_problems_description = model.thyroid_problems_description,
                              neurological_problems = model.neurological_problems,
                              neurological_problems_description = model.neurological_problems_description,
                              heart_problems = model.heart_problems,
                              heart_problems_description = model.heart_problems_description,
                              liver_problems = model.liver_problems,
                              liver_problems_description = model.liver_problems_description,
                              vision_problems = model.vision_problems,
                              vision_problems_description = model.vision_problems_description,
                              kidney_problems = model.kidney_problems,
                              kidney_problems_description = model.kidney_problems_description,
                              arthritis_problems = model.arthritis_problems,
                              arthrities_problems_description = model.arthrities_problems_description,
                              diabetes_problems = model.diabetes_problems,
                              diabetes_problems_description = (model.diabetes_problems_description),
                              chronic_infection = model.chronic_infection,
                              chronic_infection_description = model.chronic_infection_description,
                              cancer = model.cancer,
                              cancer_description = model.cancer_description,
                              family_nsf = model.family_nsf,
                              family_heart_disease = model.family_heart_disease,
                              family_hypertension = model.family_hypertension,
                              family_diabetes = model.family_diabetes,
                              family_stroke = model.family_stroke,
                              family_seizures = model.family_seizures,
                              family_kidney_disease = model.family_kidney_disease,
                              family_liver_disease = model.family_liver_disease,
                              family_desc = model.family_desc,
                              other_history = model.other_history,
                              informant = model.informant,
                              interpretive = model.interpretive,
                              admitted_via = model.admitted_via,
                              admitted_from = model.admitted_from,
                              contact = model.contact,
                              orient = model.orientation,
                              belong = model.belongings,
                              valuables_disposition = model.valuables_disposition,
                              valuables_comments = model.valuables_comments,
                              living_will = model.living_will,
                              power_of_attorney = model.power_of_attorney,
                              organ_donor = model.organ_donor,
                              directives_comments = model.directives_comments,
                              no_past_med = model.no_past_med,
                              cardiac_past_med = model.cardiac_past_med,
                              respiratory_past_med = model.respiratory_past_med,
                              others_past_med = model.others_past_med,
                              past_comments = model.past_comments,
                              past_surg_history = model.past_surg_history,
                              tetanus = model.tetanus,
                              influenza = model.influenza,
                              pneumonia = model.pneumonia,
                              hepatitisb = model.hepatitisb,
                              dpt = model.dpt,
                              polio = model.polio,
                              chickenpox = model.chickenpox,
                              immu_comments = model.immu_comments,
                              patient_lives = model.patient_lives,
                              social_abuse = model.social_abuse,
                              social_observations = model.social_observations,
                              social_support_other_services = model.social_support_other_services,
                              social_support_services = model.social_support_services,

                              social_comments = model.social_comments,
                              drug_use_freq = model.drug_use_freq,
                              last_drug_use = model.last_drug_use,
                              alcohol_use_freq = (model.alcohol_use_freq),
                              last_alcohol_use = model.last_alcohol_use,
                              tabocco_use_freq = model.tabocco_use_freq,
                              last_tabocco_use = model.last_tabocco_use,
                              substance_use = model.substance_use,
                              impaired_hearing = model.impaired_hearing,
                              impaired_vision = model.impaired_vision,
                              can_perform_adl = model.can_perform_adl,
                              can_read = model.can_read,
                              can_write = model.can_write,
                              hearing_aid_left = model.hearing_aid_left,
                              hearing_aid_right = model.hearing_aid_right,
                              use_glasses = model.use_glasses,
                              use_contacts = model.use_contacts,
                              use_dentures_lower = model.use_dentures_lower,
                              use_dentures_upper = model.use_dentures_upper,
                              use_walker = model.use_walker,
                              use_crutches = model.use_crutches,
                              use_wheelchair = model.use_wheelchair,
                              use_cane = model.use_cane,
                              use_prosthesis = model.use_prosthesis,
                              comments = model.comments,
                              gardasil = model.gardasil,

                          }).FirstOrDefault();
                if (result.orient != null)
                {
                    result.orientation = result.orient.Split(',').ToList();
                }
                if (result.belong != null)
                {
                    result.belongings = result.belong.Split(',').ToList();
                }
                var peopleCreated = (from p in _dbContext.people where p.id == result.creator_id select p).FirstOrDefault();
                var peopleModified = (from p in _dbContext.people where p.id == result.modifier_id select p).FirstOrDefault();
                if (peopleCreated != null)
                {
                    result.creator = peopleCreated.username;
                }
                if (peopleModified != null)
                {
                    result.modifier = peopleModified.username;

                }

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return result;
        }

        /// <summary>
        /// view Admission info 
        /// </summary>
        /// <returns></returns>
        public AdmissionsModel ViewAdmissionById(int id)
        {
            var result = new AdmissionsModel();
            try
            {

                result = (from model in _dbContext.admissions
                          where model.id == id
                          select new AdmissionsModel
                          {
                              id = model.id,
                              created = Helpers.ToDateTimeFormat(model.created),
                              creator_id = model.creator_id,
                              modified = Helpers.ToDateTimeFormat(model.modified),
                              modifier_id = model.modifier_id,
                              record_date = Helpers.ToDateString(model.record_date),
                              patient_id = model.patient_id,
                              complaint = model.complaint,
                              lung_problems_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.lung_problems)).ToString(),
                              lung_problems_description = model.lung_problems_description,
                              stomach_problems_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.stomach_problems)).ToString(),
                              stomach_problems_description = model.stomach_problems_description,
                              thyroid_problems_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.thyroid_problems)).ToString(),
                              thyroid_problems_description = model.thyroid_problems_description,
                              neurological_problems_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.neurological_problems)).ToString(),
                              neurological_problems_description = model.neurological_problems_description,
                              heart_problems_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.heart_problems)).ToString(),
                              heart_problems_description = model.heart_problems_description,
                              liver_problems_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.liver_problems)).ToString(),
                              liver_problems_description = model.liver_problems_description,
                              vision_problems_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.vision_problems)).ToString(),
                              vision_problems_description = model.vision_problems_description,
                              kidney_problems_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.kidney_problems)).ToString(),
                              kidney_problems_description = model.kidney_problems_description,
                              arthritis_problems_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.arthritis_problems)).ToString(),
                              arthrities_problems_description = model.arthrities_problems_description,
                              diabetes_problems_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.diabetes_problems)).ToString(),
                              diabetes_problems_description = (model.diabetes_problems_description),
                              chronic_infection_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.chronic_infection)).ToString(),
                              chronic_infection_description = model.chronic_infection_description,
                              cancer_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.cancer)).ToString(),
                              cancer_description = model.cancer_description,
                              family_nsf_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.family_nsf)).ToString(),
                              family_heart_disease_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.family_heart_disease)).ToString(),
                              family_hypertension_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.family_hypertension)).ToString(),
                              family_diabetes_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.family_diabetes)).ToString(),
                              family_stroke_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.family_stroke)).ToString(),
                              family_seizures_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.family_seizures)).ToString(),
                              family_kidney_disease_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.family_kidney_disease)).ToString(),
                              family_liver_disease_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.family_liver_disease)).ToString(),
                              family_desc = model.family_desc,
                              other_history = model.other_history,
                              informant = model.informant,
                              interpretive = model.interpretive,
                              admitted_via = model.admitted_via,
                              admitted_from = model.admitted_from,
                              contact = model.contact,
                              orient = model.orientation,
                              belong = model.belongings,
                              valuables_disposition = model.valuables_disposition,
                              valuables_comments = model.valuables_comments,
                              living_will_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.liver_problems)).ToString(),
                              power_of_attorney_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.power_of_attorney)).ToString(),
                              organ_donor_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.organ_donor)).ToString(),
                              directives_comments = model.directives_comments,
                              no_past_med_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.no_past_med)).ToString(),
                              cardiac_past_med_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.cardiac_past_med)).ToString(),
                              respiratory_past_med_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.respiratory_past_med)).ToString(),
                              others_past_med_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.others_past_med)).ToString(),
                              past_comments = model.past_comments,
                              past_surg_history = model.past_surg_history,
                              tetanus = model.tetanus,
                              influenza = model.influenza,
                              pneumonia = model.pneumonia,
                              hepatitisb = model.hepatitisb,
                              dpt = model.dpt,
                              polio = model.polio,
                              chickenpox = model.chickenpox,
                              immu_comments = model.immu_comments,
                              patient_lives = model.patient_lives,
                              social_abuse = model.social_abuse,
                              social_observations = model.social_observations,
                              social_support_other_services = model.social_support_other_services,
                              social_support_services = model.social_support_services,

                              social_comments = model.social_comments,
                              drug_use_freq = model.drug_use_freq,
                              last_drug_use = model.last_drug_use,
                              alcohol_use_freq = (model.alcohol_use_freq),
                              last_alcohol_use = model.last_alcohol_use,
                              tabocco_use_freq = model.tabocco_use_freq,
                              last_tabocco_use = model.last_tabocco_use,
                              substance_use = model.substance_use,
                              impaired_hearing_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.impaired_hearing)).ToString(),
                              impaired_vision_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.impaired_vision)).ToString(),
                              can_perform_adl_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.can_perform_adl)).ToString(),
                              can_read_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.can_read)).ToString(),
                              can_write_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.can_write)).ToString(),
                              hearing_aid_left_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.hearing_aid_left)).ToString(),
                              hearing_aid_right_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.hearing_aid_right)).ToString(),
                              use_glasses_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.use_glasses)).ToString(),
                              use_contacts_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.use_contacts)).ToString(),
                              use_dentures_lower_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.use_dentures_lower)).ToString(),
                              use_dentures_upper_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.use_dentures_upper)).ToString(),
                              use_walker_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.use_walker)).ToString(),
                              use_crutches_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.use_crutches)).ToString(),
                              use_wheelchair_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.use_wheelchair)).ToString(),
                              use_cane_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.use_cane)).ToString(),
                              use_prosthesis_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(model.use_prosthesis)).ToString(),
                              comments = model.comments,
                              gardasil = model.gardasil,
                          }).FirstOrDefault();
                if (result.orient != null)
                {
                    result.orientation = result.orient.Split(',').ToList();
                }
                if (result.belong != null)
                {
                    result.belongings = result.belong.Split(',').ToList();
                }
                var peopleCreated = (from p in _dbContext.people where p.id == result.creator_id select p).FirstOrDefault();
                var peopleModified = (from p in _dbContext.people where p.id == result.modifier_id select p).FirstOrDefault();
                if (peopleCreated != null)
                {
                    result.creator = peopleCreated.username;
                }
                if (peopleModified != null)
                {
                    result.modifier = peopleModified.username;

                }

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return result;
        }

        /// <summary>
        /// delete a provider info 
        /// </summary>
        /// <returns></returns>
        public AdmissionsModel DeleteAdmissionById(int id, int userId)
        {
            var status = new AdmissionsModel();
            try
            {
                var model = (from m in _dbContext.admissions
                             where m.id == id
                             select m).FirstOrDefault();
                model.active = false;
                model.modifier_id = userId;
                model.modified = DateTime.Now;
                _dbContext.Update(model);
                _dbContext.SaveChanges();
                status.patient_id = model.patient_id;
            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return status;
        }
    }
}