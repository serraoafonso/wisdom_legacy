using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class HandleInitial : MonoBehaviour
{

    public GameObject initialCanvas;
    public GameObject initialScreen;
    public GameObject CreditsCanvas;
    public Button ButtonJogar;
    public Button ButtonCredits;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnButtonJogarClicked()
    {
        initialCanvas.SetActive(true);
        initialScreen.SetActive(false);
    }

    public void OnButtonCreditsClicked()
    {
        CreditsCanvas.SetActive(true);
        initialScreen.SetActive(false); 
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
