using System.Collections.Generic;
using System.Threading.Tasks;
using api.leads.Data.Models;
using api.leads.Data.Repositories;
using api.leads.DTO.Request;
using api.leads.DTO.Response;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace api.leads.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class FormsController : ControllerBase
    {
        private readonly IFormsRepository _formsRepository;
        protected readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<FormsController> _logger;

        public FormsController(IFormsRepository formsRepository,
                                    IConfiguration configuration,
                                    IMapper mapper,
                                    ILogger<FormsController> logger)
        {
            _formsRepository = formsRepository;
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        ///     InsertForm
        /// </summary>
        /// <remarks>Inserts a new form.</remarks>
        /// <param name="formPostRequest"></param>
        /// <returns></returns >
        [HttpPost()]
        public async Task<ActionResult> InsertForm(FormCreatePostRequest formPostRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(400, ModelState.ValidationState);
                }

                await _formsRepository.InsertForm(formPostRequest);
                return StatusCode(200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                if (ex.Message.Contains("THIRD PARTY ID ALREADY IN USE"))
                    return StatusCode(400, ex.Message);
                else
                    return StatusCode(500, "An unknown exception has occurred in the controller.");
            }
        }

        /// <summary>
        ///     UpdateForm
        /// </summary>
        /// <remarks>Updates an existing form.</remarks>
        /// <param name="formPostRequest"></param>
        /// <returns></returns >
        [HttpPost()]
        public async Task<ActionResult> UpdateForm(FormUpdatePostRequest formPostRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(400, ModelState.ValidationState);
                }
                
                await _formsRepository.UpdateForm(formPostRequest);
                return StatusCode(200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                if (ex.Message.Contains("THIRD PARTY ID ALREADY IN USE"))
                    return StatusCode(400, ex.Message);
                else
                    return StatusCode(500, "An unknown exception has occurred in the controller.");
            }
        }

        /// <summary>
        ///     DeleteForm
        /// </summary>
        /// <remarks>Deletes a form.</remarks>
        /// <param name="formId"></param>
        /// <returns></returns >
        [HttpPost("{formId}")]
        public async Task<ActionResult> DeleteForm(int formId)
        {
            try
            {
                await _formsRepository.DeleteForm(formId);
                return StatusCode(200);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                if (ex.Message.Contains("NO VALID FORM EXISTS TO DELETE"))
                    return StatusCode(400, ex.Message);
                else
                    return StatusCode(500, "An unknown exception has occurred in the controller.");
            }
        }

        /// <summary>
        ///     GetForms
        /// </summary>
        /// <remarks>Returns a list of all the lead forms.</remarks>
        /// <param name="client"></param>
        /// <returns></returns >
        [HttpGet("{client}")]
        public async Task<ActionResult<List<FormsGetResponse>>> GetForms(string client)
        {
            try
            {
                ICollection<Forms> forms = await _formsRepository.GetForms(client);

                if (forms == null)
                {
                    return StatusCode(500, "An unknown exception has occurred in the repository.");
                }

                List<FormsGetResponse> formsGetResponse = _mapper.Map<List<FormsGetResponse>>(forms);
                return formsGetResponse;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                 return StatusCode(500, "An unknown exception has occurred in the controller.");
            }

        }

        /// <summary>
        ///     InsertGroup
        /// </summary>
        /// <remarks>Inserts a new group.</remarks>
        /// <param name="groupPostRequest"></param>
        /// <returns></returns >
        [HttpPost()]
        public async Task<ActionResult> InsertGroup(GroupPostRequest groupPostRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(400, ModelState.ValidationState);
                }

                var success = await _formsRepository.InsertGroup(groupPostRequest);

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

        /// <summary>
        ///     DeleteGroup
        /// </summary>
        /// <remarks>Deletes a lead that was flagged as a duplicate on review.</remarks>
        /// <param name="groupId"></param>
        /// <returns></returns >
        [HttpPost("{groupId}")]
        public async Task<ActionResult> DeleteGroup(int groupId)
        {
            try
            {
                await _formsRepository.DeleteGroup(groupId);
                return StatusCode(200);
            }
            catch (System.Exception ex)
            {
                if (ex.Message.Contains("NO VALID GROUP EXISTS TO DELETE"))
                    return StatusCode(400, ex.Message);
                else
                    return StatusCode(500, "An unknown exception has occurred in the controller.");
            }
        }

        /// <summary>
        ///     GetGroups
        /// </summary>
        /// <remarks>Returns a list of all the lead form groups.</remarks>
        /// <returns></returns >
        [HttpGet("")]
        public async Task<ActionResult<List<GroupsGetResponse>>> GetGroups()
        {
            try
            {
                ICollection<Groups> groups = await _formsRepository.GetGroups();

                if (groups == null)
                {
                    return StatusCode(500, "An unknown exception has occurred in the repository.");
                }

                List<GroupsGetResponse> groupsGetResponse = _mapper.Map<List<GroupsGetResponse>>(groups);
                return groupsGetResponse;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "An unknown exception has occurred in the controller.");
            }

        }
    }
}
