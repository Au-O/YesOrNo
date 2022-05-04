using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMain : MonoBehaviour
{
    #region 数据处理
    public ChooseInfo eventData;
    public ResultInfo resultData;

    public List<int> nowChoose;//当前做的选择
    public List<int> choosedResult;//所有出现过的结果id
    public List<int> isChoosed;

    public Player player;
    public int index;
    public int eventNum = 7;

    #endregion

    #region UI控制
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
                //显示对应文字
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
            nowResult = eventData.chooseSettings.FalseSide;//获取选择对应的结果列表
        int tempResult = 0;

        foreach(var item in nowResult)
        {
            //依次查看当前结果列表中结果条件是否符合
            int flag = 0;
            resultData.getInfo(item);
            //没有前置条件或只有一个条件
            if (resultData.resultSettings.Include.Equals(null) || resultData.resultSettings.Include.Equals(""))
            {
                //无前置
                if (choosedResult.Count != 0)//已出现过结果
                    flag = 1;
            }
            else if (resultData.resultSettings.Include.Count == 1)
            {
                if (!choosedResult.Contains(resultData.resultSettings.Include[0]))//已出现的结果中没有对应的结果
                    flag = 1;
            }

            //符号为 &
            if (resultData.resultSettings.IncludeOp=='&')
            {
                int num = resultData.resultSettings.Include.Count;
                int count = 0;//对已出现的结果中符合需求的结果进行计数
                foreach (var condition in resultData.resultSettings.Include)
                {
                    if (choosedResult.Contains(condition))
                        count++;
                }
                if (count != num)
                    flag = 1;
            }

            //符号为 |
            if (resultData.resultSettings.IncludeOp == '|')
            {
                int tempFlag = 0;
                foreach(var condition in resultData.resultSettings.Include)
                {
                    if (choosedResult.Contains(condition))//出现符合的
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
