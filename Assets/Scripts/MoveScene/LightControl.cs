using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LightDown()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Rigidbody2D>().velocity=5 * Vector2.down;
    }
    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<Control>().blood -= 15;
            GameData.Instance.hasHurt = true;
        }
        if (collision.transform.tag == "person")
        {
            
            Flowchart flowChart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
            if (flowChart.HasBlock("fail"))
            {
                flowChart.ExecuteBlock("fail");
            }
            GameData.Instance.hasPushed = false;
        }
            
        Destroy(this.gameObject);    
    }
}
