using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prospects.Cross.Infrastructure.Models.Responses
{
    public class ProspectResponse
    {
        public string id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string telephone { get; set; }
        public string schProspectIdentification { get; set; }
        public string address { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public int statusCd { get; set; }
        public string zoneCode { get; set; }
        public string neighborhoodCode { get; set; }
        public string cityCode { get; set; }
        public string sectionCode { get; set; }
        public int roleId { get; set; }
        public object appointableId { get; set; }
        public object rejectedObservation { get; set; }
        public string observation { get; set; }
        public bool disable { get; set; }
        public bool visited { get; set; }
        public bool callcenter { get; set; }
        public bool acceptSearch { get; set; }
        public string campaignCode { get; set; }
        public object userId { get; set; }
    }
}
