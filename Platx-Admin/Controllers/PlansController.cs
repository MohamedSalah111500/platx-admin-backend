using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Platx_Admin.Entities;
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

        [HttpGet("{planId}")]
        public async Task<ActionResult<PlanDto>> GetPlan(int planId, [FromQuery] bool includePlanFeatures)
        {
            var planEntity = await _plansRepository.GetPlanAsync(planId, includePlanFeatures);

            if (planEntity == null)
            {
                throw new NotFoundException($"Plan with ID {planId} was not found.");
            }
            return Ok(_mapper.Map<PlanWithPlanFeatureDto>(planEntity));

        }

        [HttpPost]
        public async Task<ActionResult<PlanDto>> CreatePlan(PlanForCreationDto plan)
        {

            var finalPlan = _mapper.Map<Entities.Plan>(plan);

            _plansRepository.CreatePlan(finalPlan);

            await _plansRepository.SaveChangesAsync();

            var createdPlanToReturn = _mapper.Map<Models.PlanDto>(finalPlan);

            return Ok(createdPlanToReturn);

        }

        [HttpPut("{planId}")]
        public async Task<ActionResult<PlanFeatureDto>> UpdatePlanFeature(int planId, PlanForUpdateDto plan)
        {

            var planEntity = await _plansRepository.GetPlanAsync(planId, false);

            if (planEntity == null)
            {
                throw new NotFoundException($"Plan with this id: {planId} not found.");
            }

            _mapper.Map(plan, planEntity);

            await _plansRepository.SaveChangesAsync();

            return Ok(planEntity);
        }

        [HttpDelete("{planId}")]
        public async Task<ActionResult<PlanFeatureDto>> DeletePlanFeature(int planId)
        {
            var planEntity = await _plansRepository.GetPlanAsync(planId, false);

            if (planEntity == null)
            {
                throw new NotFoundException($"Plan with this id: {planId} not found.");
            }

            _plansRepository.DeletePlan(planEntity);
            await _plansRepository.SaveChangesAsync();

            return NoContent();

        }

    }
}