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
        public IEnumerable<patient_message> GetListForDr(int pageindex)
        {
            patient_message_Bll messageBll = new patient_message_Bll();
            string error = string.Empty;
            var resultList = messageBll.SearchMessgaeList(0,DoctorID,0,0,null,null,null,null,null,pageindex,PageSize,out error);
            return resultList;
        }

        public Model.ResponseMessage Post([FromBody] patient_message info)
        {
            patient_message_Bll messageBll = new patient_message_Bll();
            string error = string.Empty;
            info = messageBll.CreateMessage(info, out error);

            Model.ResponseMessage result = new ResponseMessage();

            if (string.IsNullOrEmpty(error))
            {
                result.bSuccess = true;
                result.Extend1 = info.pkid.ToString();
            }
            else
            {
                result.bSuccess = false;
                result.Message = error;
            }

            return result;
        }
    }
}