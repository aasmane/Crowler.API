using Crowler.API.Modele;
using Crowler.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Crowler.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrowlingController : ControllerBase
    {
        private ICrowlerService _crowlerService;
        public CrowlingController(ICrowlerService crowlerService)
        {
            _crowlerService = crowlerService;
        }

        // GET: api/Default
        /// <summary>
        /// the get appi
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        [ApiConventionMethod(typeof(DefaultApiConventions),nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<CrowlingMessage>> Launch(CrowlingRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }
             await _crowlerService.LaunchCrowling(new Modele.CrowlingRequest()
            {
                CrowlingProviderType = Modele.CrowlingProviderTypeEnum.Indeed,
                IndeedRequest = new Modele.Indeed.IndeedCrowlingRequest { What = ".net", Where = "Bordeaux" }

            });
            return new CrowlingMessage();
        }

    }
}