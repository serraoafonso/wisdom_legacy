using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleWarning : MonoBehaviour
{
    public GameObject initialScreen;
    public GameObject WarningCanvas;
    public TextMeshProUGUI sciencePoints;
    public TextMeshProUGUI historyPoints;
    public TextMeshProUGUI financePoints;
    public TextMeshProUGUI texto;
    public TextMeshProUGUI textoBtnStay;
    public TextMeshProUGUI textoBtnLeave;

    // Start is called before the first frame update
    void Start()
    {
        if(GameData.language == "en")
        {
            texto.text = "Are you sure you want to exit? All progress made will be lost.";
            textoBtnStay.text = "Stay";
            textoBtnLeave.text = "Leave";
        }
        else
        {
            texto.text = "Tens a certeza que queres sair? Todo o progresso feito ser� perdido.";
            textoBtnStay.text = "Ficar";
            textoBtnLeave.text = "Sair";
        }
    }

    public void onButtonStayClicked()
    {
        WarningCanvas.SetActive(false);
    }

    public void onButtonLeaveClicked()
    {
        /*
        financePoints.text = 0.ToString();
        historyPoints.text = 0.ToString();
        sciencePoints.text = 0.ToString();
        GameData.financePoints = 0;
        GameData.sciencePoints = 0;
        GameData.historyPoints = 0;
        WarningCanvas.SetActive(false);
        initialScreen.SetActive(true);*/

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
