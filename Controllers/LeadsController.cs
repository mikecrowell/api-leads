using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using api.leads.Data.Repositories;
using api.leads.DTO.Request;
using api.leads.DTO.Response;
using api.leads.Data.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System;

namespace api.leads.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class LeadsController : ControllerBase
    {
        private readonly ILeadsRepository _leadRepository;
        protected readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<LeadsController> _logger;

        public LeadsController(ILeadsRepository leadsRepository,
                                IConfiguration configuration,
                                IMapper mapper,
                                ILogger<LeadsController> logger)
        {
            _leadRepository = leadsRepository;
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
        }

        #region HyCite Endpoints
        /// <summary>
        ///     GetLeadCCPAData
        /// </summary>
        /// <remarks>Returns possible CCPA Data</remarks>
        /// <returns></returns >
        [HttpPost()]
        public async Task<ActionResult<CCPAGetResponse>> LeadCCPAData(CCPALeadsPostRequest ccpaRequest)
        {
            CCPAGetResponse ccpaResponse = new CCPAGetResponse();
            List<Result> Results;
            try
            {
                Results = await _leadRepository.GetLeadCCPAData(ccpaRequest);
                ccpaResponse.ResultCount = Results.Count;
                ccpaResponse.result = new List<Result>();
                if (Results.Count > 0)
                {
                    foreach (Result result in Results)
                    {
                        ccpaResponse.result.Add(result);
                    }
                }
                return ccpaResponse;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                if (ex.Message.Contains("PLEASE FILL IN AT LEAST ONE VALUE FOR EMAIL, FIRSTNAME, LASTNAME, PHONENUMBER, ZIP"))
                    return StatusCode(400, ex.Message);
                else
                    return StatusCode(500, "An unknown exception has occurred in the controller.");
            }
        }

        /// <summary>
        ///     GetLeadTypes
        /// </summary>
        /// <remarks>Returns a list of different lead types that are available as options for campaigns.</remarks>
        /// <returns></returns >
        [HttpGet()]
        public async Task<ActionResult<List<LeadTypeGetResponse>>> GetLeadTypes()
        {
            try
            {
                ICollection<LeadType> leadTypes = await _leadRepository.GetLeadTypes();

                if (leadTypes == null)
                {
                    return StatusCode(500, "An unknown exception has occurred in the repository.");
                }

                List<LeadTypeGetResponse> leadTypeGetResponse = _mapper.Map<List<LeadTypeGetResponse>>(leadTypes);
                return leadTypeGetResponse;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An unknown exception has occurred in the controller.");
            }
        }

        [HttpGet("{client}/{leadTypeId}")]
        public async Task<ActionResult<int>> GetPartialLeadCampaignId(string client, int leadTypeId)
        {
            try
            {
                int partialLeadCampaignId = await Task.Run(() => _leadRepository.GetPartialLeadCampaign(client, leadTypeId).Result);

                if (partialLeadCampaignId == 0)
                {
                    return StatusCode(500, "No partial campaign found.");
                }

                return partialLeadCampaignId;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An unknown exception has occurred in the controller.");
            }
        }

        /// <summary>
        ///     GetDuplicateLeads
        /// </summary>
        /// <remarks>Returns a list of duplicate leads.</remarks>
        /// <param name="client"></param>
        /// <returns></returns >
        [HttpGet("{client}")]
        public async Task<ActionResult<List<DuplicateLeadsGetResponse>>> GetDuplicateLeads(string client)
        {
            try
            {
                ICollection<DuplicateLead> leads = await _leadRepository.GetDuplicateLeads(client);

                if (leads == null)
                {
                    return StatusCode(500, "An unknown exception has occurred in the repository.");
                }

                List<DuplicateLeadsGetResponse> duplicateLeadsGetResponse = _mapper.Map<List<DuplicateLeadsGetResponse>>(leads);
                return duplicateLeadsGetResponse;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An unknown exception has occurred in the controller.");
            }
        }

        /// <summary>
        ///     GetDuplicateInfoLeads
        /// </summary>
        /// <remarks>Returns a list of duplicate info leads.</remarks>
        /// <param name="leadId"></param>
        /// <returns></returns >
        [HttpGet("{leadId}")]
        public async Task<ActionResult<List<DuplicateLeadsGetResponse>>> GetDuplicateInfoLeads(int leadId)
        {
            try
            {
                ICollection<DuplicateLead> leads = await _leadRepository.GetDuplicateInfoLeads(leadId);

                if (leads == null)
                {
                    return StatusCode(500, "An unknown exception has occurred in the repository.");
                }

                List<DuplicateLeadsGetResponse> duplicateLeadsGetResponse = _mapper.Map<List<DuplicateLeadsGetResponse>>(leads);
                return duplicateLeadsGetResponse;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An unknown exception has occurred in the controller.");
            }
        }

        /// <summary>
        ///     DeleteLead
        /// </summary>
        /// <remarks>Deletes a lead that was flagged as a duplicate on review.</remarks>
        /// <param name="leadId"></param>
        /// <returns></returns >
        [HttpPost("{leadId}")]
        public async Task<ActionResult> DeleteLead(int leadId)
        {
            try
            {
                await _leadRepository.DeleteLead(leadId);
                return StatusCode(204);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                if (ex.Message.Contains("NO VALID LEAD EXISTS TO DELETE"))
                    return StatusCode(400, ex.Message);
                else
                    return StatusCode(500, "An unknown exception has occurred in the controller.");
            }
        }

        /// <summary>
        ///     AcceptDuplicate
        /// </summary>
        /// <remarks>Accepts a lead that was flagged as a duplicate on insert.</remarks>
        /// <param name="leadId"></param>
        /// <param name="userId"></param>
        /// <returns></returns >
        [HttpPost("{leadId}")]
        public async Task<ActionResult> AcceptDuplicate(int leadId, string userId)
        {
            try
            {
                var success = await _leadRepository.AcceptDuplicate(leadId, userId);

                if (success)
                {
                    return StatusCode(204);
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

        /// <summary>
        ///     GetPossibleLeads
        /// </summary>
        /// <remarks>Returns a list of possible leads.</remarks>
        /// <param name="state"></param>
        /// <param name="client"></param>
        /// <param name="distributorNumber"></param>
        /// <returns></returns >
        [HttpGet("{client}/{state}/{distributorNumber}")]
        public async Task<ActionResult<List<PossibleLeadsGetResponse>>> GetPossibleLeads(string state, string client, string distributorNumber)
        {
            try
            {
                ICollection<PossibleLeads> leads = await _leadRepository.GetPossibleLeads(state, client, distributorNumber);

                if (leads == null)
                {
                    return StatusCode(500, "An unknown exception has occurred in the repository.");
                }

                List<PossibleLeadsGetResponse> possibleLeadsGetResponse = _mapper.Map<List<PossibleLeadsGetResponse>>(leads);

                return possibleLeadsGetResponse;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An unknown exception has occurred in the controller.");
            }
        }

        /// <summary>
        ///     GetPurchasedLeads
        /// </summary>
        /// <remarks>Returns a list of purchased leads.</remarks>
        /// <param name="distributorNumber"></param>
        /// <returns></returns >
        [HttpGet("{distributorNumber}")]
        public async Task<ActionResult<List<PurchasedLeadsGetResponse>>> GetPurchasedLeads(string distributorNumber)
        {
            try
            {
                ICollection<PurchasedLeads> leads = await _leadRepository.GetPurchasedLeads(distributorNumber);

                if (leads == null)
                {
                    return StatusCode(500, "An unknown exception has occurred in the repository.");
                }

                List<PurchasedLeadsGetResponse> purchasedLeadsGetResponse = _mapper.Map<List<PurchasedLeadsGetResponse>>(leads);
                return purchasedLeadsGetResponse;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An unknown exception has occurred in the controller.");
            }
        }

        /// <summary>
        ///     PurchaseLeads
        /// </summary>
        /// <remarks>Purchases leads from a campaign.</remarks>
        /// <param name="purchaseLeadsPostRequest" ></param>
        /// <returns></returns >
        [HttpPost()]
        public async Task<ActionResult<List<PurchaseLeadsGetResponse>>> PurchaseLeads(PurchaseLeadsPostRequest purchaseLeadsPostRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(400, ModelState);
                }

                ICollection<PurchaseLeads> leads = await _leadRepository.PurchaseLeads(purchaseLeadsPostRequest);
                List<PurchaseLeadsGetResponse> purchaseLeadsGetResponse = _mapper.Map<List<PurchaseLeadsGetResponse>>(leads);
                return purchaseLeadsGetResponse;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                if (ex.Message.Contains("TOO MANY LEADS PURCHASED WITHIN 24 HOURS") || ex.Message.Contains("PURCHASED TOTAL DOES NOT EQUAL REQUESTED TOTAL"))
                    return StatusCode(400, ex.Message);
                else
                    return StatusCode(500, "An unknown exception has occurred in the controller.");
            }
        }

        #endregion

        #region Marketing Endpoints
        /// <summary>
        ///     InsertEntryLead
        /// </summary>
        /// <remarks>Inserts a new entry lead with zip code.</remarks>
        /// <param name = "entryPostRequest" ></param>
        /// <returns></returns >
        [HttpPost()]
        public async Task<ActionResult> InsertEntryLead(EntryPostRequest entryPostRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(400, ModelState.ValidationState);
                }

                var success = await _leadRepository.InsertEntry(entryPostRequest);

                if (success)
                {
                    return StatusCode(201);
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

        /// <summary>
        ///     InsertJsonLead
        /// </summary>
        /// <remarks>Inserts a new lead via a json object.</remarks>
        /// <param name="marketingPostRequest" ></param>
        /// <returns></returns >
        [HttpPost()]
        public async Task<ActionResult<List<LeadGetResponse>>> InsertJsonLead([FromBody] MarketingPostRequest marketingPostRequest)
        {
            try
            {
                var success = await _leadRepository.InsertMarketing(marketingPostRequest);

                if (success)
                {
                    List<LeadGetResponse> leadGetResponse = new List<LeadGetResponse>()
                    {
                        new LeadGetResponse { message = "Success"}
                    };
                    return leadGetResponse;
                }
                else
                {
                    return StatusCode(500);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        ///     InsertAssignedLead
        /// </summary>
        /// <remarks>Inserts a new assigned lead via a json object.</remarks>
        /// <param name="assignedLeadPostRequest" ></param>
        /// <returns></returns >
        [HttpPost()]
        public async Task<ActionResult<List<LeadGetResponse>>> InsertAssignedLead(AssignedLeadPostRequest assignedLeadPostRequest)
        {
            try
            {
                var success = await _leadRepository.InsertAssignedLead(assignedLeadPostRequest);

                if (success)
                {
                    List<LeadGetResponse> leadGetResponse = new List<LeadGetResponse>()
                    {
                        new LeadGetResponse { message = "Success"}
                    };
                    return leadGetResponse;
                }
                else
                {
                    return StatusCode(500);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        ///     InsertJsonLeadNoState
        /// </summary>
        /// <remarks>Inserts a new lead via a json object.</remarks>
        /// <param name="marketingCityNoStatePostRequest" ></param>
        /// <returns></returns >
        [HttpPost()]
        public async Task<ActionResult<List<LeadGetResponse>>> InsertJsonLeadNoState([FromBody] MarketingCityNoStatePostRequest marketingCityNoStatePostRequest)
        {
            try
            {
                var success = await _leadRepository.InsertMarketing(marketingCityNoStatePostRequest);

                if (success)
                {
                    List<LeadGetResponse> leadGetResponse = new List<LeadGetResponse>()
                    {
                        new LeadGetResponse { message = "Success"}
                    };
                    return leadGetResponse;
                }
                else
                {
                    return StatusCode(500);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        ///     InsertJsonLeadCity
        /// </summary>
        /// <remarks>Inserts a new lead via a json object.</remarks>
        /// <param name="marketingWithCityPostRequest" ></param>
        /// <returns></returns >
        [HttpPost()]
        public async Task<ActionResult<List<LeadGetResponse>>> InsertJsonLeadCity([FromForm] MarketingWithCityPostRequest marketingWithCityPostRequest)
        {
            try
            {
                var success = await _leadRepository.InsertMarketing(marketingWithCityPostRequest);

                if (success)
                {
                    List<LeadGetResponse> leadGetResponse = new List<LeadGetResponse>()
                    {
                        new LeadGetResponse { message = "Success"}
                    };
                    return leadGetResponse;
                }
                else
                {
                    return StatusCode(500);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        ///     InsertFormLead
        /// </summary>
        /// <remarks>Inserts a new lead via a form object.</remarks>
        /// <param name="marketingPostRequest" ></param>
        /// <returns></returns >
        [HttpPost()]
        public async Task<ActionResult> InsertFormLead([FromForm] MarketingPostRequest marketingPostRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(400, "Model Invalid");
                }

                var success = await _leadRepository.InsertMarketing(marketingPostRequest);

                if (success)
                {
                    return StatusCode(201);
                }
                else
                {
                    return StatusCode(500);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        ///     GetLeadsByLoadDateAndCountry
        /// </summary>
        /// <remarks>Returns a list of leads greater than load date and within country.</remarks>
        /// <param name="loadDate"></param>
        /// <param name="country"></param>
        /// <returns></returns >
        [HttpGet("{loadDate}/{country}")]
        public async Task<ActionResult<List<LeadsToMatchGetResponse>>> GetLeadsByLoadDateAndCountry(DateTime loadDate, string country)
        {
            try
            {
                ICollection<LeadsToMatch> leads = await _leadRepository.GetLeadsByLoadDate(loadDate, country);

                if (leads == null)
                {
                    return StatusCode(500, "An unknown exception has occurred in the repository.");
                }

                List<LeadsToMatchGetResponse> leadsToMatchGetResponse = _mapper.Map<List<LeadsToMatchGetResponse>>(leads);

                return leadsToMatchGetResponse;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An unknown exception has occurred in the controller.");
            }
        }

        /// <summary>
        ///     UpdateLeadWithOrder
        /// </summary>
        /// <remarks>Updates an existing lead with a matching order from Agresso.</remarks>
        /// <param name="matchFromAgresso"></param>
        /// <returns></returns >
        [HttpPost()]
        public async Task<ActionResult> UpdateLeadWithOrder(LeadMatchFromAgressoPostRequest matchFromAgresso)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(400, ModelState.ValidationState);
                }

                var success = await _leadRepository.UpdateLeadWithOrder(matchFromAgresso);

                if (success)
                {
                    return StatusCode(204);
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
        #endregion

        #region Webhook Endpoints
        /// <summary>
        ///     InsertFacebookData
        /// </summary>
        /// <remarks>Verifies Facebook webhook setup using verify token.</remarks>
        /// <returns></returns >
        [HttpGet()]
        [ActionName("InsertFacebookData")]
        public ActionResult AuthenticateFacebookWebhook([FromQuery(Name = "hub.mode")] string mode, [FromQuery(Name = "hub.challenge")] string challenge, [FromQuery(Name = "hub.verify_token")] string verifyToken)
        {
            try
            {
                string userSecret = _configuration.GetValue<string>("LeadsFacebookSecret");

                if (userSecret == verifyToken)
                {
                    return StatusCode(200, challenge);
                }
                else
                {
                    return StatusCode(500, "Invalid verify token.");
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An unknown exception has occurred in the controller.");
            }
        }

        /// <summary>
        ///     InsertFacebookData
        /// </summary>
        /// <remarks>Inserts a new Facebook json data object.</remarks>
        /// <param name="facebookPostRequest" ></param>
        /// <returns></returns >
        [HttpPost()]
        public async Task<ActionResult> InsertFacebookData(FacebookPostRequest facebookPostRequest)
        {
            try
            {
                var success = await _leadRepository.ProcessFacebookWebhook(facebookPostRequest);

                if (success)
                {
                    return StatusCode(201);
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

        /// <summary>
        ///     InsertGoogleData
        /// </summary>
        /// <remarks>Inserts a new Google json data object.</remarks>
        /// <param name="GooglePostRequest" ></param>
        /// <returns></returns >
        [HttpPost()]
        public async Task<ActionResult> InsertGoogleData(GooglePostRequest GooglePostRequest)
        {
            try
            {
                var success = await _leadRepository.ProcessGoogleWebhook(GooglePostRequest);

                if (success)
                {
                    return StatusCode(200);
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
        #endregion
    }
}