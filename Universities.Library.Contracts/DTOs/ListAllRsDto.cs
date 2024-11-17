﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universities.XCutting.Enums;

namespace Universities.Library.Contracts.DTOs
{
    public class ListAllRsDto
    {
        public List<ErrorsEnum>? errors;
        public List<UniversityNameAndCountryDto>? data;
    }
}
