using api.leads.Data.Models;
using api.leads.Data.Repositories;
using api.leads.DTO.Request;
using api.leads.DTO.Response;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.leads.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class EntryController : ControllerBase
    {
        private readonly IEntryRepository _entryRepository;
        protected readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<EntryController> _logger;

        public EntryController(IEntryRepository entryRepository,
                                    IConfiguration configuration,
                                    IMapper mapper,
                                    ILogger<EntryController> logger)
        {
            _entryRepository = entryRepository;
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        ///     InsertCall
        /// </summary>
        /// <remarks>Inserts a new lead entry call.</remarks>
        /// <param name = "callPostRequest" ></param>
        /// <returns></returns >
        [HttpPost()]
        public async Task<ActionResult<List<CallGetResponse>>> InsertCall(CallPostRequest callPostRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(400, ModelState.ValidationState);
                }

                ICollection<Call> calls = await _entryRepository.InsertCall(callPostRequest);

                if (calls == null)
                {
                    return StatusCode(500, "An unknown exception has occurred in the repository.");
                }

                List<CallGetResponse> callGetResponse = _mapper.Map<List<CallGetResponse>>(calls);
                return callGetResponse;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An unknown exception has occurred in the controller.");
            }
        }

        /// <summary>
        ///     InsertCallDecision
        /// </summary>
        /// <remarks>Inserts a new lead entry call decision.</remarks>
        /// <param name = "callDecisionPostRequest" ></param>
        /// <returns></returns >
        [HttpPost()]
        public async Task<ActionResult> InsertCallDecision(CallDecisionPostRequest callDecisionPostRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(400, ModelState.ValidationState);
                }

                var success = await _entryRepository.InsertCallDecision(callDecisionPostRequest);

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
        ///     InsertPerson
        /// </summary>
        /// <remarks>Inserts a new lead entry person.</remarks>
        /// <param name = "personPostRequest" ></param>
        /// <returns></returns >
        [HttpPost()]
        public async Task<ActionResult<List<PersonGetResponse>>> InsertPerson(PersonPostRequest personPostRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(400, ModelState.ValidationState);
                }

                ICollection<Person> person = await _entryRepository.InsertPerson(personPostRequest);

                if (person == null)
                {
                    return StatusCode(500, "An unknown exception has occurred in the repository.");
                }

                List<PersonGetResponse> personGetResponse = _mapper.Map<List<PersonGetResponse>>(person);
                return personGetResponse;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An unknown exception has occurred in the controller.");
            }
        }
    }
}
