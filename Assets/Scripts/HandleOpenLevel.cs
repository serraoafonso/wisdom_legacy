using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandleOpenLevel : MonoBehaviour
{
    public GameObject LevelsScreen;
    public GameObject Level1;
    public Button ButtonLevel1;
    // Start is called before the first frame update
    void Start()
    {
        if (GameData.concluded)
        {
            Image imagem = ButtonLevel1.GetComponent<Image>();

            Color cor = imagem.color;
            cor.a = 0.8f;
            imagem.color = cor;
            ButtonLevel1.interactable = false;
        }
    }

    public void onLevel1Clicked()
    {
        if (GameData.concluded == false)
        {
            LevelsScreen.SetActive(false);
            Level1.SetActive(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
