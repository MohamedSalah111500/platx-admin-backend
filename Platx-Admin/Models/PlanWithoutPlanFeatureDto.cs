namespace Platx_Admin.Models
{
    public class PlanWithoutPlanFeatureDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public decimal PriceAfterDiscount { get; set; }

        public bool HasDiscount { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
