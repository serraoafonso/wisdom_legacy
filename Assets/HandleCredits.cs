using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HandleCredits : MonoBehaviour
{
    public TextMeshProUGUI texto; 

    // Start is called before the first frame update
    void Start()
    {
        if(GameData.language == "en")
        {
            texto.text = "Team leader: Afonso Serr�o\r\n\r\nDevelopers: Louren�o Pinto, Afonso Serr�o\r\n\r\nDesigner: Francisco Sousa";
        }
        else
        {
            texto.text = "L�der de equipa: Afonso Serr�o\r\n\r\nProgramadores: Louren�o Pinto, Afonso Serr�o\r\n\r\nDesigner: Francisco Sousa";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
