using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEnd : MonoBehaviour
{
    public string ChatName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Flowchart flowChart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
            if (flowChart.HasBlock(ChatName))
            {
                flowChart.ExecuteBlock(ChatName);
            }
        }
    }
}
