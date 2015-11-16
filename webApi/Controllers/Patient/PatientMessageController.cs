using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bll;
using Share;
using Model;

namespace webApi.Controllers.Patient
{
    public class PatientMessageController : BaseApiController
    {
        public IEnumerable<patient_message> GetListForDoctor(int pageindex)
        {
            return null;
        }
    }
}