using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagManager : MonoBehaviour
{
    static BagManager instance;
    public Button useBtn;
    public Button removeBtn;
    public Bag myBag;
    public GameObject slotGrid;
    public Slot slotPrefab;
    public Text itemDes;
    public Slot bePicked;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }
    private void OnEnable()
    {
        RefreshItem();


        
    }
    public void Update()
    {
        if (instance.bePicked != null)
        {
            instance.removeBtn.interactable = true;
            if (instance.bePicked.slotItem.canUse)
                instance.useBtn.interactable = true;
            else
                instance.useBtn.interactable = false;
        }
    }
    public static void updateItemInfo(string itemDescrip)
    {
        instance.itemDes.text = itemDescrip;
    }
    public static void createNewItem(Item item)
    {
        Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position,Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.itemImg;
        newItem.slotNum.text = item.itemNum.ToString();
    }

    public static void RefreshItem()
    {
        instance.itemDes.text = "";
        instance.useBtn.interactable = false;
        instance.removeBtn.interactable = false;
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if (instance.slotGrid.transform.childCount == 0)
                break;
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
        }
        for(int i = 0; i < instance.myBag.itemList.Count; i++)
        {
            createNewItem(instance.myBag.itemList[i]);
        }
    }
    public static void clickedItem(Slot slot)
    {
        instance.bePicked = slot;
    }
    public static void useClickedItem()
    {
        GameObject.Find("Man").GetComponent<Control>().blood += instance.bePicked.slotItem.blood;
        if (instance.bePicked.slotItem.itemNum > 1)
            instance.bePicked.slotItem.itemNum--;
        else
        {//数量为1，直接在背包中删除数据
            for(int i = instance.myBag.itemList.Count - 1; i >= 0; i--)
            {
                if (instance.myBag.itemList[i] == instance.bePicked.slotItem)
                {
                    instance.myBag.itemList.Remove(instance.myBag.itemList[i]);
                }
            }
        }
        RefreshItem();
    }
    public static void removeClickedItem()
    {
        if (instance.bePicked.slotItem.itemNum > 1)
            instance.bePicked.slotItem.itemNum--;
        else
        {//数量为1，直接在背包中删除数据
            for (int i = instance.myBag.itemList.Count - 1; i >= 0; i--)
            {
                if (instance.myBag.itemList[i] == instance.bePicked.slotItem)
                {
                    instance.myBag.itemList.Remove(instance.myBag.itemList[i]);
                }
            }
        }
        RefreshItem();
    }
}
