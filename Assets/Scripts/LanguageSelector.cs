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
        optionsPanel.SetActive(false); // Esconde o menu no início
        SetLanguage(GameData.language); // Idioma padrão
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
            // Aqui você troca os textos para português
            Debug.Log("Idioma: Português");
            GameData.language = "pt";
        }
        else if (lang == "en")
        {
            currentFlagImage.sprite = flagEnglish;
            // Aqui você troca os textos para inglês
            Debug.Log("Language: English");
            GameData.language = "en";
        }

        optionsPanel.SetActive(false); // Fecha o menu
    }
}
