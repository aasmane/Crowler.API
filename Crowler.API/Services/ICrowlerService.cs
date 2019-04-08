using Crowler.API.Modele;
using System.Threading.Tasks;

namespace Crowler.API.Services
{
    public interface ICrowlerService
    {
        /// <summary>
        /// Launch Crowling
        /// </summary>
        /// <param name="request">The request class</param>
        /// <returns>the result of crowling</returns>
        Task<CrowlingMessage> LaunchCrowling(CrowlingRequest request);
    }
}
