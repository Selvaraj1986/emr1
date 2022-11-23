using System.ComponentModel.DataAnnotations;

namespace emr.Models
{
    public class courses
    {

        [Key]
        [Required]
        public int id { get; set; }
        public int canvas_id { get; set; }
        public string? sis_course_id { get; set; }
        public string? integration_id { get; set; }
        public string? name { get; set; }
        public string? course_code { get; set; }
        public string? workflow_state { get; set; }
        public int account_id { get; set; }
        public int root_account_id { get; set; }
        public int enrollment_term_id { get; set; }
        public DateTime start_at { get; set; }
        public DateTime end_at { get; set; }
        public string? enrollments { get; set; }
        public int total_students { get; set; }
        public string? calendar { get; set; }
        public string? default_view { get; set; }
        public string? syllabus_body { get; set; }
        public int needs_grading_count { get; set; }
        public string? term { get; set; }
        public string? course_progress { get; set; }
        public bool apply_assignment_group_weights { get; set; }
        public string? permissions { get; set; }
        public bool is_pubic { get; set; }
        public bool is_pubic_to_auth_users { get; set; }
        public bool public_syllabus { get; set; }
        public string? public_description { get; set; }
        public int storage_quota_mb { get; set; }
        public int storage_quota_used_mb { get; set; }
        public bool hide_final_grades { get; set; }
        public string? license { get; set; }
        public bool allow_student_assignment_edits { get; set; }
        public bool allow_wiki_comments { get; set; }
        public bool allow_student_forum_attachments { get; set; }
        public bool open_enrollment { get; set; }
        public bool self_enrollment { get; set; }
        public bool restrict_enrollments_to_course_dates { get; set; }
        public string? course_format { get; set; }
        public bool active { get; set; }
        public DateTime created { get; set; }
        public DateTime modified { get; set; }
        public int creator_id { get; set; }
        public int modifier_id { get; set; }

    }
}
