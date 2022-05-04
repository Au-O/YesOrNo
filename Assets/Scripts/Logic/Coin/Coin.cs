using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    private GameMain gameMain;
    private Animator coinAnim;
    public int choice;
    private UIDataShow uIDataShow;
    public GameObject btn_check;

    private void Start()
    {
        gameMain = GameObject.Find("Settings").GetComponent<GameMain>();
        coinAnim = GameObject.Find("coin_anim").GetComponent<Animator>();
        uIDataShow = GameObject.Find("Settings").GetComponent<UIDataShow>();
        choice = 1;
    }

    public void ThrowCoin()
    {
        if (uIDataShow.PlayerChoose())
                choice = 1;
        else
                choice = 0;
    }

    public void AnimEndEvent()
    {
        choice = Random.Range(0, 2);
        if (choice == 1)
        {
            coinAnim.SetTrigger("coin_true");
            coinAnim.SetBool("nowState", true);
        }
        else
        {
            coinAnim.SetTrigger("coin_false");
            coinAnim.SetBool("nowState", false);
        }
    }
    public void AfterAnim()
    {
        if (!btn_check.activeSelf)
            btn_check.SetActive(true);
        Thread.Sleep(1000);
        coinAnim.SetTrigger("endFlip");
        gameMain.PushChoose(choice);
        gameMain.SolveResult();
        gameMain.uIDataShow.ShowResult();       
        uIDataShow.player.AddMorality(gameMain.resultData.resultSettings.MorilityChange);
        uIDataShow.player.AddEnergy(gameMain.resultData.resultSettings.EnergyChange);
        uIDataShow.SetPlayerInfo(uIDataShow.player.energy, uIDataShow.player.morality);
        Debug.Log("choice:" + choice);
       
        gameMain.uIDataShow.RefreshCoinState();
    }

    
}