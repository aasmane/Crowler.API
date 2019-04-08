using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crowler.API.Modele.Indeed
{
    /// <summary>
    /// the indeed Crowling Request
    /// </summary>
    public class IndeedCrowlingRequest
    {
        /// <summary>
        ///Gets or set the where property
        /// </summary>
        public string Where { get; set; }

        /// <summary>
        /// Gets or set the what property
        /// </summary>
        public string What { get; set; }
    }
}
