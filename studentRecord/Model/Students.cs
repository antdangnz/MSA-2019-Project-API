using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace studentRecord.Model
{
    [Table("students")]
    public partial class Students
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("first_name")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Column("last_name")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Column("year")]
        [StringLength(10)]
        public string Year { get; set; }
        [Column("homeroom")]
        [StringLength(5)]
        public string Homeroom { get; set; }
        [Column("phone_number")]
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        [Column("email_address")]
        [StringLength(50)]
        public string EmailAddress { get; set; }
        [Column("emergency_contact")]
        [StringLength(50)]
        public string EmergencyContact { get; set; }
        [Column("emergency_number")]
        [StringLength(20)]
        public string EmergencyNumber { get; set; }
        [Column("emergency_email")]
        [StringLength(50)]
        public string EmergencyEmail { get; set; }
        [Column("dob", TypeName = "date")]
        public DateTime? Dob { get; set; }
        [Column("date_created", TypeName = "date")]
        public DateTime? DateCreated { get; set; }
    }
}
