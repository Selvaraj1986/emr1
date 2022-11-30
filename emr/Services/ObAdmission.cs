using emr.Models;
using emr.Models.DBContext;
using emr.Models.Model;
using emr.Support;
using System.Security.Cryptography;

namespace emr.Services
{
    public class ObAdmission : IObAdmission
    {
        private readonly EmrDbContext _dbContext;
        private IConfiguration _iconfiguration;
        public ObAdmission(EmrDbContext dbContext, IConfiguration iconfiguration)
        {
            _dbContext = dbContext;
            _iconfiguration = iconfiguration;
        }
        public List<ObAdmissionsModel> GetobAdmissionsAll(int id)
        {
            var result = new List<ObAdmissionsModel>();
            try
            {
                result = (from a in _dbContext.ob_admissions
                          where a.patient_id == id && a.active == true
                          orderby a.id
                          select new ObAdmissionsModel
                          {
                              id = a.id,
                              record_date = Helpers.ToDateString(a.record_date),
                              creator = (from p in _dbContext.people where p.id == a.creator_id select p).FirstOrDefault().username + ' ' + '@' + ' ' + Helpers.ToDateTimeFormat(a.created),
                              modifier = (from p in _dbContext.people where p.id == a.modifier_id select p).FirstOrDefault().username + ' ' + '@' + ' ' + Helpers.ToDateTimeFormat(a.modified)

                          }
                ).ToList();
            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());
            }
            return result;
        }
        public ObAdmissionsModel GetobAdmissionsById(int id)
        {
            var result = new ObAdmissionsModel();
            try
            {
                result = (from m in _dbContext.ob_admissions
                          where m.id == id
                          select new ObAdmissionsModel
                          {
                              id = m.id,
                              created = Helpers.ToDateTimeFormat(m.created),
                              creator_id = m.creator_id,
                              modified = Helpers.ToDateTimeFormat(m.modified),
                              modifier_id = m.modifier_id,
                              record_date = Helpers.ToDateString(m.record_date),
                              alcohol_use = m.alcohol_use,
                              alcohol_use_amount = m.alcohol_use_amount,
                              amniocentesis = m.amniocentesis,
                              amniocentesis_notes = m.amniocentesis_notes,
                              antibody_screen = m.antibody_screen,
                              blood_type = m.blood_type,
                              cerclage = m.cerclage,
                              chickpox = m.chickpox,
                              chlamydia = m.chlamydia,
                              chrhtn = m.chrhtn,
                              current_meds = m.current_meds == null ? String.Empty : m.current_meds,
                              current_pain = m.current_pain,
                              decelerations_notes = m.decelerations_notes,
                              decreased_fetal_movement = m.decreased_fetal_movement,
                              diabetes = m.diabetes,
                              diabetes_notes = m.diabetes_notes,
                              diet = m.diet,
                              drug_use = m.drug_use,
                              drug_use_amount = m.drug_use_amount,
                              fhr = m.fhr,
                              gc = m.gc,
                              group_b_strep = m.group_b_strep,
                              hepatitis_b = m.hepatitis_b,
                              hiv = m.hiv,
                              induction = m.induction,
                              induction_notes = m.induction_notes,
                              insulin = m.insulin,
                              insulin_notes = m.insulin_notes,
                              iugr = m.iugr,
                              labor = m.labor,
                              ltv = m.ltv,
                              multi_gestation = m.multi_gestation,
                              npc = m.npc,
                              nrfs = m.nrfs,
                              observation = m.observation,
                              observation_notes = m.observation_notes,
                              ob_complication = m.ob_complication,
                              ongoing_pain = m.ongoing_pain,
                              other = m.other,
                              other_language = m.other_language,
                              other_notes = m.other_notes,
                              pih = m.pih,
                              pol = m.pol,
                              patient_id = m.patient_id,
                              pprom = m.pprom,
                              previous_anomalies = m.previous_anomalies,
                              previous_diabetes = m.previous_diabetes,
                              previous_diabetes_notes = m.previous_diabetes_notes,
                              previous_htn = m.previous_htn,
                              previous_hx = m.previous_hx,
                              previous_macrosomia = m.previous_macrosomia,
                              previous_neodeath = m.previous_neodeath,
                              previous_other = m.previous_other,
                              previous_pih = m.previous_pih,
                              previous_pol = m.previous_pol,
                              previous_pp_hemorrhage = m.previous_pp_hemorrhage,
                              previous_precipitous = m.previous_precipitous,
                              previous_stillbirth = m.previous_stillbirth,
                              prev_cs_reason = m.prev_cs_reason,
                              prev_cs_type = m.prev_cs_type,
                              primarycs = m.primarycs,
                              primarycs_notes = m.primarycs_notes,
                              primary_language = m.primary_language,
                              repeatcs = m.repeatcs,
                              rhogam = m.rhogam,
                              rpr = m.rpr,
                              rubella = m.rubella,
                              stv = m.stv,
                              tobacco_use = m.tobacco_use,
                              tobacco_use_amount = m.tobacco_use_amount,
                              wnl = m.wnl,
                          }).FirstOrDefault();

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

        public ObAdmissionsModel ViewobAdmissionsById(int id)
        {
            var result = new ObAdmissionsModel();
            try
            {
                result = (from m in _dbContext.ob_admissions
                          where m.id == id
                          select new ObAdmissionsModel
                          {
                              id = m.id,
                              created = Helpers.ToDateTimeFormat(m.created),
                              creator_id = m.creator_id,
                              modified = Helpers.ToDateTimeFormat(m.modified),
                              modifier_id = m.modifier_id,
                              record_date = Helpers.ToDateString(m.record_date),
                              alcohol_use = m.alcohol_use,
                              alcohol_use_amount = m.alcohol_use_amount,
                              amniocentesis_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.amniocentesis)).ToString(),
                              amniocentesis_notes = m.amniocentesis_notes,
                              antibody_screen = m.antibody_screen,
                              blood_type = m.blood_type,
                              cerclage_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.cerclage)).ToString(),
                              chickpox_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.chickpox)).ToString(),
                              chlamydia = m.chlamydia,
                              chrhtn_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.chrhtn)).ToString(),
                              current_meds = m.current_meds == null ? String.Empty : m.current_meds,
                              current_pain_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.current_pain)).ToString(),
                              decelerations_notes = m.decelerations_notes,
                              decreased_fetal_movement_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.decreased_fetal_movement)).ToString(),
                              diabetes_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.diabetes)).ToString(),
                              diabetes_notes = m.diabetes_notes,
                              diet = m.diet,
                              drug_use = m.drug_use,
                              drug_use_amount = m.drug_use_amount,
                              fhr = m.fhr,
                              gc = m.gc,
                              group_b_strep = m.group_b_strep,
                              hepatitis_b = m.hepatitis_b,
                              hiv = m.hiv,
                              induction_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.induction)).ToString(),
                              induction_notes = m.induction_notes,
                              insulin_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.insulin)).ToString(),
                              insulin_notes = m.insulin_notes,
                              iugr_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.iugr)).ToString(),
                              labor_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.labor)).ToString(),
                              ltv = m.ltv,
                              multi_gestation_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.multi_gestation)).ToString(),
                              npc_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.npc)).ToString(),
                              nrfs_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.nrfs)).ToString(),
                              observation_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.observation)).ToString(),
                              observation_notes = m.observation_notes,
                              ob_complication = m.ob_complication,
                              ongoing_pain_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.ongoing_pain)).ToString(),
                              other_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.other)).ToString(),
                              other_language = m.other_language,
                              other_notes = m.other_notes,
                              pih_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.pih)).ToString(),
                              pol_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.pol)).ToString(),
                              patient_id = m.patient_id,
                              pprom_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.pprom)).ToString(),
                              previous_anomalies_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.previous_anomalies)).ToString(),
                              previous_diabetes_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.previous_diabetes)).ToString(),
                              previous_diabetes_notes = m.previous_diabetes_notes,
                              previous_htn_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.previous_htn)).ToString(),
                              previous_hx_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.previous_hx)).ToString(),
                              previous_macrosomia_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.previous_macrosomia)).ToString(),
                              previous_neodeath_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.previous_neodeath)).ToString(),
                              previous_other = m.previous_other,
                              previous_pih_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.previous_pih)).ToString(),
                              previous_pol_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.previous_pol)).ToString(),
                              previous_pp_hemorrhage_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.previous_pp_hemorrhage)).ToString(),
                              previous_precipitous_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.previous_precipitous)).ToString(),
                              previous_stillbirth_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.previous_stillbirth)).ToString(),
                              prev_cs_reason = m.prev_cs_reason,
                              prev_cs_type = m.prev_cs_type,
                              primarycs_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.primarycs)).ToString(),

                              primarycs_notes = m.primarycs_notes,
                              primary_language = m.primary_language,
                              repeatcs_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.repeatcs)).ToString(),
                              rhogam = m.rhogam,
                              rpr = m.rpr,
                              rubella = m.rubella,
                              stv = m.stv,
                              tobacco_use = m.tobacco_use,
                              tobacco_use_amount = m.tobacco_use_amount,
                              wnl_status = Helpers.GetEnumValue<Helpers.Status>(Convert.ToInt16(m.wnl)).ToString(),
                          }).FirstOrDefault();

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

        public long SaveobAdmissionInfo(ObAdmissionsModel model)
        {
            long returnID = 0;
            try
            {
                ob_admissions ob_admission = new ob_admissions()
                {
                    created = DateTime.Now,
                    creator_id = model.creator_id,
                    modified = DateTime.Now,
                    modifier_id = model.modifier_id,
                    record_date = Helpers.ToDateFormat(model.record_date),
                    alcohol_use = model.alcohol_use,
                    alcohol_use_amount = model.alcohol_use_amount,
                    amniocentesis = model.amniocentesis,
                    amniocentesis_notes = model.amniocentesis_notes,
                    antibody_screen = model.antibody_screen,
                    blood_type = model.blood_type,
                    cerclage = model.cerclage,
                    chickpox = model.chickpox,
                    chlamydia = model.chlamydia,
                    chrhtn = model.chrhtn,
                    current_meds = model.current_meds,
                    current_pain = model.current_pain,
                    decelerations_notes = model.decelerations_notes,
                    decreased_fetal_movement = model.decreased_fetal_movement,
                    diabetes = model.diabetes,
                    diabetes_notes = model.diabetes_notes,
                    diet = model.diet,
                    drug_use = model.drug_use,
                    drug_use_amount = model.drug_use_amount,
                    fhr = model.fhr,
                    gc = model.gc,
                    group_b_strep = model.group_b_strep,
                    hepatitis_b = model.hepatitis_b,
                    hiv = model.hiv,
                    induction = model.induction,
                    induction_notes = model.induction_notes,
                    insulin = model.insulin,
                    insulin_notes = model.insulin_notes,
                    iugr = model.iugr,
                    labor = model.labor,
                    ltv = model.ltv,
                    multi_gestation = model.multi_gestation,
                    npc = model.npc,
                    nrfs = model.nrfs,
                    observation = model.observation,
                    observation_notes = model.observation_notes,
                    ob_complication = model.ob_complication,
                    ongoing_pain = model.ongoing_pain,
                    other = model.other,
                    other_language = model.other_language,
                    other_notes = model.other_notes,
                    pih = model.pih,
                    pol = model.pol,
                    patient_id = model.patient_id,
                    pprom = model.pprom,
                    previous_anomalies = model.previous_anomalies,
                    previous_diabetes = model.previous_diabetes,
                    previous_diabetes_notes = model.previous_diabetes_notes,
                    previous_htn = model.previous_htn,
                    previous_hx = model.previous_hx,
                    previous_macrosomia = model.previous_macrosomia,
                    previous_neodeath = model.previous_neodeath,
                    previous_other = model.previous_other,
                    previous_pih = model.previous_pih,
                    previous_pol = model.previous_pol,
                    previous_pp_hemorrhage = model.previous_pp_hemorrhage,
                    previous_precipitous = model.previous_precipitous,
                    previous_stillbirth = model.previous_stillbirth,
                    prev_cs_reason = model.prev_cs_reason,
                    prev_cs_type = model.prev_cs_type,
                    primarycs = model.primarycs,
                    primarycs_notes = model.primarycs_notes,
                    primary_language = model.primary_language,
                    repeatcs = model.repeatcs,
                    rhogam = model.rhogam,
                    rpr = model.rpr,
                    rubella = model.rubella,
                    stv = model.stv,
                    tobacco_use = model.tobacco_use,
                    tobacco_use_amount = model.tobacco_use_amount,
                    wnl = model.wnl,
                    active = true

                };

                _dbContext.Add(ob_admission);
                _dbContext.SaveChanges();
                returnID = model.id;

            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
            return returnID;
        }

        public long UpdateobAdmissionInfo(ObAdmissionsModel model)
        {
            long returnID = 0;
            try
            {
                var admissions = new ob_admissions();
                admissions = _dbContext.ob_admissions.Where(id => id.id == model.id).FirstOrDefault();
                if (admissions != null)
                {

                    admissions.modified = DateTime.Now;
                    admissions.modifier_id = model.modifier_id;
                    admissions.record_date = Helpers.ToDateFormat(model.record_date);
                    admissions.patient_id = admissions.patient_id;
                    admissions.alcohol_use = model.alcohol_use;
                    admissions.alcohol_use_amount = model.alcohol_use_amount;
                    admissions.amniocentesis = model.amniocentesis;
                    admissions.amniocentesis_notes = model.amniocentesis_notes;
                    admissions.antibody_screen = model.antibody_screen;
                    admissions.blood_type = model.blood_type;
                    admissions.cerclage = model.cerclage;
                    admissions.chickpox = model.chickpox;
                    admissions.chlamydia = model.chlamydia;
                    admissions.chrhtn = model.chrhtn;
                    admissions.current_meds = model.current_meds;
                    admissions.current_pain = model.current_pain;
                    admissions.decelerations_notes = model.decelerations_notes;
                    admissions.decreased_fetal_movement = model.decreased_fetal_movement;
                    admissions.diabetes = model.diabetes;
                    admissions.diabetes_notes = model.diabetes_notes;
                    admissions.diet = model.diet;
                    admissions.drug_use = model.drug_use;
                    admissions.drug_use_amount = model.drug_use_amount;
                    admissions.fhr = model.fhr;
                    admissions.gc = model.gc;
                    admissions.group_b_strep = model.group_b_strep;
                    admissions.hepatitis_b = model.hepatitis_b;
                    admissions.hiv = model.hiv;
                    admissions.induction = model.induction;
                    admissions.induction_notes = model.induction_notes;
                    admissions.insulin = model.insulin;
                    admissions.insulin_notes = model.insulin_notes;
                    admissions.iugr = model.iugr;
                    admissions.labor = model.labor;
                    admissions.ltv = model.ltv;
                    admissions.multi_gestation = model.multi_gestation;
                    admissions.npc = model.npc;
                    admissions.nrfs = model.nrfs;
                    admissions.observation = model.observation;
                    admissions.observation_notes = model.observation_notes;
                    admissions.ob_complication = model.ob_complication;
                    admissions.ongoing_pain = model.ongoing_pain;
                    admissions.other = model.other;
                    admissions.other_language = model.other_language;
                    admissions.other_notes = model.other_notes;
                    admissions.pih = model.pih;
                    admissions.pol = model.pol;
                    admissions.pprom = model.pprom;
                    admissions.previous_anomalies = model.previous_anomalies;
                    admissions.previous_diabetes = model.previous_diabetes;
                    admissions.previous_diabetes_notes = model.previous_diabetes_notes;
                    admissions.previous_htn = model.previous_htn;
                    admissions.previous_hx = model.previous_hx;
                    admissions.previous_macrosomia = model.previous_macrosomia;
                    admissions.previous_neodeath = model.previous_neodeath;
                    admissions.previous_other = model.previous_other;
                    admissions.previous_pih = model.previous_pih;
                    admissions.previous_pol = model.previous_pol;
                    admissions.previous_pp_hemorrhage = model.previous_pp_hemorrhage;
                    admissions.previous_precipitous = model.previous_precipitous;
                    admissions.previous_stillbirth = model.previous_stillbirth;
                    admissions.prev_cs_reason = model.prev_cs_reason;
                    admissions.prev_cs_type = model.prev_cs_type;
                    admissions.primarycs = model.primarycs;
                    admissions.primarycs_notes = model.primarycs_notes;
                    admissions.primary_language = model.primary_language;
                    admissions.repeatcs = model.repeatcs;
                    admissions.rhogam = model.rhogam;
                    admissions.rpr = model.rpr;
                    admissions.rubella = model.rubella;
                    admissions.stv = model.stv;
                    admissions.tobacco_use = model.tobacco_use;
                    admissions.tobacco_use_amount = model.tobacco_use_amount;
                    admissions.wnl = model.wnl;
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
        /// delete a provider info 
        /// </summary>
        /// <returns></returns>
        public void DeleteObAdmissionById(int id, int userId)
        {

            try
            {
                var model = (from m in _dbContext.ob_admissions
                             where m.id == id
                             select m).FirstOrDefault();
                model.active = false;
                model.modifier_id = userId;
                model.modified = DateTime.Now;
                _dbContext.Update(model);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                var loggers = new Loggers(_iconfiguration);
                loggers.WriteLog(ex.Message.ToString());

            }
        }
    }
}
