using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logic.Models
{
    public class Tag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? TagId { get; set; }
        public string Name { get; set; }
        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
