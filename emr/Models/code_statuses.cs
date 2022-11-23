﻿namespace emr.Models
{
    public class code_statuses
    {
        public int id { get; set; }
        public DateTime created { get; set; }
        public int creator_id { get; set; }
        public DateTime modified { get; set; }
        public int modifier_id { get; set; }
        public DateTime record_date { get; set; }
        public int patient_id { get; set; }
        public string? code_status { get; set; }
        public string? notes { get; set; }
        public bool active { get; set; }
    }
}
