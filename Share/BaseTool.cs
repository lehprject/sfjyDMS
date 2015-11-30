using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Data;
using System.Net;
using System.IO;
using System.Reflection;  

namespace Share
{
    public class BaseTool
    {
       
        //系统默认日期值
        public static DateTime MiniDate = Convert.ToDateTime("1900-1-1");

        #region 类型转换函数
        public static Int32 GetIntNumber(object obj, int defaultValue)
        {
            try
            {
                if (obj == null)
                    return defaultValue;
                return Convert.ToInt32(obj);
            }
            catch
            {
                return defaultValue;
            }
        }
        public static Double GetDoubleNumber(object obj, double defaultValue)
        {
            try
            {
                if (obj == null)
                    return defaultValue;
                return Convert.ToDouble(obj);
            }
            catch
            {
                return defaultValue;
            }
        }
        public static Decimal GetDecimalNumber(object obj, decimal defaultValue)
        {
            try
            {
                if (obj == null)
                    return defaultValue;
                return Convert.ToDecimal(obj);
            }
            catch
            {
                return defaultValue;
            }
        }
        public static Int32 GetIntNumber(object obj)
        {
            return GetIntNumber(obj, 0);
        }
        public static Double GetDoubleNumber(object obj)
        {
            return GetDoubleNumber(obj, 0);
        }
        public static Decimal GetDecimalNumber(object obj)
        {
            return GetDecimalNumber(obj, 0);
        }

        public static long GetLongMumber(object obj)
        {
            if (obj == null)
                return 0;

            long l = 0;
            bool result = long.TryParse(obj.ToString(), out l);
            if (result)
                return long.Parse(obj.ToString());
            return 0;
        }

        public static int? GetNullableIntNumber(object obj)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch
            {
                return new Nullable<int>();
            }
        }

        /// <summary>
        /// 报价取整 如果是 5.1 就返回 6 
        /// </summary>
        /// <param name="obj">只能用在正数</param>
        /// <returns></returns>
        public static Decimal Round(object obj)
        {
            decimal temp = GetDecimalNumber(obj, 0);
            decimal result = Math.Round(temp, 0, MidpointRounding.ToEven);
            if (temp > result)
                result += 1;
            return result;
        }
        public static Decimal RoundToTen(object obj)
        {
            decimal temp = GetDecimalNumber(obj, 0);
            decimal result = Math.Round(temp / 10) * 10;
            return result;
        }
        public static DateTime GetDateTime(object obj)
        {
            try
            {
                return Convert.ToDateTime(obj);
            }
            catch
            {
                return Convert.ToDateTime(" ");
            }
        }

        public static DateTime GetDateTime(object obj,DateTime defaultDatetime)
        {
            try
            {
                return Convert.ToDateTime(obj);
            }
            catch
            {
                return defaultDatetime;
            }
        }

        public static DateTime? GetNullableDateTime(object obj)
        {
            try
            {
                return Convert.ToDateTime(obj);
            }
            catch
            {
                return null;
            }
        }

        public static bool IsDateTime(object obj)
        {
            if (obj == null)
                return false;
            DateTime dt;
            return DateTime.TryParse(obj.ToString(), out dt);
        }

        public static DateTime TryDateTimePase(object obj)
        {
            if (obj == null)
                return new DateTime(1900, 1, 1);
            if (IsDateTime(obj))
            {
                DateTime dt = DateTime.Parse(obj.ToString());
                if (dt.Year <= 1900)
                    return new DateTime(1900, 1, 1);
                return dt;
            }
            return new DateTime(1900, 1, 1);
        }

        public static bool IsNumber(object obj)
        {
            if (obj == null)
                return false;

            int i = 0;
            return int.TryParse(obj.ToString(), out i);
        }
        public static int TryNumber(object obj)
        {
            if (IsNumber(obj))
                return Convert.ToInt32(obj);
            return 0;

        }

        public static T ConvertObject<T>(Object obj)
        {
            return (T)obj;
        }

        /// <summary>  
        /// DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time"> DateTime时间格式</param>  
        /// <returns>Unix时间戳格式</returns>  
        public static int GetTimestamp(DateTime time)
        {
            try
            {
                DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1,0,0,0));
                return (int)(time - startTime).TotalSeconds;
            }
            catch 
            {
                return 0;
            }
        }

        public static int GetTimestamp(string timeStr)
        {
            try
            {
                DateTime time = BaseTool.GetDateTime(timeStr, DateTime.Today);
                System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0));
                return (int)(time - startTime).TotalSeconds;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Unix时间戳格式转换为DateTime时间格式 
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeByTimestamp(int timestamp)
        {
            try
            {
                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                dtDateTime = dtDateTime.AddSeconds(timestamp).ToLocalTime();
                return dtDateTime;
            }
            catch
            {
                return new DateTime(1970, 1, 1, 0, 0, 0, 0);
            }
        }

        /// <summary>
        /// 输入两个时间，返回其时间差。并用文字描述
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="interval">TimeSpan的常数字段,如TimeSpan.TicksPerMinute</param>
        /// <returns></returns>
        public static string GetDateTimeDiffDescription(DateTime startTime, DateTime endTime, long interval)
        {
            try
            {
                if (interval == TimeSpan.TicksPerMinute)
                { 
                    double min = (endTime - startTime).TotalMinutes;
                    if (min == 0)
                        return string.Empty;
                    int hours = 0;
                    int day = 0;
                    if (min < 60)
                    {
                        return min + "分钟";
                    }
                    else if (min >= 60 && min < 1440)
                    {
                        hours = (int)Math.Floor((decimal)min / 60);
                        min = (int)Math.Floor(min - hours * 60);
                        return string.Format("{0}小时{1}分钟", hours, min);
                    }
                    else
                    {
                        day = (int)Math.Floor((decimal)min / 1440);
                        min = min - day * 1440;
                        hours = (int)Math.Floor((decimal)min / 60);
                        min = (int)Math.Floor( min - hours * 60 );
                        return string.Format("{0}天{1}小时{2}分钟", day, hours, min); 
                    }

                }

                return string.Empty;

            }
            catch 
            {
                return string.Empty;
            }

        }

        #region Date Diff
        public enum DateInterval
        {
            Day,
            DayOfYear,
            Hour,
            Minute,
            Month,
            Quarter,
            Second,
            Weekday,
            WeekOfYear,
            Year
        }


        public static long DateDiff(DateInterval interval, DateTime dt1, DateTime dt2)
        {
            return DateDiff(interval, dt1, dt2, System.Globalization.DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);
        }

        private static int GetQuarter(int nMonth)
        {
            if (nMonth <= 3)
                return 1;
            if (nMonth <= 6)
                return 2;
            if (nMonth <= 9)
                return 3;
            return 4;
        }

        public static long DateDiff(DateInterval interval, DateTime dt1, DateTime dt2, DayOfWeek eFirstDayOfWeek)
        {
            if (interval == DateInterval.Year)
                return dt2.Year - dt1.Year;

            if (interval == DateInterval.Month)
                return (dt2.Month - dt1.Month) + (12 * (dt2.Year - dt1.Year));

            TimeSpan ts = dt2 - dt1;

            if (interval == DateInterval.Day || interval == DateInterval.DayOfYear)
                return Round(ts.TotalDays);

            if (interval == DateInterval.Hour)
                return Round(ts.TotalHours);

            if (interval == DateInterval.Minute)
                return Round(ts.TotalMinutes);

            if (interval == DateInterval.Second)
                return Round(ts.TotalSeconds);

            if (interval == DateInterval.Weekday)
            {
                return Round(ts.TotalDays / 7.0);
            }

            if (interval == DateInterval.WeekOfYear)
            {
                while (dt2.DayOfWeek != eFirstDayOfWeek)
                    dt2 = dt2.AddDays(-1);
                while (dt1.DayOfWeek != eFirstDayOfWeek)
                    dt1 = dt1.AddDays(-1);
                ts = dt2 - dt1;
                return Round(ts.TotalDays / 7.0);
            }

            if (interval == DateInterval.Quarter)
            {
                double d1Quarter = GetQuarter(dt1.Month);
                double d2Quarter = GetQuarter(dt2.Month);
                double d1 = d2Quarter - d1Quarter;
                double d2 = (4 * (dt2.Year - dt1.Year));
                return Round(d1 + d2);
            }

            return 0;

        }

        private static long Round(double dVal)
        {
            if (dVal >= 0)
                return (long)Math.Floor(dVal);
            return (long)Math.Ceiling(dVal);
        }
        #endregion

        #region 十六进制转换
        public static string BytesToHexString(byte[] input)
        {
            try
            {
                StringBuilder hexString = new StringBuilder(64);

                for (int i = 0; i < input.Length; i++)
                {
                    hexString.Append(String.Format("{0:X2}", input[i]));
                }
                return hexString.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static byte[] HexStringToBytes(string hex)
        {
            if (hex.Length == 0)
            {
                return new byte[] { 0 };
            }

            if (hex.Length % 2 == 1)
            {
                hex = "0" + hex;
            }

            byte[] result = new byte[hex.Length / 2];

            for (int i = 0; i < hex.Length / 2; i++)
            {
                result[i] = byte.Parse(hex.Substring(2 * i, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            }

            return result;
        }

        #endregion

        #endregion

        #region 异常处理函数
        public static String FormatExceptionMessage(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            while (ex != null)
            {
                if (i > 0)
                {
                    string em = string.Empty;
                    em = em.PadLeft((i - 1) * 3, '　') + "├──";
                    sb.Append(em);
                }
                sb.AppendLine("【" + ex.GetType().ToString() + "】【" + ex.Message + "】");
                ex = ex.InnerException;
                i++;
            }
            return sb.ToString();
        }
        #endregion

        #region 验证函数，手机，邮件等
        public static bool IsEmail(string email)
        {
            return Regex.IsMatch(email, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        }
        #endregion

        #region DES加密函数
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        /// <summary> 
        /// DES加密字符串 
        /// </summary> 
        /// <param name="encryptString">待加密的字符串</param> 
        /// <param name="encryptKey">加密密钥,要求为8位</param> 
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns> 
        public static string EncryptDES(string encryptString, string encryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        /// <summary> 
        /// DES解密字符串 
        /// </summary> 
        /// <param name="decryptString">待解密的字符串</param> 
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param> 
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns> 
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }
        #endregion

        #region RSA加密函数

        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="publickey"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RSAEncrypt(string publickey, string content)
        { 
           
            try
            {
                //未尝试
                //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                //byte[] pubKeyBytes = Convert.FromBase64String(publickey);
                //rsa.ImportCspBlob(pubKeyBytes);
                //byte[] resultBytes = rsa.Encrypt(Encoding.UTF8.GetBytes(content), false);
                //String result = Convert.ToBase64String(resultBytes);
                //return result;
                return null;
            }
            catch  
            { 
                return null;
            }
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="privatekey"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RSADecrypt(RSACryptoServiceProvider rsa, string content)
        { 
            try
            {
                byte[] data = BaseTool.HexStringToBytes(content);
                byte[] decryptData = rsa.Decrypt(data, false);
                System.Text.UTF8Encoding enc = new UTF8Encoding();
                Encoding GB2312 = Encoding.GetEncoding("GB2312");
                return GB2312.GetString( Convert.FromBase64String( enc.GetString(decryptData) ) );
                 
            }
            catch
            {
                return null;
            }
             
        }

        #endregion

        #region 合并数据
        /// <summary>
        /// 合并数据
        /// </summary>
        /// <param name="tblSource"></param>
        /// <param name="difFields"></param>
        /// <param name="keyField"></param>
        /// <returns></returns>
        public static DataTable CombinData(DataTable tblSource, string[] difFields, string keyField)
        {
            DataTable tblResult = new DataTable();
            for (int i = 0; i < tblSource.Columns.Count; i++)
            {
                DataColumn col = tblSource.Columns[i];
                tblResult.Columns.Add(col.ColumnName, col.DataType);
            }
            tblResult.Columns[keyField].DataType = typeof(String);
            if (tblSource.Rows.Count == 0)
                return tblResult;
            DataRow pRow = tblSource.Rows[0];
            DataRow row = null;
            object sId = pRow[keyField];
            object eId = pRow[keyField];
            bool bStop = true;
            for (int i = 1; i < tblSource.Rows.Count; i++)
            {
                row = tblSource.Rows[i];
                bStop = true;
                if (Convert.ToInt32(row[keyField]) == Convert.ToInt32(pRow[keyField]) + 1)
                {
                    bool equal = true;
                    foreach (string fn in difFields)
                    {
                        if (row[fn].ToString().Trim() != pRow[fn].ToString().Trim())
                        {
                            equal = false;
                            break;
                        }
                    }
                    if (equal)
                    {
                        eId = row[keyField];
                        bStop = false;
                    }
                }
                if (bStop)
                {
                    DataRow newRow = tblResult.NewRow();
                    newRow.ItemArray = pRow.ItemArray;
                    newRow[keyField] = sId + " - " + eId;
                    sId = eId = row[keyField];
                    tblResult.Rows.Add(newRow);
                }
                pRow = row;
            }
            if (!bStop)
            {
                DataRow newRow = tblResult.NewRow();
                newRow.ItemArray = pRow.ItemArray;
                newRow[keyField] = sId + " - " + eId;
                tblResult.Rows.Add(newRow);
            }
            return tblResult;
        }

        public delegate bool ChkSequenceDelegate(DataRow prerow, DataRow therow);
        public delegate void ReadFieldDelegate(ref DataRow newrow, DataRow therow, object k1, object k2);
        /// <summary>
        /// 合并数据
        /// </summary>
        /// <param name="tblSource"></param>
        /// <param name="difFields"></param>
        /// <param name="keyField"></param>
        /// <returns></returns>
        public static DataTable CombinData(DataView dvSource, string[] difFields, string keyField, ChkSequenceDelegate ChkSequence, ReadFieldDelegate ReadField)
        {
            DataTable tblTemp = dvSource.Table.Clone();
            tblTemp.Columns[keyField].DataType = typeof(String);
            if (dvSource.Count == 0)
                return tblTemp;
            DataRow pRow = dvSource[0].Row;
            DataRow row = null;
            object sId = pRow[keyField];
            object eId = pRow[keyField];
            bool bStop = true;
            for (int i = 1; i < dvSource.Count; i++)
            {
                row = dvSource[i].Row;
                bStop = true;
                if (ChkSequence(pRow, row))
                {
                    bool equal = true;
                    foreach (string fn in difFields)
                    {
                        if (row[fn].ToString().Trim() != pRow[fn].ToString().Trim())
                        {
                            equal = false;
                            break;
                        }
                    }
                    if (equal)
                    {
                        eId = row[keyField];
                        bStop = false;
                    }
                }
                if (bStop)
                {
                    DataRow newRow = tblTemp.NewRow();
                    ReadField(ref newRow, pRow, sId, eId);
                    sId = eId = row[keyField];
                    tblTemp.Rows.Add(newRow);
                }
                pRow = row;
            }
            //   if ((dvSource.Count==1))
            {
                DataRow newRow = tblTemp.NewRow();
                ReadField(ref newRow, pRow, sId, eId);
                tblTemp.Rows.Add(newRow);
            }
            return tblTemp;
        }

        #endregion


        #region 根据经纬度计算距离
        static double DEF_PI = 3.14159265359; // PI
        static double DEF_2PI = 6.28318530712; // 2*PI
        static double DEF_PI180 = 0.01745329252; // PI/180.0
        static double DEF_R = 6370693.5; // radius of earth

        //最短距离
        public double GetShortDistance(double lon1, double lat1, double lon2, double lat2)
        {
            double ew1, ns1, ew2, ns2;
            double dx, dy, dew;
            double distance;
            // 角度转换为弧度
            ew1 = lon1 * DEF_PI180;
            ns1 = lat1 * DEF_PI180;
            ew2 = lon2 * DEF_PI180;
            ns2 = lat2 * DEF_PI180;
            // 经度差
            dew = ew1 - ew2;
            // 若跨东经和西经180 度，进行调整
            if (dew > DEF_PI)
                dew = DEF_2PI - dew;
            else if (dew < -DEF_PI)
                dew = DEF_2PI + dew;
            dx = DEF_R * Math.Cos(ns1) * dew; // 东西方向长度(在纬度圈上的投影长度)
            dy = DEF_R * (ns1 - ns2); // 南北方向长度(在经度圈上的投影长度)
            // 勾股定理求斜边长
            distance = Math.Sqrt(dx * dx + dy * dy);
            return distance;
        }
        //最长距离
        public double GetLongDistance(double lon1, double lat1, double lon2, double lat2)
        {
            double ew1, ns1, ew2, ns2;
            double distance;
            // 角度转换为弧度
            ew1 = lon1 * DEF_PI180;
            ns1 = lat1 * DEF_PI180;
            ew2 = lon2 * DEF_PI180;
            ns2 = lat2 * DEF_PI180;
            // 求大圆劣弧与球心所夹的角(弧度)
            distance = Math.Sin(ns1) * Math.Sin(ns2) + Math.Cos(ns1) * Math.Cos(ns2) * Math.Cos(ew1 - ew2);
            // 调整到[-1..1]范围内，避免溢出
            if (distance > 1.0)
                distance = 1.0;
            else if (distance < -1.0)
                distance = -1.0;
            // 求大圆劣弧长度
            distance = DEF_R * Math.Acos(distance);
            return distance;
        }
       
        #endregion


        #region 反射
        /// <summary>
        /// 返回属性的名称
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static List<string> GetPropertyNames<TModel>(System.Linq.Expressions.Expression<Func<TModel,object>> selector)
        {
            List<string> resultList = new List<string>();
            try
            {
                string propertyName = GetPropertyName <TModel>(selector);
                if(!string.IsNullOrEmpty(propertyName))
                {
                    //单个属性
                    resultList.Add(propertyName); 
                }
                else
                {
                    //多个属性
                    System.Linq.Expressions.NewExpression nbody = (System.Linq.Expressions.NewExpression)selector.Body;
                    resultList = nbody.Members.Select(t => t.Name).ToList();
                }

                return resultList;
            }
            catch(Exception ex)
            {
                return resultList;
            }
        }

        public static string GetPropertyName<TModel>(System.Linq.Expressions.Expression<Func<TModel, object>> selector)
        {
            try
            {
                string propertyName = string.Empty;
                //try 1
                System.Linq.Expressions.MemberExpression body = selector.Body as System.Linq.Expressions.MemberExpression;

                if (body == null)
                {
                    //try 2 
                    System.Linq.Expressions.UnaryExpression ubody = (System.Linq.Expressions.UnaryExpression)selector.Body;
                    body = ubody.Operand as System.Linq.Expressions.MemberExpression;
                }

                if (body != null)
                {
                    propertyName = body.Member.Name; 
                }

                return propertyName;
            }
            catch(Exception ex)
            {
                return string.Empty;
            }

        }
        #endregion

        #region DataTable分页
        public static DataTable GetPagedTable(DataTable dt, int PageIndex, int PageSize)
        {

            if (PageIndex == 0)
                return dt;
            DataTable newdt = dt.Clone();
            //newdt.Clear();
            int rowbegin = (PageIndex - 1) * PageSize;
            int rowend = PageIndex * PageSize;

            if (rowbegin >= dt.Rows.Count)
                return newdt;

            if (rowend > dt.Rows.Count)
                rowend = dt.Rows.Count;
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                DataRow newdr = newdt.NewRow();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    newdr[column.ColumnName] = dr[column.ColumnName];
                }
                newdt.Rows.Add(newdr);
            }

            return newdt;
        }

        #endregion

        #region list转换DataTable

        /// <summary>
        /// 将泛类型集合List类转换成DataTable
        /// </summary>
        /// <param name="list">泛类型集合</param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(List<T> entitys)
        {
            //检查实体集合不能为空
            if (entitys == null || entitys.Count < 1)
            {
                throw new Exception("需转换的集合为空");
            }
            //取出第一个实体的所有Propertie
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();

            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
            DataTable dt = new DataTable();
            for (int i = 0; i < entityProperties.Length; i++)
            {
                //dt.Columns.Add(entityProperties[i].Name, entityProperties[i].PropertyType);
                dt.Columns.Add(entityProperties[i].Name);
            }
            //将所有entity添加到DataTable中
            foreach (object entity in entitys)
            {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);
                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }

        #endregion

        #region 随机

        public static string Random(int length)
        {
            string NumberStr = "";
            var random = new Random((int)DateTime.Now.Ticks);
            const string textArray = "0123456789";

            //生成验证码
            for (var i = 0; i < length; i++)
            {
                NumberStr = NumberStr + textArray.Substring(random.Next() % textArray.Length, 1);
            }
            return NumberStr;
        }

        public static string getRandomNum(string[] arrNum, string tmp, int length)
        {
            int n = 0;
            while (n <= arrNum.Length - 1)
            {
                if (arrNum[n] == tmp) //利用循环判断是否有重复
                {
                    tmp = Random(length); //重新随机获取。
                    getRandomNum(arrNum, tmp, length); //递归:如果取出来的数字和已取得的数字有重复就重新随机获取。
                }
                n++;
            }
            return tmp;
        }

        #endregion

    }


    /// <summary>
    /// 枚举操作
    /// </summary>
    public class EnumOperate
    {
        #region Enum 转为 DataTable
        public static string Text = "text";
        public static string Value = "value";

        public static DataTable ListEnum(Type enumType)
        {
            DataTable table = new DataTable();
            table.Columns.Add(Text);
            table.Columns.Add(Value);
            foreach (Enum enum2 in Enum.GetValues(enumType))
            {
                DataRow row = table.NewRow();
                row[Text] = enum2.ToString();
                row[Value] = (int)Enum.Parse(enumType, enum2.ToString());
                table.Rows.Add(row);
            }
            return table;
        }

        public static string GetText(Type typeInfo, int value)
        {
            try
            {
                string text = Enum.GetName(typeInfo, value);
                if (text == null)
                    return string.Empty;
                else
                    return text;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static int GetValue(Type typeInfo, string text)
        {
            try
            {
                var value = Enum.Parse(typeInfo, text);
                if (value != null)
                    return (int)value;
                else
                    return int.MinValue;
            }
            catch
            {
                return int.MinValue;
            }
        }

        public static void InsertEnumValue(ref DataTable dt, string text, string value, int position)
        {
            var defaultRow = dt.NewRow();
            defaultRow[EnumOperate.Text] = text;
            defaultRow[EnumOperate.Value] = value;
            dt.Rows.InsertAt(defaultRow, 0);
        }

        #endregion

        #region Enum 转为 KeyValue

        public static List<KeyValuePair<int,string>> ToKeyValues(Type typeInfo)
        {
            return ToKeyValues<int>(typeInfo);
        }

        public static List<KeyValuePair<int, string>> ToKeyValues(Type typeInfo,int defaultValue,string defaultText)
        {
            return ToKeyValues<int>(typeInfo, defaultValue,defaultText);
        }

        public static List<KeyValuePair<K,string>> ToKeyValues<K>(Type typeInfo)
        {
            List<KeyValuePair<K, string>> resultList = new List<KeyValuePair<K, string>>();
            foreach (var enumtype in Enum.GetValues(typeInfo))
            {
                K value = (K)Enum.Parse(typeInfo, enumtype.ToString());
                string text = enumtype.ToString();
                resultList.Add(new KeyValuePair<K, string>(value, text));
            }
            return resultList;
        }

        public static List<KeyValuePair<K, string>> ToKeyValues<K>(Type typeInfo,K defaultValue,string defaultText)
        {
            var resultList = ToKeyValues<K>( typeInfo);
            resultList.Insert(0,new KeyValuePair<K, string>(defaultValue, defaultText));
            return resultList;
        }

        #endregion

    }
}
