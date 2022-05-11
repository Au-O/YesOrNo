using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }
    public int index { get; set; } 
    public int energy { get; set; } 
    public int morality { get; set; } 
    public List<int> choosedResult { get; set; }
    public List<int> isChoosed{ get; set; } 
    public List<int> nowChoose { get; set; }
    public bool hasPushed { get; set; } = true;
    public bool hasHurt { get; set; } = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}