using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class HandleVictoryPanel : MonoBehaviour
{
    public TextMeshProUGUI textBtn;
    public TextMeshProUGUI texto;
    // Start is called before the first frame update
    void Start()
    {
        if(GameData.language == "en")
        {
            textBtn.text = "End";
            texto.text = "Congratulations! You've completed the library! Thank you for being part of the beginning of something much bigger!";
        }else
        {
            textBtn.text = "Terminar";
            texto.text = "Parabéns! Completaste a biblioteca! Obrigado por fazeres parte do início de algo muito maior!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
