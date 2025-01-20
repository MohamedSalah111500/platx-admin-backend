using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Platx_Admin.Entities
{
    public class PlanFeature
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [ForeignKey("PlanId")]
        public Plan? Plan { get; set; }
        public int PlanId { get; set; }

        public PlanFeature(string content)
        {
            Content = content;
        }
    }
}
