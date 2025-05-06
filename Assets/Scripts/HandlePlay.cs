using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HandlePlay : MonoBehaviour
{
    public GameObject canvas;
    public TextMeshProUGUI texto;
    public TextMeshProUGUI textoBtn;
    // Start is called before the first frame update
    void Start()
    {
        if(GameData.language == "en")
        {
            texto.text = "Welcome to Wisdom Legacy! Try to reach the end of the library while learning about topics like financial literacy, science and history!";
            textoBtn.text = "Play!";
        }
        else
        {
            texto.text = "Bem-vindo ao Wisdom Legacy! Tenta chegar ao fim da biblioteca enquando aprendes sobre temas como literacia financeira, ciência e história!";
            textoBtn.text = "Jogar!";
        }
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
