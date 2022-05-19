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

    public List<int> moveScene;
    public List<int> sceneId;

    public bool justUI = true;
    #endregion

    #region UI����
    public UIDataShow uIDataShow;
    #endregion

    #region ��Ч����
    public EffectManager emt;
    public bool playEffect;
    #endregion

    #region ��Ч
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
                //��ʾ��Ӧ����
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
