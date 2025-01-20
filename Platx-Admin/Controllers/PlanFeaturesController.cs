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

        //[HttpPut("{planFeatureId}")]
        //public ActionResult<PlanFeatureDto> UpdatePlanFeature(int planId, int planFeatureId, PlanFeatureForUpdateDto planFeature)
        //{
        //    //@TODO check if plan exist 
        //    var feature = planFeatuers.FirstOrDefault<PlanFeatureDto>();
        //    if (feature == null)
        //    {
        //        return NotFound();
        //    }
        //    feature.Content = planFeature.Content;

        //    return Ok(feature);

        //}

        //[HttpPatch("{planFeatureId}")]
        //public ActionResult<PlanFeatureDto> PartiallyUpdatePlanFeature(int planId, int planFeatureId,
        //    JsonPatchDocument<PlanFeatureForUpdateDto> patchDocument)
        //{
        //    //@TODO check if plan exist 
        //    var feature = planFeatuers.FirstOrDefault<PlanFeatureDto>();
        //    if (feature == null)
        //    {
        //        return NotFound();
        //    }
        //    var planFeatureToPatch = new PlanFeatureForUpdateDto()
        //    {
        //        Content = "new content"
        //    };
        //    patchDocument.ApplyTo(planFeatureToPatch, ModelState);

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    return Ok(feature);

        //}


        //[HttpDelete("{planFeatureId}")]
        //public ActionResult<PlanFeatureDto> DeletePlanFeature(int planFeatureId)
        //{
        //    //@TODO check if plan exist 
        //    var planFeature = planFeatuers.FirstOrDefault<PlanFeatureDto>();
        //    if (planFeature == null)
        //    {
        //        return NotFound();
        //    }
        //    return NoContent();

        //}

    }
}