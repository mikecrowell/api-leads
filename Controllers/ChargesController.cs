using api.leads.Data.Models;
using api.leads.Data.Repositories;
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
    public class ChargesController : ControllerBase
    {
        private readonly IChargesRepository _chargesRepository;
        protected readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<ChargesController> _logger;

        public ChargesController(IChargesRepository chargesRepository,
                                    IConfiguration configuration,
                                    IMapper mapper,
                                    ILogger<ChargesController> logger)
        {
            _chargesRepository = chargesRepository;
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        ///     GetCharges
        /// </summary>
        /// <remarks>Returns a list of charges that need transactions submitted.</remarks>
        /// <returns></returns >
        [HttpGet()]
        public async Task<ActionResult<List<ChargesGetResponse>>> GetCharges()
        {
            try
            {
                ICollection<Charges> leadCharges = await _chargesRepository.GetCharges();

                if (leadCharges == null)
                {
                    return StatusCode(500, "An unknown exception has occurred in the repository.");
                }

                List<ChargesGetResponse> leadChargesGetResponse = _mapper.Map<List<ChargesGetResponse>>(leadCharges);
                return leadChargesGetResponse;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An unknown exception has occurred in the controller.");
            }
        }

        /// <summary>
        ///     UpdateCharges
        /// </summary>
        /// <remarks>Returns a list of charges that need transactions submitted.</remarks>
        /// <param name="purchaseId"></param>
        /// <returns></returns >
        [HttpPost("{purchaseId}")]
        public async Task<ActionResult> UpdateCharges(int purchaseId)
        {
            try
            {
                var success = await _chargesRepository.UpdateCharges(purchaseId);

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
    }
}
