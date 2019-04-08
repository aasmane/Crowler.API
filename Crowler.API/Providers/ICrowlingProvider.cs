using Crowler.API.Modele;
using Crowler.API.Modele.Indeed;
using System.Threading.Tasks;

namespace Crowler.API.Providers
{
    /// <summary>
    /// The Crowling Interface
    /// </summary>
    public interface ICrowlingProvider
    {
        Task<CrowlingMessage> LaunchCrowling(IndeedCrowlingRequest request);
    }
}
