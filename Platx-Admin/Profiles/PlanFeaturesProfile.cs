using AutoMapper;
using Platx_Admin;

namespace Platx_Admin.Profiles
{
    public class PlanFeaturesProfile:Profile
    {

        public PlanFeaturesProfile()
        {
            CreateMap<Entities.PlanFeature, Models.PlanFeatureDto>();
            CreateMap<Entities.PlanFeature, Models.PlanFeatureDto>();
            CreateMap<Models.PlanFeatureForCreationDto , Entities.PlanFeature>();
            CreateMap<Models.PlanFeatureForUpdateDto, Entities.PlanFeature>();


        }
    }
}
