using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandleStore : MonoBehaviour
{
    public TextMeshProUGUI sciencePoints;
    public TextMeshProUGUI historyPoints;
    public TextMeshProUGUI financePoints;
    public TextMeshProUGUI text_soon;
    public TextMeshProUGUI Text1;
    public TextMeshProUGUI Text2;
    public TextMeshProUGUI Text3;
    // Start is called before the first frame update
    void Start()
    {
        if(GameData.language == "en")
        {
            Text1.text = "Buy";
            Text2.text = "Buy";
            Text3.text = "Buy";
        }
        else
        {
            Text1.text = "Comprar";
            Text2.text = "Comprar";
            Text3.text = "Comprar";
        }
        sciencePoints.text = GameData.sciencePoints.ToString();
        historyPoints.text = GameData.historyPoints.ToString();
        financePoints.text = GameData.financePoints.ToString();
    }


    public void onBuyClicked()
    {
        StartCoroutine(MostrarMensagemTemporaria());
    }

    private IEnumerator MostrarMensagemTemporaria()
    {
        if (GameData.language == "en") {
            text_soon.text = "Coming soon!";
        }
        else
        {
            text_soon.text = "Em breve!";
        }
        

        yield return new WaitForSeconds(3f);

        text_soon.text = "";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
