using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Platx_Admin.Models;
using Platx_Admin.Services;
using Platx_Admin.Shared.Exceptions;
using System.Collections.Generic;

namespace Platx_Admin.Controllers
{
    [ApiController]
    [Route("api/plans")]
    public class PlansController : ControllerBase
    {
        private readonly IPlansRepository _plansRepository;
        private readonly IMapper _mapper;


        public PlansController(IPlansRepository plansRepository, IMapper mapper)
        {
            _plansRepository = plansRepository;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanWithoutPlanFeatureDto>>> GetPlans([FromQuery] bool includePlanFeatures)
        {

            var plansEntity = await _plansRepository.GetPlansAsync(includePlanFeatures);

            if (plansEntity == null)
            {
                throw new NotFoundException($"Plans was not found.");
            }
            if (includePlanFeatures)
            {
                return Ok(_mapper.Map<IEnumerable<PlanWithPlanFeatureDto>>(plansEntity));
            }
            else
            {
                return Ok(_mapper.Map<IEnumerable<PlanWithoutPlanFeatureDto>>(plansEntity));

            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlanDto>> GetPlan(int id, [FromQuery] bool includePlanFeatures)
        {
            var planEntity = await _plansRepository.GetPlanAsync(id, includePlanFeatures);

            if (planEntity == null)
            {
                throw new NotFoundException($"Plan with ID {id} was not found.");
            }
            return Ok(_mapper.Map<PlanWithPlanFeatureDto>(planEntity));

        }


    }
}