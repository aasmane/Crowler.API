using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Crowler.API.Modele
{
    [DataContract]
    public class CrowlingMessage
    {
        [DataMember]
        public bool OperationSucces { get; set; }

        [DataMember]
        public string ErrorMessage { get; set; }
    }
}
