using System.Runtime.InteropServices;

namespace Platx_Admin.Models
{
    public class PlanFeatureDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
