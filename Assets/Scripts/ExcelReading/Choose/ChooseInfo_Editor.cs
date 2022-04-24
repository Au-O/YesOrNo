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
        if (GUILayout.Button("�����IDˢ��"))//��Ӱ�ť�͹��ܡ���������ϵİ�ť������ʱ
        {
            ChooseInfo chooseInfo = (ChooseInfo)target;
            Init(chooseInfo);//��������������InitSelf����
        }
    }

    public void Init(ChooseInfo instance)
    {
        Action init;

        var dictionary = ChooseXls.LoadExcelAsDictionary();
        if (!dictionary.ContainsKey(instance.InitFromID))
        {
            Debug.LogErrorFormat("δ���ҵ�ָ��ID��{0}", instance.InitFromID);
            return;
        }

        IndividualData item = dictionary[instance.InitFromID];

        //System.Convert����������ʵ�ֱ�����ı��Դ������������͵�����Ӧ����Excel��Ԫ���е��ַ���ת����int����������
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
        //�б�����ת��
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
