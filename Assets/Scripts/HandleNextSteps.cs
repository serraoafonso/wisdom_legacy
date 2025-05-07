using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandleNextSteps : MonoBehaviour
{
    public TextMeshProUGUI texto;
    // Start is called before the first frame update
    void Start()
    {
        if(GameData.language == "en")
        {
            texto.text = "Creation of levels for specific areas: science, history and finance.\r\nCreation of personalized levels according to each player's preferences.\r\nImprovement of the store.\r\nCreation of an interface that allows users to create levels for educational or entertainment purposes (e.g. schools, influencers).";
        }
        else
        {
            texto.text = "Cria��o de n�veis das �reas espec�ficas: ci�ncia, hist�ria e finan�as.\r\nCria��o de n�veis personalizados de acordo com as prefer�ncias de cada jogador.\r\nMelhoria da loja.\r\nCria��o de uma interface que permite a cria��o de n�veis pelos usu�rios, para fins educacionais ou para entretenimento (ex: escolas, influencers).";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.language == "en")
        {
            texto.text = "Creation of levels for specific areas: science, history and finance.\r\nCreation of personalized levels according to each player's preferences.\r\nImprovement of the store.\r\nCreation of an interface that allows users to create levels for educational or entertainment purposes (e.g. schools, influencers).";
        }
        else
        {
            texto.text = "Cria��o de n�veis das �reas espec�ficas: ci�ncia, hist�ria e finan�as.\r\nCria��o de n�veis personalizados de acordo com as prefer�ncias de cada jogador.\r\nMelhoria da loja.\r\nCria��o de uma interface que permite a cria��o de n�veis pelos usu�rios, para fins educacionais ou para entretenimento (ex: escolas, influencers).";
        }
    }
}
