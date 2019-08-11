using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace questionCollection.Model
{
    public partial class Questions
    {
        public Questions()
        {
            Authors = new HashSet<Authors>();
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
        public virtual ICollection<Authors> Authors { get; set; }
        [InverseProperty("Question")]
        public virtual ICollection<Ratings> Ratings { get; set; }
    }
}
