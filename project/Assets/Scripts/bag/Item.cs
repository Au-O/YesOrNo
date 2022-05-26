using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Bag/New Item")]
public class Item : ScriptableObject
{
    public Sprite itemImg;
    public int itemNum;
    public int blood;
    [TextArea]
    public string itemDes;
    public bool canUse;
}
