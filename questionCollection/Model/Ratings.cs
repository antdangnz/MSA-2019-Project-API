using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace questionCollection.Model
{
    public partial class Ratings
    {
        [Column("ratingId")]
        public int RatingId { get; set; }
        [Column("questionId")]
        public int? QuestionId { get; set; }
        [Column("rating")]
        public byte? Rating { get; set; }
        [Column("rating_description", TypeName = "text")]
        public string RatingDescription { get; set; }
        [Column("date_created", TypeName = "date")]
        public DateTime? DateCreated { get; set; }

        [ForeignKey("QuestionId")]
        [InverseProperty("Ratings")]
        public virtual Questions Question { get; set; }
    }
}
