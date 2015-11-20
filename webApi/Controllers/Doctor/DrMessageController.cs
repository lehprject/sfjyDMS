using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Bll;
using Model;
using Share;

namespace webApi.Controllers.Doctor
{
    public class DrMessageController : BaseApiController
    {
        // POST api/<controller>
        public ResponseMessage Post([FromBody]dr_message info)
        {
            dr_message_Bll messageBll = new dr_message_Bll();
            string error = string.Empty;

            dr_message message = new dr_message();
            message.Initial();
            message.drid = DoctorID;
            message.msg_content = info.msg_content;
            message.attach_file1 = info.attach_file1;
            message.attach_file2 = info.attach_file2;
            message.attach_file3 = info.attach_file3;
            messageBll.CreateDrMessage(message,out error);

            Model.ResponseMessage result = new ResponseMessage();
            if (!string.IsNullOrEmpty(error))
            {
                result.bSuccess = true;
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