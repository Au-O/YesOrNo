using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDown : MonoBehaviour
{
    public List<Transform> objectArray;
    private int time=25;
    public float inter;
    // Start is called before the first frame update
    void Start()
    {
        objectArray = new List<Transform>();
        foreach (Transform child in this.transform)
        {
            objectArray.Add(child);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag=="Player")
            StartCoroutine("startCountDown");
    }
    public IEnumerator startCountDown()
    {
        while (time > 0)
        {
            time -= 5;
            if (time % 5 == 0)
            {
                objectDown();
            }
            if (time == 0)
            {
                yield break;//Í£Ö¹ Ð­³Ì
            }
            else if (time > 0)
            {
                yield return new WaitForSeconds(inter);
            }
        }
    }
    public void objectDown()
    {
        int i = Random.Range(0, objectArray.Count);
        if (objectArray[i]!=null)
            objectArray[i].gameObject.GetComponent<DownObject>().down();
        objectArray.RemoveAt(i);
    }
}
