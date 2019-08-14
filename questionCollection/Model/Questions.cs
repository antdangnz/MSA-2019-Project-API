using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuestionCollection.Model
{
    public partial class Questions
    {
        public Questions()
        {
            Ratings = new HashSet<Ratings>();
        }

        [Column("questionId")]
        public int QuestionId { get; set; }
        [Column("class_name")]
        [StringLength(30)]
        public string ClassName { get; set; }
        [Column("class_number")]
        [StringLength(30)]
        public string ClassNumber { get; set; }
        [Column("author")]
        [StringLength(50)]
        public string Author { get; set; }
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
        [Column("date_created", TypeName = "date")]
        public DateTime? DateCreated { get; set; }

        [InverseProperty("Question")]
        public virtual ICollection<Ratings> Ratings { get; set; }
    }
}
