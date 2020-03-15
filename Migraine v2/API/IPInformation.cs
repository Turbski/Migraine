using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migraine_v2.API
{

    public class IPInformation
    {
        public string ip { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public string region_code { get; set; }
        public string country { get; set; }
        public string country_code { get; set; }
        public string country_code_iso3 { get; set; }
        public string country_capital { get; set; }
        public string country_tld { get; set; }
        public string country_name { get; set; }
        public string continent_code { get; set; }
        public bool in_eu { get; set; }
        public string postal { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public string timezone { get; set; }
        public string utc_offset { get; set; }
        public string country_calling_code { get; set; }
        public string currency { get; set; }
        public string currency_name { get; set; }
        public string languages { get; set; }
        public float country_area { get; set; }
        public float country_population { get; set; }
        public string asn { get; set; }
        public string org { get; set; }
    }

}
