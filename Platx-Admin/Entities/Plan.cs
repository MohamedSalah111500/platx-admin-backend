using Platx_Admin.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Platx_Admin.Entities
{
    public class Plan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public decimal Price { get; set; }

        public decimal PriceAfterDiscount { get; set; }

        public bool HasDiscount { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public ICollection<PlanFeature> planFeatures { get; set; } = new List<PlanFeature>();

        public Plan(string name)
        {
            Name = name;
        }
    }
}
