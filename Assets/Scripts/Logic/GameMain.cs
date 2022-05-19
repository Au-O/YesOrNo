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

    public List<int> moveScene;
    public List<int> sceneId;

    public bool justUI = true;
    #endregion

    #region UI控制
    public UIDataShow uIDataShow;
    #endregion

    #region 特效控制
    public EffectManager emt;
    public bool playEffect;
    #endregion

    #region 音效
    public Sound sound;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        playEffect = true;
        player = new Player();
        uIDataShow = GetComponent<UIDataShow>();
        if (GameData.Instance.index == 0)
        {
            resultData.getInfo(1001);
            choosedResult = new List<int>();
            isChoosed = new List<int>(eventNum);
            nowChoose = new List<int>();
            for (int i = 0; i < eventNum; i++)
                isChoosed.Add(0);
        }
        else
        {
            index = GameData.Instance.index;
            choosedResult = GameData.Instance.choosedResult;
            isChoosed = GameData.Instance.isChoosed;
            nowChoose = GameData.Instance.nowChoose;
            uIDataShow.energy = GameData.Instance.energy;
            uIDataShow.morality = GameData.Instance.morality;
            player.energy= GameData.Instance.energy;
            player.morality = GameData.Instance.morality;
            //uIDataShow.RefreshCoinState();
            //uIDataShow.CountDown(GetLastTime());
        }
        Debug.Log(uIDataShow.energy);
        if (uIDataShow.energy <= 0)
            SceneManager.LoadScene(2);
        if (uIDataShow.morality <= 0)
        {
            List<int> temp = new List<int>();
            temp.Add(7);
            GameObject.Find("Canvas").GetComponent<EffectManager>().ShowEffect(temp);
        }
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
                    if (playEffect)
                    {
                        PlayEventSound();
                        playEffect = false;
                    }

                    FillUIImg();
                }
            }
        }
        
    }

    public void SolveResult()
    {
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
        if (tempResult == 5002 && GameData.Instance.hasPushed == false)
            tempResult = 5001;
        else if (tempResult == 5004 && GameData.Instance.hasPushed == false)
        {
            if (GameData.Instance.hasHurt)
                tempResult = 5006;
            else
                tempResult = 5003;
        }
        else if (tempResult == 5005 && GameData.Instance.hasPushed == false)
            tempResult = 5006;
            
        TriggerResult(tempResult);
        choosedResult.Add(tempResult);
        player.AddMorality(resultData.resultSettings.MorilityChange);
        player.AddEnergy(resultData.resultSettings.EnergyChange);
        uIDataShow.SetPlayerInfo(player.energy, player.morality);
        checkScene(tempResult);

    }
    
    public void TriggerResult(int rid)
    {
        resultData.getInfo(rid);
        PlayResultSound();
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

    public void checkScene(int id)
    {
        if (moveScene.Contains(id))
        {
            justUI = false;
            GameData.Instance.index = index;
            GameData.Instance.energy = uIDataShow.energy;
            GameData.Instance.morality = uIDataShow.morality;
            GameData.Instance.choosedResult = choosedResult;
            GameData.Instance.isChoosed = isChoosed;
            GameData.Instance.nowChoose = nowChoose;
            //Debug.Log("1:" + GameData.Instance.morality);
            SceneManager.LoadScene(sceneId[moveScene.IndexOf(id)]);
        }
    }
    public void PlayEventSound()
    {
        emt.ShowEffect(GetChooseEffect());
        sound.PlayBG(eventData.chooseSettings.BGM);
        sound.PlayEffect(eventData.chooseSettings.Sound);
    }
    public void PlayResultSound()
    {
        emt.ShowEffect(GetResultEffect());
        sound.PlayBG(resultData.resultSettings.BGM);
        sound.PlayEffect(resultData.resultSettings.Sound);
    }
    public List<int> GetChooseEffect()
    {
        return eventData.chooseSettings.Effect;
    }
    public List<int> GetResultEffect()
    {
        return resultData.resultSettings.Effect;
    }
}
