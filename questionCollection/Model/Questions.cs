using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace questionCollection.Model
{
    [Table("questions")]
    public partial class Questions
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("class_name")]
        [StringLength(30)]
        public string ClassName { get; set; }
        [Column("class_number")]
        [StringLength(30)]
        public string ClassNumber { get; set; }
        [Column("institution")]
        [StringLength(50)]
        public string Institution { get; set; }
        [Column("question_type")]
        [StringLength(30)]
        public string QuestionType { get; set; }
        [Column("question_text", TypeName = "text")]
        public string QuestionText { get; set; }
        [Column("answer", TypeName = "text")]
        public string Answer { get; set; }
        [Column("author")]
        [StringLength(50)]
        public string Author { get; set; }
        [Column("rating")]
        public byte? Rating { get; set; }
        [Column("date_created", TypeName = "date")]
        public DateTime? DateCreated { get; set; }
    }
}
