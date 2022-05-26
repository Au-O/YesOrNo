using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSet : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> list;
    // Start is called before the first frame update
    void Start()
    {
        int i = Random.Range(0, 2);
        list[i].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
