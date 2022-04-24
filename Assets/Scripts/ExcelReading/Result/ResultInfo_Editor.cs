using UnityEditor;
using UnityEngine;
using System;
using XlsWork;
using XlsWork.UnitsXls;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(ResultInfo))]
public class ResultInfo_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("�����IDˢ��"))//��Ӱ�ť�͹��ܡ���������ϵİ�ť������ʱ
        {
            ResultInfo resultInfo = (ResultInfo)target;
            Init(resultInfo);//��������������InitSelf����
        }
    }

    public void Init(ResultInfo instance)
    {
        Action init;

        var dictionary = ResultXls.LoadExcelAsDictionary();
        if (!dictionary.ContainsKey(instance.InitFromID))
        {
            Debug.LogErrorFormat("δ���ҵ�ָ��ID��{0}", instance.InitFromID);
            return;
        }

        IndividualData item = dictionary[instance.InitFromID];

        //System.Convert����������ʵ�ֱ�����ı��Դ������������͵�����Ӧ����Excel��Ԫ���е��ַ���ת����int����������
        init = (() =>
        {
            instance.resultSettings.ID = Convert.ToInt32(item.Values[0]);

            stringToList(item.Values[1], instance.resultSettings.Include);

            if(item.Values[2].Length!=0)
                instance.resultSettings.IncludeOp = Convert.ToChar(item.Values[2]);

            instance.resultSettings.Event = Convert.ToString(item.Values[3]);

            stringToList(item.Values[4], instance.resultSettings.Effect);

            instance.resultSettings.Sound = Convert.ToString(item.Values[5]);
            instance.resultSettings.BGM = Convert.ToString(item.Values[6]);
            instance.resultSettings.MorilityChange = Convert.ToInt32(item.Values[7]);
            instance.resultSettings.EnergyChange = Convert.ToInt32(item.Values[8]);
        });
        init();
    }

    private void stringToList(string ss, List<int> setting)
    {
        //�б�����ת��
        setting.Clear();
        if (ss.Length != 0)
        {
            string str = ss.Substring(1, ss.Length - 2);
            string[] strArray = str.Split(',');
            Debug.Log(strArray[0]);
            List<string> list = strArray.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                setting.Add(Convert.ToInt32(list[i]));
            }
        }
        else return;
    }

}
