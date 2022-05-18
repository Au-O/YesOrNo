using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using OfficeOpenXml;

namespace XlsWork
{
    namespace UnitsXls
    {
        public class ChooseXls : MonoBehaviour
        {
            /// <summary>
            /// 配表中属性字段的数量
            /// </summary>
            /// 
            public static int CountOfAttributes = 10;

            public static Dictionary<int, IndividualData> LoadExcelAsDictionary()
            {
                Dictionary<int, IndividualData> ItemDictionary = new Dictionary<int, IndividualData>();//新建词典
                string path = Application.streamingAssetsPath + "/Data/Excels/Choose.xlsx";
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                ExcelPackage excel = new ExcelPackage(fs);
                ExcelWorksheets workSheets = excel.Workbook.Worksheets;//查找到工作簿内各工作表
                ExcelWorksheet workSheet = workSheets[1];//读取第一个工作表

                int colCount = workSheet.Dimension.End.Column;
                int rowCount = workSheet.Dimension.End.Row;

                for (int row = 4; row <= rowCount; row++)//前三行是表头
                {
                    IndividualData item = new IndividualData(CountOfAttributes);
                    for (int col = 1; col <= colCount; col++)
                    {
                        //读取每个单元格中的数据
                        item.Values[col - 1] = workSheet.Cells[row, col].Text;
                    }
                    int itemID = Convert.ToInt32(item.Values[0].ToString());//获取ID
                    ItemDictionary.Add(itemID, item);//将ID和操作单元写入字典
                    //Debug.Log(item.Values[2]);
                }
                //Debug.Log("complete");
                return ItemDictionary;
            }
        }
    }
}
