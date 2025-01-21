using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Platx_Admin.DbContexts;
using Platx_Admin.Entities;
using Platx_Admin.Models;
using System.Numerics;

namespace Platx_Admin.Services
{
    public class PlansRepository : IPlansRepository
    {
        private readonly PlatXAdminContext _context;

        public PlansRepository(PlatXAdminContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Plan>> GetPlansAsync(bool includePlanFeatures)
        {
            if (includePlanFeatures)
            {
                return await _context.Plans
                    .Include(p => p.planFeatures)
                    .OrderBy(p => p.Name)
                    .ToListAsync();
            }
            else
            {
                return await _context.Plans.OrderBy(p => p.Name).ToListAsync();
            }
        }

        public async Task<Plan?> GetPlanAsync(int planId, bool includePlanFeatures)
        {
            if (includePlanFeatures)
            {
                return await _context.Plans
                    .Where(p => p.Id == planId)
                    .Include(p => p.planFeatures)
                    .FirstOrDefaultAsync();
            }
            else
            {
                return await _context.Plans
                   .Where(p => p.Id == planId)
                   .FirstOrDefaultAsync();
            }
        }
        public void CreatePlan(Plan plan)
        {
            if (plan != null)
            {
                _context.Plans.Add(plan);
            }
        }

        public void DeletePlan(Plan plan)
        {
            _context.Plans.Remove(plan);
        }

        public async Task<bool> PlanExistAsync(int planId)
        {
            return await _context.Plans.AnyAsync(p => p.Id == planId);
        }

        public async Task<IEnumerable<PlanFeature>> GetPlanFeaturesAsync(int planId)
        {

            return await _context.PlanFeatures
                   .Where(f => f.PlanId == planId)
                   .ToListAsync();
        }
        public async Task<PlanFeature?> GetPlanFeatureAsync(int planId, int featureId)
        {
            return await _context.PlanFeatures
                  .Where(f => f.PlanId == planId && f.Id == featureId)
                  .FirstOrDefaultAsync();
        }

        public async Task CreatePlanFeatureAsync(int planId, PlanFeature planFeature)
        {
            var plan = await GetPlanAsync(planId, false);
            if (plan != null)
            {
                plan.planFeatures.Add(planFeature);
            }

        }
        public void DeletePlanFeatureAsync(PlanFeature planFeature)
        {
            _context.PlanFeatures.Remove(planFeature);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

       
    }
}