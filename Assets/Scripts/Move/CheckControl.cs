using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckControl : MonoBehaviour
{
    public int blood;
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
}