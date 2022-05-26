using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CoinButton : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler
{
    public bool coinState = true;
    private Animator coinAnim;
    private GameObject trueTxt;
    private GameObject falseTxt;
    private PointerEventData pointerEventData;

    private void Start()
    {
        trueTxt = GameObject.Find("text_left");
        falseTxt = GameObject.Find("text_right");
        coinAnim = GameObject.Find("coin_anim").GetComponent<Animator>();
        coinAnim.SetBool("nowState", coinState);
        trueTxt.SetActive(false);
        falseTxt.SetActive(false);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (coinState)
        {
            trueTxt.SetActive(true);
            falseTxt.SetActive(false);
        }
        else
        {
            trueTxt.SetActive(false);
            falseTxt.SetActive(true);
        }
        pointerEventData = eventData;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        trueTxt.SetActive(false);
        falseTxt.SetActive(false);
        pointerEventData = eventData;
    }

    public void OnClick()
    {
        if (coinState)
        {
            coinAnim.SetTrigger("coin_choose_toF");
        }
        else
        {
            coinAnim.SetTrigger("coin_choose_toT");
        }
        coinState = !coinState;
        coinAnim.SetBool("nowState", coinState);
        OnPointerEnter(pointerEventData);
        coinAnim.GetComponent<Coin>().ThrowCoin();
    }

    public void RefreshState()
    {

        coinAnim.SetTrigger("coin_choose_toT");
        coinState = true;
        coinAnim.SetBool("nowState", coinState);
        trueTxt.SetActive(false);
        falseTxt.SetActive(false);
        OnPointerEnter(pointerEventData);
        coinAnim.GetComponent<Coin>().choice = 1;
    }
}
