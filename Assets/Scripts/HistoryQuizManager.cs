using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class HistoryQuizManager : MonoBehaviour
{
    public TextMeshProUGUI factText; // Texto do fato hist�rico
    public TextMeshProUGUI questionText; // Texto da pergunta
    public Button[] answerButtons; // Bot�es de resposta
    public AudioSource audioSource; // AudioSource para sons de resposta
    public AudioClip correctSound; // Som de resposta correta
    public AudioClip incorrectSound; // Som de resposta incorreta
    public GameObject factPanel; // Painel que exibe o fato hist�rico
    public GameObject questionPanel; // Painel que exibe a pergunta
    public TextMeshProUGUI themeText;
    public TextMeshProUGUI HistoryPointsText;
    public GameObject Canva; // Painel para controle adicional

    [SerializeField] private GhostText ghostText;
    public PlayerMovement playerMovement; // Refer�ncia ao script PlayerMovement
    public GameObject book1; // Refer�ncia ao objeto do livro
    public GameObject book2; // Refer�ncia ao objeto do livro
    public GameObject book3; // Refer�ncia ao objeto do livro
    public GameObject book4; // Refer�ncia ao objeto do livro
    public GameObject player; // Refer�ncia ao jogador

    private int currentFactIndex;
    private List<int> factIndexes;

    private string[] historyFacts = {
    "O Renascimento foi um movimento cultural que ocorreu na Europa entre os s�culos XIV e XVII, caracterizado pelo ressurgimento das artes, ci�ncias e do pensamento humanista.",
    "A Revolu��o Francesa, que come�ou em 1789, foi um marco que derrubou a monarquia absolutista na Fran�a, resultando em profundas mudan�as sociais e pol�ticas.",
    "A Guerra Civil Americana (1861-1865) foi um conflito entre os estados do Norte e do Sul dos EUA, em grande parte devido � quest�o da escravid�o e � luta pela preserva��o da Uni�o.",
    "A Primeira Guerra Mundial (1914-1918) envolveu grandes pot�ncias mundiais e resultou em milh�es de mortos, al�m de mudan�as significativas no mapa pol�tico da Europa.",
    "A Era das Grandes Navega��es, entre os s�culos XV e XVII, foi um per�odo de expans�o mar�tima europeia que levou � descoberta de novas rotas e territ�rios ao redor do mundo.",
    "A cria��o da Organiza��o das Na��es Unidas (ONU) em 1945 teve como objetivo promover a paz e a coopera��o internacional ap�s a devasta��o da Segunda Guerra Mundial.",
    "A Revolu��o Industrial, que come�ou no s�culo XVIII, transformou a economia mundial com o surgimento de m�quinas e f�bricas, promovendo urbaniza��o e inova��o tecnol�gica.",
    "A Declara��o de Independ�ncia dos Estados Unidos foi assinada em 1776, marcando a separa��o oficial das Treze Col�nias do dom�nio brit�nico.",
    "A Segunda Guerra Mundial (1939-1945) foi um conflito global que envolveu as principais pot�ncias mundiais e levou � divis�o do mundo entre blocos ideol�gicos.",
    "O Tratado de Versalhes, assinado em 1919, marcou o fim oficial da Primeira Guerra Mundial, impondo condi��es severas � Alemanha.",
    "O Imp�rio Romano foi uma das civiliza��es mais influentes da hist�ria, deixando um legado em �reas como direito, engenharia e cultura."
};

    private string[] historyQuestions = {
    "Qual foi o impacto cultural do Renascimento na Europa?",
    "Quais foram algumas das principais consequ�ncias da Revolu��o Francesa?",
    "Qual foi a causa central da Guerra Civil Americana?",
    "Qual foi o principal resultado da Primeira Guerra Mundial para o mapa pol�tico europeu?",
    "Qual era o principal objetivo da Era das Grandes Navega��es?",
    "Por que foi criada a ONU em 1945?",
    "Qual foi o impacto da Revolu��o Industrial no mundo?",
    "O que representou a assinatura da Declara��o de Independ�ncia dos Estados Unidos?",
    "Quais foram as principais consequ�ncias da Segunda Guerra Mundial?",
    "O que foi o Tratado de Versalhes e qual foi seu impacto na Alemanha?",
    "Qual foi o legado mais duradouro do Imp�rio Romano?"
};

    private string[] correctAnswers = {
    "Ressurgimento das artes e ci�ncias e fortalecimento do pensamento humanista.",
    "Derrubada da monarquia e estabelecimento de princ�pios de igualdade e liberdade.",
    "Conflito entre estados do Norte e do Sul sobre a quest�o da escravid�o e a preserva��o da Uni�o.",
    "Mudan�as significativas nas fronteiras europeias e o enfraquecimento de imp�rios tradicionais.",
    "Descobrir novas rotas e territ�rios para expandir o com�rcio e a influ�ncia europeia.",
    "Promover a paz e a coopera��o internacional ap�s a Segunda Guerra Mundial.",
    "Transformou a economia mundial, promovendo urbaniza��o, inova��o e o surgimento de f�bricas.",
    "A separa��o oficial das Treze Col�nias do dom�nio brit�nico e a forma��o de uma na��o independente.",
    "A divis�o do mundo entre blocos ideol�gicos e a reconstru��o de pa�ses devastados pelo conflito.",
    "Imposi��o de condi��es severas � Alemanha, que contribuiu para tens�es pol�ticas futuras.",
    "Legado em �reas como direito, engenharia, arquitetura e cultura que influenciam at� hoje."
};

    private string[][] incorrectAnswers = new string[][] {
    new string[] { "Decl�nio do interesse em ci�ncias e artes, com foco exclusivo em religi�o.", "Expans�o da monarquia absoluta na Europa." },
    new string[] { "Fortalecimento da monarquia na Fran�a.", "Cria��o de uma alian�a entre Fran�a e Inglaterra contra a �ustria." },
    new string[] { "Quest�es territoriais com o M�xico.", "Conflito religioso entre estados do Norte e do Sul dos EUA." },
    new string[] { "Unifica��o completa dos pa�ses europeus sob um governo �nico.", "Expans�o do imp�rio alem�o sobre toda a Europa Ocidental." },
    new string[] { "Explora��o de minas de ouro no continente africano.", "Conquista e coloniza��o da �sia exclusivamente." },
    new string[] { "Promover a divis�o dos territ�rios coloniais da Europa.", "Organizar o com�rcio internacional e a explora��o econ�mica global." },
    new string[] { "Reduziu a inova��o tecnol�gica e aumentou a depend�ncia de trabalho manual.", "Teve impacto apenas na Europa, sem influenciar outras partes do mundo." },
    new string[] { "A submiss�o das col�nias aos interesses brit�nicos.", "O in�cio de uma nova alian�a entre as Treze Col�nias e a Inglaterra." },
    new string[] { "A unifica��o imediata de todas as na��es sob um governo global.", "O fortalecimento dos imp�rios europeus em todo o mundo." },
    new string[] { "A restaura��o da monarquia alem� ap�s a guerra.", "Uma alian�a entre Fran�a e Alemanha para reconstru��o econ�mica." },
    new string[] { "Impacto limitado � pen�nsula It�lica, sem influenciar outras civiliza��es.", "Apenas conquistas militares, sem legado cultural ou jur�dico." }
};


    private void Start()
    {
        audioSource.clip = null;
        InitializeFactIndexes();
        DisplayRandomFact();
    }

    private void InitializeFactIndexes()
    {
        factIndexes = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; // �ndices para selecionar perguntas n�o repetidas
    }

    private void DisplayRandomFact()
    {
        if (factIndexes.Count == 0)
        {
            InitializeFactIndexes(); // Reinicia a lista se todas as perguntas foram feitas
        }

        currentFactIndex = factIndexes[Random.Range(0, factIndexes.Count)];
        themeText.text = "Hist�ria";
        factText.text = historyFacts[currentFactIndex]; // Atualiza o fato hist�rico exibido
        factIndexes.Remove(currentFactIndex); // Remove o �ndice exibido

        factPanel.SetActive(true);
        questionPanel.SetActive(false);
    }

    public void OnNextButtonClicked()
    {
        factPanel.SetActive(false);
        questionPanel.SetActive(true);

        questionText.text = historyQuestions[currentFactIndex];
        SetupAnswers();
    }

    private void SetupAnswers()
    {
        // Obter a resposta correta
        string correctAnswer = correctAnswers[currentFactIndex];

        // Obter as respostas incorretas
        string[] incorrectAnswersArray = incorrectAnswers[currentFactIndex];

        // Criar lista para todas as respostas e adicionar a correta em posi��o aleat�ria
        List<string> allAnswers = new List<string>(incorrectAnswersArray);
        int correctAnswerPosition = Random.Range(0, allAnswers.Count + 1);
        allAnswers.Insert(correctAnswerPosition, correctAnswer);

        // Certifique-se de que temos o mesmo n�mero de bot�es que respostas
        if (answerButtons.Length < allAnswers.Count)
        {
            Debug.LogError("O n�mero de bot�es � menor que o n�mero de respostas dispon�veis!");
            return;
        }

        // Configurar bot�es de resposta
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < allAnswers.Count)
            {
                int buttonIndex = i; // Captura o �ndice localmente para evitar problemas com closures

                // Configurar o bot�o com o texto da resposta
                answerButtons[buttonIndex].GetComponentInChildren<TextMeshProUGUI>().text = allAnswers[buttonIndex];
                answerButtons[buttonIndex].onClick.RemoveAllListeners();

                bool isCorrect = (buttonIndex == correctAnswerPosition);
                answerButtons[buttonIndex].onClick.AddListener(() => AnswerSelected(isCorrect, answerButtons[buttonIndex].gameObject));

                answerButtons[buttonIndex].gameObject.SetActive(true); // Certifique-se de ativar o bot�o
            }
            else
            {
                // Desativar bot�es extras
                answerButtons[i].gameObject.SetActive(false);
            }
        }
    }



    private void AnswerSelected(bool isCorrect, GameObject botao)
    {
        Vector3 playerPosition = player.transform.position;
        GameObject targetBook = DetermineBookBasedOnPlayerPosition();

        if (isCorrect)
        {
            botao.GetComponent<Image>().color = Color.green; // Indica resposta correta
            Debug.Log("Resposta correta!");
            audioSource.clip = correctSound;

            if (targetBook != null)
            {
                DisableBookCollider(targetBook);
            }
            GameData.historyPoints++;
            HistoryPointsText.text = GameData.historyPoints.ToString();

            // Mant�m o Canvas ativo por um tempo para mostrar a cor/som
            StartCoroutine(HandleAnswerFeedback(true, botao));
        }
        else
        {
            botao.GetComponent<Image>().color = Color.red; // Indica resposta incorreta
            ghostText.HandleMiss();
            Debug.Log("Resposta incorreta!");
            audioSource.clip = incorrectSound;

            if (GameData.historyPoints > 0)
            {
                GameData.historyPoints--;

            }
            else
            {
                GameData.historyPoints = 0;
            }

            HistoryPointsText.text = GameData.historyPoints.ToString();

            if (player != null)
            {
                // Reposiciona o jogador com base na posi��o Y
                if (playerPosition.y < 10)
                {
                    player.transform.position = new Vector3(-39, 9.531775f, player.transform.position.z);
                    Debug.Log("Jogador reposicionado para a posi��o inicial.");
                }
                else
                {
                    player.transform.position = new Vector3(-130, 37, player.transform.position.z); // Reposicionar jogador
                    Debug.Log("Jogador reposicionado para a posi��o inicial.");
                }
            }

            // Mant�m o Canvas ativo por um tempo para mostrar a cor/som
            StartCoroutine(HandleAnswerFeedback(false, botao));
        }

        audioSource.Play();
    }

    // Corrotina para atrasar o fechamento do Canvas
    private IEnumerator HandleAnswerFeedback(bool isCorrect, GameObject botao)
    {
        // Aguarda 1 segundo para mostrar a cor e tocar o som
        yield return new WaitForSeconds(0.2f);

        // Reseta a cor do bot�o para a cor padr�o
        botao.GetComponent<Image>().color = Color.white;

        // Fecha o Canvas ap�s o feedback
        Canva.SetActive(false);
        playerMovement.collidedStop = false;

        // Exibe um novo fato apenas ap�s o feedback
        DisplayRandomFact();
    }


    // M�todo para determinar qual livro alterar com base na posi��o do jogador
    private GameObject DetermineBookBasedOnPlayerPosition()
    {
        Vector3 playerPosition = player.transform.position;

        if (playerPosition.y < 10) // Exemplo de condi��o
        {
            return book1;
        }
        else if (playerPosition.x <= -100)
        {
            return book2;
        }
        else if (playerPosition.x <= -80)
        {
            return book3;
        }
        else
        {
            return book4;
        }
    }

    private void DisableBookCollider(GameObject book)
    {
        if (book != null)
        {
            BoxCollider2D bookCollider = book.GetComponent<BoxCollider2D>();
            Rigidbody2D bookRigidbody = book.GetComponent<Rigidbody2D>();

            if (bookCollider != null)
            {
                bookCollider.enabled = false;
                Debug.Log($"Box Collider 2D do livro {book.name} desativado.");
            }
            if (bookRigidbody != null)
            {
                bookRigidbody.bodyType = RigidbodyType2D.Dynamic;
                bookRigidbody.gravityScale = 10;
                Debug.Log($"Rigidbody2D do livro {book.name} alterado para Dynamic.");
            }
            Destroy(book, 5);
        }
        else
        {
            Debug.LogWarning("Livro n�o encontrado.");
        }
    }
}
