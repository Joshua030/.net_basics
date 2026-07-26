using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Schools
{
    public class CreateSchoolRequest
    {
        public string Name { get; set; }
        public DateTime EstablishedDate { get; set; }
    }
}
