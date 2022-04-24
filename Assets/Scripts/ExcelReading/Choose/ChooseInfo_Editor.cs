using UnityEditor;
using UnityEngine;
using System;
using XlsWork;
using XlsWork.UnitsXls;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(ChooseInfo))]
public class ChooseInfo_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("从配表ID刷新"))//添加按钮和功能——当组件上的按钮被按下时
        {
            ChooseInfo chooseInfo = (ChooseInfo)target;
            Init(chooseInfo);//令组件调用自身的InitSelf方法
        }
    }

    public void Init(ChooseInfo instance)
    {
        Action init;

        var dictionary = ChooseXls.LoadExcelAsDictionary();
        if (!dictionary.ContainsKey(instance.InitFromID))
        {
            Debug.LogErrorFormat("未能找到指定ID：{0}", instance.InitFromID);
            return;
        }

        IndividualData item = dictionary[instance.InitFromID];

        //System.Convert在这里用于实现表格内文本对代码内数据类型的自适应，将Excel单元格中的字符串转换成int或其它类型
        init = (() =>
        {
            instance.chooseSettings.ID = Convert.ToInt32(item.Values[0]);
            instance.chooseSettings.Description = Convert.ToString(item.Values[1]);

            stringToList(item.Values[2], instance.chooseSettings.TrueSide);
            stringToList(item.Values[3], instance.chooseSettings.FalseSide);

            instance.chooseSettings.TrueSideDes = Convert.ToString(item.Values[4]);
            instance.chooseSettings.FalseSideDes = Convert.ToString(item.Values[5]);
            instance.chooseSettings.Time = Convert.ToInt32(item.Values[6]);

            stringToList(item.Values[7], instance.chooseSettings.Effect);

            instance.chooseSettings.Sound = Convert.ToString(item.Values[8]);
            instance.chooseSettings.BGM = Convert.ToString(item.Values[9]);
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
