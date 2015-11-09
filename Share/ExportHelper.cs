using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace Share
{
    public class ExcelExport:IDisposable
    {
        #region public fields
        public FileInfo ExcelFile { get;private set;}
        #endregion

        #region private fields

        #endregion

        #region public method
        public bool Export<T>(List<T> dataList, System.Linq.Expressions.Expression<Func<T, object>> selector,string filename,out string error)
        {
            error = string.Empty;

            try
            {

                Type tInfo = typeof(T);
                var metaType = tInfo.GetCustomAttributes(typeof(MetadataTypeAttribute), true).FirstOrDefault();
                if (metaType == null)
                {
                    return false;
                }
                MetadataTypeAttribute metaInfo = (MetadataTypeAttribute)metaType;

                var propertiesList = metaInfo.MetadataClassType.GetProperties();
                var nameTypeInfo = typeof(System.ComponentModel.DataAnnotations.DisplayAttribute);
                List<string> columnNameList = new List<string>();
                List<string> columnList = new List<string>();
                List<string> columns = BaseTool.GetPropertyNames(selector);
                foreach (var eachColumn in columns)
                {
                    var propertyInfo = propertiesList.FirstOrDefault(t => t.Name == eachColumn);
                    if (propertyInfo == null)
                        continue;

                    var theNameInfo = propertyInfo.GetCustomAttributes(nameTypeInfo, true).FirstOrDefault();
                    if (theNameInfo == null)
                    {
                        columnNameList.Add(eachColumn);
                    }
                    else
                    {
                        columnNameList.Add(((System.ComponentModel.DataAnnotations.DisplayAttribute)theNameInfo).Name);
                    }
                    columnList.Add(eachColumn);
                    
                }
                string columnStr = string.Join(",", columnList);
                string columnNameStr = string.Join(",", columnNameList);

                return Export<T>(dataList, columnStr, columnNameStr, filename, out error);
            }
            catch(Exception ex)
            {
                error += BaseTool.FormatExceptionMessage(ex);
                return false;
            }
        }

        public bool Export<T>(List<T> dataList, System.Linq.Expressions.Expression<Func<T, object>> selector, string columnNameStr, string filename, out string error)
        {
            error = string.Empty;
            try
            {
                List<string> columns = BaseTool.GetPropertyNames(selector);
                string columnStr = string.Join(",", columns);

                return Export<T>(dataList, columnStr, columnNameStr, filename, out error);
            }
            catch(Exception ex)
            {
                error += BaseTool.FormatExceptionMessage(ex);
                return false;
            }
            
        }

        public bool Export<T>(List<T> dataList, string columnStr, string columnNameStr, string filename, out string error)
        {
            error = string.Empty;
            try
            {
                DataTable dt = new DataTable();
                var columnList = columnStr.Split(',');
                var columnNameList = columnNameStr.Split(',');
                foreach (var item in columnNameList)
                {
                    dt.Columns.Add(new DataColumn(item));
                }

                Type tInfo = typeof(T);
                var propertiesInfo = tInfo.GetProperties();
                int columnLength = columnList.Count();
                foreach (var item in dataList)
                {
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < columnLength; i++)
                    {
                        var thePropertInfo = propertiesInfo.FirstOrDefault(t=>t.Name == columnList[i]);
                        dr[i] = thePropertInfo.GetValue(item);
                    }
                    dt.Rows.Add(dr);
                }

                return Export( dt,  filename, out  error);
            }
            catch (Exception ex)
            {
                error += BaseTool.FormatExceptionMessage(ex);
                return false;
            }
        }

        public bool Export(DataTable dt, string filename, out string error)
        {
            error = string.Empty;
            try
            {
                using (FileStream file = new FileStream(filename, FileMode.Create, FileAccess.Write))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook();
                    HSSFSheet sheet = workbook.CreateSheet("sheet1") as HSSFSheet;

                    HSSFRow headerRow = sheet.CreateRow(0) as HSSFRow;
                    int i = 0;
                    foreach (DataColumn header in dt.Columns)
                    {
                        headerRow.CreateCell(i).SetCellValue(header.ColumnName);
                        i++;
                    }

                    i = 1;
                    int colunmnCount = dt.Columns.Count;
                    foreach (DataRow dr in dt.Rows)
                    {
                        HSSFRow eachRow = sheet.CreateRow(i) as HSSFRow;
                        for (int j = 0; j < colunmnCount; j++)
                        {
                            eachRow.CreateCell(j).SetCellValue(dr[j].ToString());
                        }
                        i++;
                    }

                    workbook.Write(file);

                    ExcelFile = new FileInfo(filename);
                }

                return true;
            }
            catch (Exception ex)
            {
                error += BaseTool.FormatExceptionMessage(ex);
                return false;
            }
        }

        #endregion

        #region private method
        public void Dispose()
        {
            if(ExcelFile!=null)
            {
                //delete excel file
                //此类负责导出Excel文件,使用者决定如何处理Excel文件。
                //ExcelFile.Delete();
            }
        }
        #endregion
    }
     
}
