using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logic.Models
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionId { get; set; }
        public string Description { get; set; }
        public int? AnswerId { get; set; }
        public int? TagId { get; set; }
        [ForeignKey("TagId")]
        public virtual Tag? Tag { get; set; }
    }
}
