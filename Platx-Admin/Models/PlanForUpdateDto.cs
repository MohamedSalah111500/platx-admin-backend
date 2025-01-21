namespace Platx_Admin.Models
{
    public class PlanForUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal PriceAfterDiscount { get; set; }
        public bool HasDiscount { get; set; }
    }
}
