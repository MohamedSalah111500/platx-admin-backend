using AutoMapper;

namespace Platx_Admin.Profiles
{
    public class PlanProfile : Profile
    {

        public PlanProfile()
        {
            CreateMap<Entities.Plan, Models.PlanWithoutPlanFeatureDto>();

            CreateMap<Entities.Plan, Models.PlanWithPlanFeatureDto>()
                .ForMember(dest => dest.Features, opt => opt.MapFrom(src => src.planFeatures));
        }
    }
}
