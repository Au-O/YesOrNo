using UnityEngine;
using UnityEngine.SceneManagement;
public class Player
{
    public int morality;//����
    public int energy;//����

    public Player()
    {
        morality = 30;
        energy = 100;
    }

    public void AddMorality(int value)
    {
        morality += value;
        Mathf.Clamp(morality, 0, 100);
    }
    public void AddEnergy(int value)
    {     
        energy += value;
        Debug.Log("Energy: " + energy);
        Debug.Log("Morality: " + morality);
        Mathf.Clamp(energy, 0, 100);
        if (energy <= 0)
        {
            //Dead();
        }
    }

    public void Dead()
    {
        GameMain gameMain = GameObject.Find("Settings").GetComponent<GameMain>();
        if (gameMain.eventNum - 1 == gameMain.index)
        {
            //todo ���2
            SceneManager.LoadScene(3);
        }
        else
        {
            //todo ���1
            SceneManager.LoadScene(2);
        }
    }
}
