using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TextMeshProUGUI Text1;
    public TextMeshProUGUI Text2;
    public TextMeshProUGUI Text3;   


    // Start is called before the first frame update
    void Start()
    {
        if(GameData.language == "en")
        {
            Text1.text = "Play";
            Text2.text = "Credits";
            Text3.text = "Next steps";
        }
        else
        {
            Text1.text = "Jogar";
            Text2.text = "Créditos";
            Text3.text = "Próximos passos";
        }
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
