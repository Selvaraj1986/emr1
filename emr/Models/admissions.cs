namespace emr.Models
{
    public class admissions
    {
        public int id { get; set; }
        public DateTime created { get; set; }
        public int creator_id { get; set; }
        public DateTime modified { get; set; }
        public int modifier_id { get; set; }
        public DateTime record_date { get; set; }
        public int patient_id { get; set; }
        public string? complaint { get; set; }
        public bool lung_problems { get; set; }
        public string? lung_problems_description { get; set; }
        public bool stomach_problems { get; set; }
        public string? stomach_problems_description { get; set; }
        public bool thyroid_problems { get; set; }
        public string? thyroid_problems_description { get; set; }
        public bool neurological_problems { get; set; }
        public string? neurological_problems_description { get; set; }
        public bool heart_problems { get; set; }
        public string? heart_problems_description { get; set; }
        public bool liver_problems { get; set; }
        public string? liver_problems_description { get; set; }
        public bool vision_problems { get; set; }
        public string? vision_problems_description { get; set; }
        public bool kidney_problems { get; set; }
        public string? kidney_problems_description { get; set; }
        public bool arthritis_problems { get; set; }
        public string? arthrities_problems_description { get; set; }
        public bool diabetes_problems { get; set; }
        public string? diabetes_problems_description { get; set; }
        public bool chronic_infection { get; set; }
        public string? chronic_infection_description { get; set; }
        public bool cancer { get; set; }
        public string? cancer_description { get; set; }
        public bool family_nsf { get; set; }
        public bool family_heart_disease { get; set; }
        public bool family_hypertension { get; set; }
        public bool family_diabetes { get; set; }
        public bool family_stroke { get; set; }
        public bool family_seizures { get; set; }
        public bool family_kidney_disease { get; set; }
        public bool family_liver_disease { get; set; }
        public string? family_desc { get; set; }
        public string? other_history { get; set; }
        public string? informant { get; set; }
        public string? interpretive { get; set; }
        public string? admitted_via { get; set; }
        public string? admitted_from { get; set; }
        public string? contact { get; set; }
        public string? orientation { get; set; }
        public string? belongings { get; set; }
        public string? valuables_disposition { get; set; }
        public string? valuables_comments { get; set; }
        public bool living_will { get; set; }
        public bool power_of_attorney { get; set; }
        public bool organ_donor { get; set; }
        public string? directives_comments { get; set; }
        public bool no_past_med { get; set; }
        public bool cardiac_past_med { get; set; }
        public bool respiratory_past_med { get; set; }
        public bool others_past_med { get; set; }
        public string? past_comments { get; set; }
        public string? past_surg_history { get; set; }
        public string? tetanus { get; set; }
        public string? influenza { get; set; }
        public string? pneumonia { get; set; }
        public string? hepatitisb { get; set; }
        public string? dpt { get; set; }
        public string? polio { get; set; }
        public string? chickenpox { get; set; }
        public string? immu_comments { get; set; }
        public string? patient_lives { get; set; }
        public string? social_abuse { get; set; }
        public string? social_observations { get; set; }
        public string? social_support_services { get; set; }
        public string? social_support_other_services { get; set; }
        public string? social_comments { get; set; }
        public string? drug_use_freq { get; set; }
        public string? last_drug_use { get; set; }
        public string? alcohol_use_freq { get; set; }
        public string? last_alcohol_use { get; set; }
        public string? tabocco_use_freq { get; set; }
        public string? last_tabocco_use { get; set; }
        public string? substance_use { get; set; }
        public bool impaired_hearing { get; set; }
        public bool impaired_vision { get; set; }
        public bool can_perform_adl { get; set; }
        public bool can_read { get; set; }
        public bool can_write { get; set; }
        public bool hearing_aid_left { get; set; }
        public bool hearing_aid_right { get; set; }
        public bool use_glasses { get; set; }
        public bool use_contacts { get; set; }
        public bool use_dentures_upper { get; set; }
        public bool use_dentures_lower { get; set; }
        public bool use_walker { get; set; }
        public bool use_crutches { get; set; }
        public bool use_wheelchair { get; set; }
        public bool use_cane { get; set; }
        public bool use_prosthesis { get; set; }
        public string? comments { get; set; }
        public bool active { get; set; }
        public string? gardasil { get; set; }
    }
}
