using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TriggerControl : MonoBehaviour
{
    public CheckControl prompt;
    private bool pick = false;

    public void Show(GameObject prompt)
    {
        prompt.SetActive(true);
    }
    public void Hide(GameObject prompt)
    {
        prompt.SetActive(false);
    }
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
            pick = false;
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "checkTrigger")
        {
            if (Input.GetKeyDown(KeyCode.E)&&prompt.canChat)
            {
                prompt.Say();
                
            }
        }
        if (collision.tag == "person")
        {
            if (Input.GetKeyDown(KeyCode.E))
                prompt.pushed();
        }
        if (Input.GetKeyDown(KeyCode.R) && pick)
        {
            this.GetComponent<Control>().blood += prompt.blood;
            Destroy(collision.gameObject);
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
    public void loadScene()
    {
        GameData.Instance.index++;
        GameData.Instance.energy += this.GetComponent<Control>().blood;
        SceneManager.LoadScene(1);
    }
    public void canPick()
    {
        pick = true;
    }
    
}
