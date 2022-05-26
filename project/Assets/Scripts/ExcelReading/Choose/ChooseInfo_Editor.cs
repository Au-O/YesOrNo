using UnityEditor;
using UnityEngine;
using System;
using XlsWork;
using XlsWork.UnitsXls;
using System.Collections.Generic;
using System.Linq;

public class ChooseInfo_Editor : MonoBehaviour
{
    public void Init()
    {
        Action init;

        ChooseInfo chooseInfo = this.GetComponent<ChooseInfo>();
        var dictionary = ChooseXls.LoadExcelAsDictionary();
        if (!dictionary.ContainsKey(chooseInfo.InitFromID))
        {
            Debug.LogErrorFormat("未能找到指定ID：{0}", chooseInfo.InitFromID);
            return;
        }

        IndividualData item = dictionary[chooseInfo.InitFromID];

        //System.Convert在这里用于实现表格内文本对代码内数据类型的自适应，将Excel单元格中的字符串转换成int或其它类型
        init = (() =>
        {
            chooseInfo.chooseSettings.ID = Convert.ToInt32(item.Values[0]);
            chooseInfo.chooseSettings.Description = Convert.ToString(item.Values[1]);

            stringToList(item.Values[2], chooseInfo.chooseSettings.TrueSide);
            stringToList(item.Values[3], chooseInfo.chooseSettings.FalseSide);

            chooseInfo.chooseSettings.TrueSideDes = Convert.ToString(item.Values[4]);
            chooseInfo.chooseSettings.FalseSideDes = Convert.ToString(item.Values[5]);
            chooseInfo.chooseSettings.Time = Convert.ToInt32(item.Values[6]);

            stringToList(item.Values[7], chooseInfo.chooseSettings.Effect);

            chooseInfo.chooseSettings.Sound = Convert.ToString(item.Values[8]);
            chooseInfo.chooseSettings.BGM = Convert.ToString(item.Values[9]);
        });
        init();
    }

    private void stringToList(string ss, List<int> setting)
    {
        //列表类型转换
        setting.Clear();
        if (ss.Length!=0)
        {
            string str = ss.Substring(1, ss.Length - 2);
            string[] strArray = str.Split(',');
            List<string> list = strArray.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                setting.Add(Convert.ToInt32(list[i]));
            }
        }
        else return;
    }

}
