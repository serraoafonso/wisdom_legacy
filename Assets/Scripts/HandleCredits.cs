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
            texto.text = "Team leader: Afonso Serrão\r\n\r\nDevelopers: Lourenço Pinto, Afonso Serrão\r\n\r\nDesigner: Francisco Sousa";
        }
        else
        {
            texto.text = "Líder de equipa: Afonso Serrão\r\n\r\nProgramadores: Lourenço Pinto, Afonso Serrão\r\n\r\nDesigner: Francisco Sousa";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
