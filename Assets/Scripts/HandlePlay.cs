using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlay : MonoBehaviour
{
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void handleClick()
    {
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
