using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    public bool hasPushed=false;
    private bool countDown=false;
    private float time;
    private GameObject person;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (countDown)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                Destroy(person);
                countDown = false;
            }
        }
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
            collision.gameObject.GetComponent<Control>().beAttacked(15);
            GameData.Instance.hasHurt = true;
        }
        if (collision.transform.tag == "person"&&!hasPushed)
        {
            collision.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
            person = collision.gameObject;
            countDown = true;
            time=0.5f;
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
