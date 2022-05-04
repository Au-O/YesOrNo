using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ChooseSettings
{
    public int ID;
    public string Description;
    public List<int> TrueSide;
    public List<int> FalseSide;
    public string TrueSideDes;
    public string FalseSideDes;
    public int Time;
    public List<int> Effect;
    public string Sound;
    public string BGM;
}
public class ChooseInfo : MonoBehaviour
{
    public ChooseSettings chooseSettings;

    [Header("Åä±íÄÚID")]
    public int InitFromID;

    public void getInfo(int i)
    {
        InitFromID = i;
        this.GetComponent<ChooseInfo_Editor>().Init();
    }
    
}
