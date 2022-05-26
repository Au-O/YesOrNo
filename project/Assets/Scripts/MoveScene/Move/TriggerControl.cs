using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TriggerControl : MonoBehaviour
{
    public CheckControl prompt;
    public int scene;
 
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "checkTrigger"|| collision.tag == "person")
        {
            prompt = collision.transform.GetChild(0).gameObject.GetComponent<CheckControl>();
            prompt.Show();
            if(collision.tag == "checkTrigger")
                prompt.canChat = true;   
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "checkTrigger" || collision.tag == "person")
        {
            prompt.Hide();
            if (collision.tag == "checkTrigger")
                prompt.canChat =false;
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "checkTrigger")
        {
            if (prompt.canChat)
            {
                if(Input.GetKeyDown(KeyCode.E))
                    prompt.Say();           
                else if (Input.GetKeyDown(KeyCode.R))
                {
                    CheckControl checkItem = prompt.GetComponent<CheckControl>();
                    if (checkItem.canPick)
                    {
                        if (checkItem.thisBag.itemList.Count == 15)
                        {
                            Flowchart flowChart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
                            if (flowChart.HasBlock("bag"))
                            {
                                flowChart.ExecuteBlock("bag");
                            }
                        }
                        else
                        {
                            addNewItem(checkItem);
                            Destroy(collision.gameObject);
                        }
                    }
                    
                    
                }

            }
        }
        if (collision.tag == "person")
        {
            if (Input.GetKeyDown(KeyCode.E))
                prompt.pushed();
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void addNewItem(CheckControl checkItem)
    {
        if (!checkItem.thisBag.itemList.Contains(checkItem.thisItem))
        {
            checkItem.thisBag.itemList.Add(checkItem.thisItem);
            //BagManager.createNewItem(checkItem.thisItem);
        }
        else
        {
            checkItem.thisItem.itemNum++;
        }
        if(this.gameObject.GetComponent<Control>().canOpen)
            BagManager.RefreshItem();
    }
    public void loadScene()
    {
        if(scene==1)
            GameData.Instance.index++;
        GameData.Instance.energy += this.GetComponent<Control>().blood;
        SceneManager.LoadScene(scene);
    }  
}
