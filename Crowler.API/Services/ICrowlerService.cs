using Crowler.API.Modele;
using System.Threading.Tasks;

namespace Crowler.API.Services
{
    /// <summary>
    /// the Crower Service Interface
    /// </summary>
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
