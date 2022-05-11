using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownObject : MonoBehaviour
{
    public int hurt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void down()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Rigidbody2D>().AddForce(5 * Vector2.down);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<Control>().blood -= hurt;
        }
        Destroy(this.gameObject);
    }
}
