using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 此类专用来返回远程调用操作的结果
    /// </summary>
    public class ResponseMessage
    {
        int _PKId;
        bool _bSuccess;
        int _Status;
        string _Message = "";
        string _Extend1 = "";
        string _Extend2 = "";
        string _Extend3 = "";

        [global::System.Runtime.Serialization.DataMember]
        public int PKId
        {
            get
            {
                return _PKId;
            }
            set
            {
                this._PKId = value;
            }
        }

        [global::System.Runtime.Serialization.DataMember]
        public bool bSuccess
        {
            get
            {
                return _bSuccess;
            }
            set
            {
                this._bSuccess = value;
            }
        }

        [global::System.Runtime.Serialization.DataMember]
        public int Status
        {
            get
            {
                return _Status;
            }
            set
            {
                this._Status = value;
            }
        }

        [global::System.Runtime.Serialization.DataMember]
        public string Message
        {
            get
            {
                return _Message;
            }
            set
            {
                this._Message = value;
            }
        }

        [global::System.Runtime.Serialization.DataMember]
        public string Extend1
        {
            get
            {
                return _Extend1;
            }
            set
            {
                this._Extend1 = value;
            }
        }

        [global::System.Runtime.Serialization.DataMember]
        public string Extend2
        {
            get
            {
                return _Extend2;
            }
            set
            {
                this._Extend2 = value;
            }
        }
        [global::System.Runtime.Serialization.DataMember]
        public string Extend3
        {
            get
            {
                return _Extend3;
            }
            set
            {
                this._Extend3 = value;
            }
        }
    }

    /// <summary>
    /// 异步调用操作后，返回给客户端的结果
    /// </summary>
    public class UpdateResponse
    {
        public bool bSuccess { get; set; }

        public string Error { get; set; }

        public List<GuidPKIDPair> PairList { get; set; }

        public Dictionary<string, object> ObjectDic { get; set; }

        public object Info { get; set; }

        public List<object> Items { get; set; }
    }


    /// <summary>
    /// Guid 客户端为对象生成的唯一键
    /// PKID 对应对象在服务端的主键
    /// </summary>
    public class GuidPKIDPair
    {
        public string Guid { get; set; }
        public string PKID { get; set; }
    }


    /// <summary>
    /// 异步分页
    /// </summary>
    public class SearchResponse
    {
        public bool bSuccess { get; set; }

        public string Message { get; set; }

        public object ResultList { get; set; }
    }
}
