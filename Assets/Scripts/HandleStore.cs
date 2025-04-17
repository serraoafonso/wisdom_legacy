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
    // Start is called before the first frame update
    void Start()
    {
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
        text_soon.text = "Em breve!";

        yield return new WaitForSeconds(3f);

        text_soon.text = "";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
