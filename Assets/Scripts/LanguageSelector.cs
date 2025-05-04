using UnityEngine;
using UnityEngine.UI;

public class LanguageSelector : MonoBehaviour
{
    public Image currentFlagImage;
    public GameObject optionsPanel;

    public Sprite flagPortuguese;
    public Sprite flagEnglish;

    private void Start()
    {
        optionsPanel.SetActive(false); // Esconde o menu no in�cio
        SetLanguage(GameData.language); // Idioma padr�o
    }

    public void ToggleOptions()
    {
        optionsPanel.SetActive(!optionsPanel.activeSelf);
    }

    public void SetLanguage(string lang)
    {
        if (lang == "pt")
        {
            currentFlagImage.sprite = flagPortuguese;
            // Aqui voc� troca os textos para portugu�s
            Debug.Log("Idioma: Portugu�s");
            GameData.language = "pt";
        }
        else if (lang == "en")
        {
            currentFlagImage.sprite = flagEnglish;
            // Aqui voc� troca os textos para ingl�s
            Debug.Log("Language: English");
            GameData.language = "en";
        }

        optionsPanel.SetActive(false); // Fecha o menu
    }
}
