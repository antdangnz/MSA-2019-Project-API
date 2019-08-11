using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace questionCollection.Model
{
    public partial class Authors
    {
        [Column("authorId")]
        public int AuthorId { get; set; }
        [Column("questionId")]
        public int? QuestionId { get; set; }
        [Column("author")]
        [StringLength(50)]
        public string Author { get; set; }
        [Column("class_name")]
        [StringLength(30)]
        public string ClassName { get; set; }
        [Column("class_number")]
        [StringLength(30)]
        public string ClassNumber { get; set; }
        [Column("institution")]
        [StringLength(50)]
        public string Institution { get; set; }

        [ForeignKey("QuestionId")]
        [InverseProperty("Authors")]
        public virtual Questions Question { get; set; }
    }
}
