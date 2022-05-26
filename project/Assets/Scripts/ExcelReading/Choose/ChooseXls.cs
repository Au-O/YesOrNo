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
            /// ����������ֶε�����
            /// </summary>
            /// 
            public static int CountOfAttributes = 10;

            public static Dictionary<int, IndividualData> LoadExcelAsDictionary()
            {
                Dictionary<int, IndividualData> ItemDictionary = new Dictionary<int, IndividualData>();//�½��ʵ�
                string path = Application.streamingAssetsPath + "/Data/Excels/Choose.xlsx";
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                ExcelPackage excel = new ExcelPackage(fs);
                ExcelWorksheets workSheets = excel.Workbook.Worksheets;//���ҵ��������ڸ�������
                ExcelWorksheet workSheet = workSheets[1];//��ȡ��һ��������

                int colCount = workSheet.Dimension.End.Column;
                int rowCount = workSheet.Dimension.End.Row;

                for (int row = 4; row <= rowCount; row++)//ǰ�����Ǳ�ͷ
                {
                    IndividualData item = new IndividualData(CountOfAttributes);
                    for (int col = 1; col <= colCount; col++)
                    {
                        //��ȡÿ����Ԫ���е�����
                        item.Values[col - 1] = workSheet.Cells[row, col].Text;
                    }
                    int itemID = Convert.ToInt32(item.Values[0].ToString());//��ȡID
                    ItemDictionary.Add(itemID, item);//��ID�Ͳ�����Ԫд���ֵ�
                    //Debug.Log(item.Values[2]);
                }
                //Debug.Log("complete");
                return ItemDictionary;
            }
        }
    }
}
