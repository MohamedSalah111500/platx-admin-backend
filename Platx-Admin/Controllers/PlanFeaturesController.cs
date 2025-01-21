using AutoMapper;
using AutoMapper.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Platx_Admin.Models;
using Platx_Admin.Services;
using Platx_Admin.Shared.Exceptions;
using System.Linq;

namespace Platx_Admin.Controllers
{
    [Route("api/plans/{planId}/features")]
    [ApiController]
    public class PlanFeaturesController : ControllerBase
    {
        private readonly ILogger<PlanFeaturesController> _logger;
        private readonly IMailServices _mailService;
        private readonly IPlansRepository _plansRepository;
        private readonly IMapper _mapper;


        public PlanFeaturesController(
            ILogger<PlanFeaturesController> logger,
            IMailServices localMailService,
            IPlansRepository plansRepository,
            IMapper mapper
            )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = localMailService ?? throw new ArgumentNullException(nameof(localMailService));
            _plansRepository = plansRepository ?? throw new ArgumentNullException(nameof(plansRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }


        public ILogger<PlanFeaturesController> Logger { get; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanFeatureDto>>> GetPlanFeatures(int planId)
        {

            var planFeaturesEntity = await _plansRepository.GetPlanFeaturesAsync(planId);

            if (planFeaturesEntity == null || !planFeaturesEntity.Any())
            {
                _logger.LogInformation($"Plan Feature with id {planId} is not found");
                throw new NotFoundException($"Plan Feature with id {planId} not found.");
            }

            return Ok(_mapper.Map<IEnumerable<PlanFeatureDto>>(planFeaturesEntity));
        }

        [HttpGet("{featureId}")]
        public async Task<ActionResult<PlanFeatureDto>> GetPlanFeature(int planId, int featureId)
        {


            if (!await _plansRepository.PlanExistAsync(planId))
            {
                _logger.LogInformation($"Plan  with id {featureId} is not found");
                throw new NotFoundException($"Plan Feature with id {featureId} not found.");
            }


            var planFeatureEntity = await _plansRepository.GetPlanFeatureAsync(planId, featureId);

            if (planFeatureEntity == null)
            {
                _logger.LogInformation($"Plan Feature with id {featureId} is not found");
                throw new NotFoundException($"Plan Feature with id {featureId} not found.");
            }

            return Ok(_mapper.Map<PlanFeatureDto>(planFeatureEntity));

        }

        [HttpPost]
        public async Task<ActionResult<PlanFeatureDto>> CreatePlanFeature(int planId, PlanFeatureForCreationDto planFeature)
        {
            if (!await _plansRepository.PlanExistAsync(planId))
            {
                throw new NotFoundException($"Plan with this id: {planId} not found.");
            }

            var finalPlanFeature = _mapper.Map<Entities.PlanFeature>(planFeature);

            await _plansRepository.CreatePlanFeatureAsync(planId, finalPlanFeature);

            await _plansRepository.SaveChangesAsync();

            var createdPlanFeatureToReturn = _mapper.Map<Models.PlanFeatureDto>(finalPlanFeature);

            return Ok(createdPlanFeatureToReturn);

        }

        [HttpPut("{planFeatureId}")]
        public async Task<ActionResult<PlanFeatureDto>> UpdatePlanFeature(int planId, int planFeatureId, PlanFeatureForUpdateDto planFeature)
        {
            if (!await _plansRepository.PlanExistAsync(planId))
            {
                throw new NotFoundException($"Plan with this id: {planId} not found.");
            }

            var planFeatureEntity = await _plansRepository.GetPlanFeatureAsync(planId, planFeatureId);
            if (planFeatureEntity == null)
            {
                throw new NotFoundException($"Plan Feature with this id: {planFeatureId} not found.");
            }

            _mapper.Map(planFeature, planFeatureEntity);

            await _plansRepository.SaveChangesAsync();

            return Ok(planFeatureEntity);

        }

        [HttpDelete("{planFeatureId}")]
        public async Task<ActionResult<PlanFeatureDto>> DeletePlanFeature(int planId,  int planFeatureId)
        {
            if (!await _plansRepository.PlanExistAsync(planId))
            {
                throw new NotFoundException($"Plan with this id: {planId} not found.");
            }

            var planFeatureEntity = await _plansRepository.GetPlanFeatureAsync(planId, planFeatureId);
            if (planFeatureEntity == null)
            {
                throw new NotFoundException($"Plan Feature with this id: {planFeatureId} not found.");
            }

            _plansRepository.DeletePlanFeatureAsync(planFeatureEntity);
            await _plansRepository.SaveChangesAsync();

            return NoContent();

        }

    }
}