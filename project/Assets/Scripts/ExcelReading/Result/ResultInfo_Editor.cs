using UnityEditor;
using UnityEngine;
using System;
using XlsWork;
using XlsWork.UnitsXls;
using System.Collections.Generic;
using System.Linq;

public class ResultInfo_Editor : MonoBehaviour
{
    public void Init()
    {
        Action init;

        ResultInfo resultInfo = this.GetComponent<ResultInfo>();
        var dictionary = ResultXls.LoadExcelAsDictionary();
        if (!dictionary.ContainsKey(resultInfo.InitFromID))
        {
            Debug.LogErrorFormat("未能找到指定ID：{0}", resultInfo.InitFromID);
            return;
        }

        IndividualData item = dictionary[resultInfo.InitFromID];

        //System.Convert在这里用于实现表格内文本对代码内数据类型的自适应，将Excel单元格中的字符串转换成int或其它类型
        init = (() =>
        {
            resultInfo.resultSettings.ID = Convert.ToInt32(item.Values[0]);

            stringToList(item.Values[1], resultInfo.resultSettings.Include);

            if(item.Values[2].Length!=0)
                resultInfo.resultSettings.IncludeOp = Convert.ToChar(item.Values[2]);

            resultInfo.resultSettings.Event = Convert.ToString(item.Values[3]);

            stringToList(item.Values[4], resultInfo.resultSettings.Effect);

            resultInfo.resultSettings.Sound = Convert.ToString(item.Values[5]);
            resultInfo.resultSettings.BGM = Convert.ToString(item.Values[6]);
            resultInfo.resultSettings.MorilityChange = Convert.ToInt32(item.Values[7]);
            resultInfo.resultSettings.EnergyChange = Convert.ToInt32(item.Values[8]);
        });
        init();
    }

    private void stringToList(string ss, List<int> setting)
    {
        //列表类型转换
        setting.Clear();
        if (ss.Length != 0)
        {
            string str = ss.Substring(1, ss.Length - 2);
            string[] strArray = str.Split(',');
            //Debug.Log(strArray[0]);
            List<string> list = strArray.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                setting.Add(Convert.ToInt32(list[i]));
            }
        }
        else return;
    }

}
