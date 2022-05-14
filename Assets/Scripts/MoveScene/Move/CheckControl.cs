using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class CheckControl : MonoBehaviour
{
    public bool canPick;
    public int blood;
    public string ChatName;    //定义选择哪个对话block
    public bool canChat = false;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Show()
    {
        this.gameObject.SetActive(true);
    }
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Say()
    {
        if (canChat)
        {
            Flowchart flowChart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
            if (flowChart.HasBlock(ChatName))
            {
                flowChart.ExecuteBlock(ChatName);
            }
        }
    }
    public void pushed()
    {
        gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity=Vector2.right*3;
        anim.SetTrigger("push");
        Flowchart flowChart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
        if (flowChart.HasBlock(ChatName))
        {
            flowChart.ExecuteBlock(ChatName);
        }
    }
    public void destroy()
    {
        Destroy(transform.parent.gameObject);
    }
}
