using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class HandleInitial : MonoBehaviour
{
    public GameObject initialScreen;
    public GameObject CreditsCanvas;
    public GameObject StoreScreen;
    public GameObject NextSteps;
    public GameObject LastCanvas;
    public GameObject Levels;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnButtonJogarClicked()
    {
        Levels.SetActive(true);
        initialScreen.SetActive(false);
    }

    public void OnButtonCreditsClicked()
    {
        CreditsCanvas.SetActive(true);
        initialScreen.SetActive(false); 
     
    }

    public void OnButtonLojaClicked()
    {
        StoreScreen.SetActive(true);
        initialScreen.SetActive(false);
    }

    public void OnButtonNextStepsClicked()
    {
        NextSteps.SetActive(true);
        initialScreen.SetActive(false);
    }

    public void OnButtonEndClicked()
    {
        LastCanvas.SetActive(false);
        initialScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
