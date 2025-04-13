using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleOpenLevel : MonoBehaviour
{
    public GameObject LevelsScreen;
    public GameObject Level1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void onLevel1Clicked()
    {
        LevelsScreen.SetActive(false);
        Level1.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
