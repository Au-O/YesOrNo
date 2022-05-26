using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitAndSay : MonoBehaviour
{
    public GameObject img_tick;
    public string ChatName;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        img_tick.transform.localEulerAngles = new Vector3(0, 0, 90 - 6 * time);
        StartCoroutine("startCountDown");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator startCountDown()
    {
        while (time >= 0)
        {
            time--;
            img_tick.transform.Rotate(new Vector3(0, 0, 1), 6);
            if (time == 0)
            {
                Flowchart flowChart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
                if (flowChart.HasBlock(ChatName))
                {
                    flowChart.ExecuteBlock(ChatName);
                }
                yield break;//停止 协程
            }
            else if (time > 0)
            {
                yield return new WaitForSeconds(1);// 每次 自减1，等待 1 秒
            }
        }
    }
}
