using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ResultSettings
{
    public int ID;
    public List<int> Include;
    public char IncludeOp;
    public string Event;
    public List<int> Effect;
    public string Sound;
    public string BGM;
    public int MorilityChange;
    public int EnergyChange;
}
public class ResultInfo : MonoBehaviour
{
    public ResultSettings resultSettings;

    [Header("Åä±íÄÚID")]
    public int InitFromID;


}
