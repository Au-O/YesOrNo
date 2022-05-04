using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.UIElements;
using UnityEngine.SceneManagement;

public class UIDataShow : MonoBehaviour
{
    private GameMain gamemain;
    public Player player;
    public GameObject choiceObject;
    //Panel下组件
    private Text txtTitle;
    private Text txtTrue;
    private Text txtFalse;
    private Text txtResult;
    private GameObject choice;
    private GameObject result;
    private GameObject canFlip;
    private Animator coinAnim;
    private CoinButton btn_choose;
    private Button btn_check;
    private Image img_energy;
    private Image img_posMorality;
    private Image img_nagMorality;
    private Image bg_normal;
    private Image bg_shock;
    //private Image blur_mask;
    public GameObject img_tick;    //时钟指针

    public int morality = 30;  //道德
    public int energy = 100;  //精力
    public bool show_choice = true; //是否Choice面板
    private int countDown;  //倒计时时间
    public Coroutine cd = null;

    public Text TxtTitle { get => txtTitle; private set => txtTitle = value; }
    public Text TxtTrue { get => txtTrue; private set => txtTrue = value; }
    public Text TxtFalse { get => txtFalse; private set => txtFalse = value; }

    public GameObject Choice { get => choice; private set => choice = value; }
    public GameObject Result { get => result; private set => result = value; }
    public Animator CoinAnim { get => coinAnim; private set => coinAnim = value; }
    public CoinButton BtnChoose { get => btn_choose; private set => btn_choose = value; }
    public Image BgNormal { get => bg_normal; private set => bg_normal = value; }
    public Image ImgEnergy { get => img_energy; private set => img_energy = value; }
    public Image ImgPosMor { get => img_posMorality; private set => img_posMorality = value; }
    public Image ImgNegMor { get => img_nagMorality; private set => img_nagMorality = value; }
    public Image BgShock { get => bg_shock; private set => bg_shock = value; }
    //public Image BlurMask { get => blur_mask; private set => blur_mask = value; }
    public Text TxtResult { get => txtResult; set => txtResult = value; }

    void Start()
    {
        gamemain = this.GetComponent<GameMain>();
        player = gamemain.player;
        TxtTitle = GameObject.Find("text_title").GetComponent<Text>();
        TxtTrue = GameObject.Find("text_left").GetComponent<Text>();
        TxtFalse = GameObject.Find("text_right").GetComponent<Text>();
        TxtResult = GameObject.Find("txt_result").GetComponent<Text>();
        Choice = GameObject.Find("Choice");
        Result = GameObject.Find("Result");
        CoinAnim = GameObject.Find("coin_anim").GetComponent<Animator>();
        BtnChoose = GameObject.Find("btn_choose").GetComponent<CoinButton>();
        btn_check = GameObject.Find("btn_check").GetComponent<Button>();
        BgNormal = GameObject.Find("bg_normal").GetComponent<Image>();
        BgShock = GameObject.Find("bg_shock").GetComponent<Image>();
        ImgEnergy = GameObject.Find("red").GetComponent<Image>();
        ImgNegMor = GameObject.Find("negative").GetComponent<Image>();
        ImgPosMor = GameObject.Find("positive").GetComponent<Image>();
       // BlurMask = GameObject.Find("blur_mask").GetComponent<Image>();
        canFlip = GameObject.Find("canFlip");

    }

    public void SetTxt(string title, string trueStr, string falseStr)
    {
        TxtTitle.text = title;
        TxtTrue.text = trueStr;
        TxtFalse.text = falseStr;
    }

    public void SetPlayerInfo(int e, int m)
    {
        this.energy = e;
        this.morality = m;
    }

    public void ShowResult()
    {
        this.show_choice = false;
        ShowData();
    }
    public void ShowChoice()
    {
        this.show_choice = true;
        ShowData();
    }
    /// <summary>
    /// 显示数据
    /// </summary>
    public void ShowData()
    {
        
        ChangeValue1(energy);
        ChangeValue2(morality);
        choice.SetActive(show_choice);
        result.SetActive(!show_choice);
        BgNormal.gameObject.SetActive(morality > 0);
        BgShock.gameObject.SetActive(morality <= 0);
    }
    public void CountDown(int time)
    {
        StopAllCoroutines();
        countDown = time;
        img_tick.transform.localEulerAngles = new Vector3(0, 0, 90 - 6 * countDown);
        Debug.Log(img_tick.transform.rotation);
        cd = StartCoroutine(CountDown());

    }
    public IEnumerator CountDown()
    {
        while (countDown >= 0 && choiceObject.activeSelf)
        {
            img_tick.transform.Rotate(new Vector3(0, 0, 1), 6);
            //Debug.Log(countDown);
            yield return new WaitForSeconds(1);
            countDown--;
        }
        if(countDown<0)
        {
            coinAnim.gameObject.GetComponent<Coin>().AnimEndEvent();
            btn_check.gameObject.SetActive(false);
            yield return null;
        }
        
    }
        public void ChangeValue1(int energy)
    {
        img_energy.fillAmount=Mathf.Lerp(0,1f,energy/100f);
    }

    public void ChangeValue2(int morality)
    {
        if (morality >= 0)
        {
            img_posMorality.fillAmount = Mathf.Lerp(0,1f,morality/100f) ;
            img_nagMorality.fillAmount = 0;
        }
        else
        {
            img_posMorality.fillAmount = 0;
            img_nagMorality.fillAmount=Mathf.Lerp(0,1f,-morality/100f);
        }
    }

    public void RefreshCoinState()
    {
        btn_choose.RefreshState();
    }

    public bool PlayerChoose()
    {
        return btn_choose.coinState;
    }

    public void onClick()
    {
        //点击确定按钮
        if (show_choice)//event页面
        {
            if (morality <= 0)
            {
                CoinAnim.gameObject.GetComponent<Coin>().AnimEndEvent();
            }
            else
            {
                gamemain.PushChoose(coinAnim.GetComponent<Coin>().choice);
                gamemain.SolveResult();
                ShowResult();
                player.AddMorality(gamemain.resultData.resultSettings.MorilityChange);
                player.AddEnergy(gamemain.resultData.resultSettings.EnergyChange);
                SetPlayerInfo(player.energy, player.morality);
            }
            
        }       
        else //result页面
        {
            if (gamemain.index == gamemain.eventNum-1)
            {
                if (energy <= 0)
                    SceneManager.LoadScene(3);
                else
                    SceneManager.LoadScene(4);
            }
            else if(energy <= 0)
                SceneManager.LoadScene(2);
            else
            {
                ShowChoice();
                gamemain.uIDataShow.RefreshCoinState();
                gamemain.index++;
                CountDown(gamemain.GetLastTime());
            }
        }
    }
}
