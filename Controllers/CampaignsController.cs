using System.Collections.Generic;
using System.Threading.Tasks;
using api.leads.Data.Models;
using api.leads.Data.Repositories;
using api.leads.DTO.Request;
using api.leads.DTO.Response;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.leads.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class CampaignsController : ControllerBase
    {
        private readonly ICampaignsRepository _campaignsRepository;
        protected readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<CampaignsController> _logger;

        public CampaignsController(ICampaignsRepository campaignsRepository,
                                    IConfiguration configuration,
                                    IMapper mapper,
                                    ILogger<CampaignsController> logger)
        {
            _campaignsRepository = campaignsRepository;
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        ///     GetCampaigns
        /// </summary>
        /// <remarks>Returns a list of all the lead campaigns.</remarks>
        /// <param name="client"></param>
        /// <returns></returns >
        [HttpGet("{client}")]
        public async Task<ActionResult<List<CampaignGetResponse>>> GetCampaigns(string client)
        {
            try
            {
                ICollection<Campaign> campaigns = await _campaignsRepository.GetCampaigns(client);

                if (campaigns == null)
                {
                    return StatusCode(500, "An unknown exception has occurred in the repository.");
                }

                List<CampaignGetResponse> campaignGetResponse = _mapper.Map<List<CampaignGetResponse>>(campaigns);
                return campaignGetResponse;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An unknown exception has occurred in the controller.");
            }

        }

        /// <summary>
        ///     GetEntryCampaigns
        /// </summary>
        /// <remarks>Returns a list of all the active lead entry campaigns.</remarks>
        /// <returns></returns >
        [HttpGet("")]
        public async Task<ActionResult<List<CampaignGetResponse>>> GetEntryCampaigns()
        {
            try
            {
                ICollection<Campaign> campaigns = await _campaignsRepository.GetEntryCampaigns();

                if (campaigns == null)
                {
                    return StatusCode(500, "An unknown exception has occurred in the repository.");
                }

                List<CampaignGetResponse> campaignGetResponse = _mapper.Map<List<CampaignGetResponse>>(campaigns);
                return campaignGetResponse;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An unknown exception has occurred in the controller.");
            }
        }

        /// <summary>
        ///     GetCampaignFields
        /// </summary>
        /// <remarks>Returns a list of campaign fields.</remarks>
        /// <param name="campaignId"></param>
        /// <returns></returns >
        [HttpGet("{campaignId}")]
        public async Task<ActionResult<List<CampaignFieldsGetResponse>>> GetCampaignFields(int campaignId)
        {
            try
            {
                ICollection<CampaignFields> campaignFields = await _campaignsRepository.GetCampaignFields(campaignId);

                if (campaignFields == null)
                {
                    return StatusCode(500, "An unknown exception has occurred in the repository.");
                }

                List<CampaignFieldsGetResponse> campaignFieldsGetResponse = _mapper.Map<List<CampaignFieldsGetResponse>>(campaignFields);
                return campaignFieldsGetResponse;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An unknown exception has occurred in the repository.");
            }
        }

        /// <summary>
        ///     InsertCampaign
        /// </summary>
        /// <remarks>Inserts a new lead campaign.</remarks>
        /// <param name="campaignPostRequest"></param>
        /// <returns></returns >
        [HttpPost()]
        public async Task<ActionResult<List<CampaignIdGetResponse>>> InsertCampaign(CampaignPostRequest campaignPostRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(400, ModelState.ValidationState);
                }

                ICollection<CampaignId> campaignIds = await _campaignsRepository.InsertCampaign(campaignPostRequest);

                if (campaignIds == null)
                {
                    return StatusCode(500, "An unknown exception has occurred in the repository.");
                }

                List<CampaignIdGetResponse> campaignIdGetResponse = _mapper.Map<List<CampaignIdGetResponse>>(campaignIds);
                return campaignIdGetResponse;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An unknown exception has occurred in the controller.");
            }
        }

        /// <summary>
        ///     UpdateCampaign
        /// </summary>
        /// <remarks>Updates an existing lead campaign.</remarks>
        /// <param name="campaignPostRequest"></param>
        /// <returns></returns >
        [HttpPost()]
        public async Task<ActionResult> UpdateCampaign(CampaignPostRequest campaignPostRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(400, ModelState.ValidationState);
                }

                var success = await _campaignsRepository.UpdateCampaign(campaignPostRequest);

                if (success)
                {
                    return StatusCode(204, ModelState.ValidationState);
                }
                else
                {
                    return StatusCode(500, "An unknown exception has occurred in the repository.");
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An unknown exception has occurred in the controller.");
            }
        }
    }
}
