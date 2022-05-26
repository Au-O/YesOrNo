using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{
    public UIManager ui;
    private void Start()
    {

    }
    private void Update()
    {
    }
    public void ShowEffect(List<int> efts)
    {
        if (efts.Count <= 0)
        {
            return;
        }
        foreach (var v in efts)
        {
            switch (v)
            {
                case 1:
                    ui.ShakeEffect(5, 0.2f, 30, true);
                    break;
                case 2:
                    ui.ShakeEffect(8, 0.4f, 37, true);
                    break;
                case 3:
                    ui.ShakeEffect(12, 0.5f, 45, true);
                    break;
                case 4://ÉÁ°×
                    Flowchart flowChart1 = GameObject.Find("Flowchart").GetComponent<Flowchart>();
                    if (flowChart1.HasBlock("white"))
                    {
                        flowChart1.ExecuteBlock("white");
                    }
                    break;
                case 5://ºìÆÁ
                    Flowchart flowChart2 = GameObject.Find("Flowchart").GetComponent<Flowchart>();
                    if (flowChart2.HasBlock("red"))
                    {
                        flowChart2.ExecuteBlock("red");
                    }
                    break;
                case 6://Ñ¹ºÚ
                    Flowchart flowChart3 = GameObject.Find("Flowchart").GetComponent<Flowchart>();
                    if (flowChart3.HasBlock("black"))
                    {
                        flowChart3.ExecuteBlock("black");
                    }
                    break;
                case 7://crazy
                    Flowchart flowChart4 = GameObject.Find("Flowchart").GetComponent<Flowchart>();
                    if (flowChart4.HasBlock("crazy"))
                    {
                        flowChart4.ExecuteBlock("crazy");
                    }
                    break;
                case 8://stop crazy
                    Flowchart flowChart5 = GameObject.Find("Flowchart").GetComponent<Flowchart>();
                    if (flowChart5.HasBlock("stopCrazy"))
                    {
                        flowChart5.ExecuteBlock("stopCrazy");
                    }
                    break;
            }
        }
    }

}
