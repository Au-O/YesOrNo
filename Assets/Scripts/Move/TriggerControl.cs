using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerControl : MonoBehaviour
{
    public GameObject prompt;

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
        
        if (collision.tag == "checkTrigger")
        {
            prompt = collision.transform.GetChild(0).gameObject;
            prompt.GetComponent<CheckControl>().Show();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "checkTrigger")
        {
            prompt = collision.transform.GetChild(0).gameObject;
            prompt.GetComponent<CheckControl>().Hide();
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "checkTrigger")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                this.GetComponent<Control>().blood += prompt.GetComponent<CheckControl>().blood;
                Destroy(collision.gameObject);
            }
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
}
