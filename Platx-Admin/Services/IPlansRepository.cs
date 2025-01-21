using Platx_Admin.Entities;
using Platx_Admin.Models;

namespace Platx_Admin.Services
{
    public interface IPlansRepository
    {
        Task<IEnumerable<Plan>> GetPlansAsync(bool includePlanFeatures);
        Task<Plan?> GetPlanAsync(int planId, bool includePlanFeatures);
        Task<bool> PlanExistAsync(int planId);
        void CreatePlan(Plan plan);
        void DeletePlan(Plan plan);

        Task<IEnumerable<PlanFeature>> GetPlanFeaturesAsync(int planId);
        Task<PlanFeature?> GetPlanFeatureAsync(int planId, int featureId);
        Task CreatePlanFeatureAsync(int planId, PlanFeature planFeature);
        void DeletePlanFeatureAsync(PlanFeature planFeature);
        Task<bool> SaveChangesAsync();

    }
}
