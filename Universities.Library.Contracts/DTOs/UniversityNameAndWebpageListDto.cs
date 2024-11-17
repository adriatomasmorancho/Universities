using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Universities.Library.Contracts.DTOs
{
    public class UniversityNameAndWebpageListDto
    {
        public string? Name { get; set; }
        public List<string>? WebpageList { get; set; }
    }
}
