using Crowler.API.Modele;
using Crowler.API.Providers;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Crowler.API.Services
{
    /// <summary>
    /// Crowling Class Services
    /// </summary>
    public class CrowlerService : ICrowlerService
    {
         
        private ICrowlingProvider _crowlingProvider;
        private IConfiguration _configuration;

        public CrowlerService(IConfiguration configuration, ICrowlingProvider crowlingProvider)
        {
            _configuration = configuration;
            _crowlingProvider = crowlingProvider;
        }

        /// <summary>
        /// Launch Crowling
        /// </summary>
        /// <param name="request">the request</param>
        /// <returns>the Message represent result of crowling</returns>
        public async Task<CrowlingMessage> LaunchCrowling(CrowlingRequest request)
        {
            var provider = this.GetProvider(request);
            if (provider == null)
            {
                return null;
            }
            return await provider.LaunchCrowling(request.IndeedRequest);
        }

        #region Private Methode


        /// <summary>
        /// Get Provider Type
        /// </summary>
        /// <param name="request">The request</param>
        /// <returns>Instance of desired provider</returns>
        private ICrowlingProvider GetProvider(CrowlingRequest request)
        {
            if (request == null)
            {
                return null;
            }

            switch (request.CrowlingProviderType)
            {
                case CrowlingProviderTypeEnum.Indeed:
                    return _crowlingProvider;
                default:
                    break;
            }

            return null;
        }
        #endregion
    }
}
