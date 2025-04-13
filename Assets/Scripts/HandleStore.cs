using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandleStore : MonoBehaviour
{
    public TextMeshProUGUI sciencePoints;
    public TextMeshProUGUI historyPoints;
    public TextMeshProUGUI financePoints;
    // Start is called before the first frame update
    void Start()
    {
        sciencePoints.text = GameData.sciencePoints.ToString();
        historyPoints.text = GameData.historyPoints.ToString();
        financePoints.text = GameData.financePoints.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
