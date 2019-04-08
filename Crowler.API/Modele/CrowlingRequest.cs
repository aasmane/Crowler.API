using Crowler.API.Modele.Indeed;

namespace Crowler.API.Modele
{
    /// <summary>
    /// The  CrowlingRequest
    /// </summary>
    public class CrowlingRequest
    {
        /// <summary>
        ///  Get or set the Crowling Provider Type
        /// </summary>
        public CrowlingProviderTypeEnum CrowlingProviderType { get; set; }

        /// <summary>
        ///  Get or set the Indeed Crowling Provider Type
        /// </summary>
        public IndeedCrowlingRequest IndeedRequest { get; set; }
    }
}
