using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class HandleOpenLevel : MonoBehaviour
{
    public GameObject LevelsScreen;
    public GameObject Level1;
    public Button ButtonLevel1;
    public TextMeshProUGUI txtBtn1;
    public TextMeshProUGUI txtBtn2;
    public TextMeshProUGUI txtBtn3;
    public TextMeshProUGUI txt2;
    public TextMeshProUGUI txt1;
    // Start is called before the first frame update
    void Start()
    {
        if(GameData.language == "en")
        {
            txtBtn1.text = "Level 1 - General";
            txtBtn2.text = "Level 2 - Science";
            txtBtn3.text = "Level 3 - History";
            txt1.text = "Coming soon!";
            txt2.text = "Coming soon!";
        }
        else
        {
            txtBtn1.text = "Nível 1 - Geral";
            txtBtn2.text = "Nível 2 - Ciência\r\n";
            txtBtn3.text = "Nível 3 - História\r\n";
            txt1.text = "Em breve!\r\n";
            txt2.text = "Em breve!\r\n";
        }

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
        if (GameData.language == "en")
        {
            txtBtn1.text = "Level 1 - General";
            txtBtn2.text = "Level 2 - Science";
            txtBtn3.text = "Level 3 - History";
            txt1.text = "Coming soon!";
            txt2.text = "Coming soon!";
        }
        else
        {
            txtBtn1.text = "Nível 1 - Geral";
            txtBtn2.text = "Nível 2 - Ciência\r\n";
            txtBtn3.text = "Nível 3 - História\r\n";
            txt1.text = "Em breve!\r\n";
            txt2.text = "Em breve!\r\n";
        }
    }
}
