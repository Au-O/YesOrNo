using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player
{
    public int morality;//道德
    public int energy;//精力
    
    public Player()
    {
        morality = 20;
        energy = 45;
    }

    public void AddMorality(int value)
    {
        morality += value;
        Mathf.Clamp(morality, 0, 100);
        if (morality <= 0)
        {
            List<int> temp = new List<int>();
            temp.Add(7);
            GameObject.Find("Canvas").GetComponent<EffectManager>().ShowEffect(temp);
        }
        else if (morality > 0)
        {
            List<int> temp = new List<int>();
            temp.Add(8);
            GameObject.Find("Canvas").GetComponent<EffectManager>().ShowEffect(temp);
        }
    }
    public void AddEnergy(int value)
    {     
        energy += value;
        Debug.Log("Energy: " + energy);
        Debug.Log("Morality: " + morality);
        Mathf.Clamp(energy, 0, 100);
    }

}
