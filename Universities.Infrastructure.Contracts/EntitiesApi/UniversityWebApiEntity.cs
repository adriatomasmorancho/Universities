using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Universities.Infrastructure.Contracts.Entities
{
    public class UniversityWebApiEntity
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("alpha_two_code")]
        public string? Code { get; set; }

        [JsonPropertyName("domains")]
        public List<string>? DomainList { get; set; }

        [JsonPropertyName("stateprovince")]
        public string? StateProvince { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("web_pages")]
        public List<string>? WebPageList { get; set; }
    }
}
