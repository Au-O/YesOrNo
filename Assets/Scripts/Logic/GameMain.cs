using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMain : MonoBehaviour
{
    #region ���ݴ���
    public ChooseInfo eventData;
    public ResultInfo resultData;

    public List<int> nowChoose;//��ǰ����ѡ��
    public List<int> choosedResult;//���г��ֹ��Ľ��id
    public List<int> isChoosed;

    public Player player;
    public int index;
    public int eventNum = 7;

    #endregion

    #region UI����
    public UIDataShow uIDataShow;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        resultData.getInfo(1001);

        player = new Player();
        uIDataShow = GetComponent<UIDataShow>();
        choosedResult = new List<int>();
        nowChoose = new List<int>();
        index = 0;
        isChoosed = new List<int>(eventNum);
        for (int i = 0; i < eventNum; i++)
            isChoosed.Add(0);

        uIDataShow.CountDown(GetLastTime());
    }

    // Update is called once per frame
    void Update()
    {
        if (index < eventNum)
        {
            eventData.getInfo(index + 1);
            if (isChoosed[index] == 0)
            {
                //��ʾ��Ӧ����
                if (uIDataShow.TxtTitle != null)
                {
                    uIDataShow.SetTxt(eventData.chooseSettings.Description, eventData.chooseSettings.TrueSideDes, eventData.chooseSettings.FalseSideDes);

                    //PlayEventSound();
                    FillUIImg();
                }
            }
        }
        
    }

    public void SolveResult()
    {
        //Debug.Log(nowChoose.Count);
        //if (nowChoose.Count <= index) return;
        List<int> nowResult;
        if (nowChoose[index] == 1)
            nowResult = eventData.chooseSettings.TrueSide;
        else
            nowResult = eventData.chooseSettings.FalseSide;//��ȡѡ���Ӧ�Ľ���б�
        int tempResult = 0;

        foreach(var item in nowResult)
        {
            //���β鿴��ǰ����б��н�������Ƿ����
            int flag = 0;
            resultData.getInfo(item);
            //û��ǰ��������ֻ��һ������
            if (resultData.resultSettings.Include.Equals(null) || resultData.resultSettings.Include.Equals(""))
            {
                //��ǰ��
                if (choosedResult.Count != 0)//�ѳ��ֹ����
                    flag = 1;
            }
            else if (resultData.resultSettings.Include.Count == 1)
            {
                if (!choosedResult.Contains(resultData.resultSettings.Include[0]))//�ѳ��ֵĽ����û�ж�Ӧ�Ľ��
                    flag = 1;
            }

            //����Ϊ &
            if (resultData.resultSettings.IncludeOp=='&')
            {
                int num = resultData.resultSettings.Include.Count;
                int count = 0;//���ѳ��ֵĽ���з�������Ľ�����м���
                foreach (var condition in resultData.resultSettings.Include)
                {
                    if (choosedResult.Contains(condition))
                        count++;
                }
                if (count != num)
                    flag = 1;
            }

            //����Ϊ |
            if (resultData.resultSettings.IncludeOp == '|')
            {
                int tempFlag = 0;
                foreach(var condition in resultData.resultSettings.Include)
                {
                    if (choosedResult.Contains(condition))//���ַ��ϵ�
                    {
                        tempFlag = 1;
                        break;
                    }
                }
                if(tempFlag==0)
                    flag = 1;
            }

            if (flag == 0)
            {
                tempResult = item;
                break;
            }
        }
        isChoosed[index] = 1;
        TriggerResult(tempResult);
        choosedResult.Add(tempResult);

    }
    
    public void TriggerResult(int rid)
    {
        resultData.getInfo(rid);
        //PlayResultSound();
        uIDataShow.TxtResult.text = resultData.resultSettings.Event;
        FillUIImg();
    }

    public int GetLastTime()
    {
        return eventData.chooseSettings.Time;
    }

    public void FillUIImg()
    {
        uIDataShow.ShowData();
        
    }

    public void PushChoose(int choose)
    {
        if (index >= eventNum) return;
        nowChoose.Add(choose);
    }
    public void PushChoose(bool choose)
    {
        if (index >= eventNum) return;
        if (choose == true)
            nowChoose.Add(1);
        else
            nowChoose.Add(0);
    }
}
