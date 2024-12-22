using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

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
    public GameObject Canva; // Painel para controle adicional

    [SerializeField] private GhostText ghostText;
    public PlayerMovement playerMovement; // Refer�ncia ao script PlayerMovement
    public GameObject book1; // Refer�ncia ao objeto do livro
    public GameObject player; // Refer�ncia ao jogador

    private int currentFactIndex;
    private List<int> factIndexes;

    private string[] historyFacts = {
        "O Renascimento foi um movimento cultural que ocorreu na Europa entre os s�culos XIV e XVII, caracterizado pelo ressurgimento das artes, ci�ncias e do pensamento humanista.",
        "A Revolu��o Francesa, que come�ou em 1789, foi um marco que derrubou a monarquia absolutista na Fran�a, resultando em profundas mudan�as sociais e pol�ticas.",
        "A Guerra Civil Americana (1861-1865) foi um conflito entre os estados do Norte e do Sul dos EUA, em grande parte devido � quest�o da escravid�o e � luta pela preserva��o da Uni�o.",
        "A Primeira Guerra Mundial (1914-1918) envolveu grandes pot�ncias mundiais e resultou em milh�es de mortos, al�m de mudan�as significativas no mapa pol�tico da Europa.",
        "A Era das Grandes Navega��es, entre os s�culos XV e XVII, foi um per�odo de expans�o mar�tima europeia que levou � descoberta de novas rotas e territ�rios ao redor do mundo.",
        "A cria��o da Organiza��o das Na��es Unidas (ONU) em 1945 teve como objetivo promover a paz e a coopera��o internacional ap�s a devasta��o da Segunda Guerra Mundial."
    };

    private string[] historyQuestions = {
        "Qual foi o impacto cultural do Renascimento na Europa?",
        "Quais foram algumas das principais consequ�ncias da Revolu��o Francesa?",
        "Qual foi a causa central da Guerra Civil Americana?",
        "Qual foi o principal resultado da Primeira Guerra Mundial para o mapa pol�tico europeu?",
        "Qual era o principal objetivo da Era das Grandes Navega��es?",
        "Por que foi criada a ONU em 1945?"
    };

    private string[] correctAnswers = {
        "Ressurgimento das artes e ci�ncias e fortalecimento do pensamento humanista.",
        "Derrubada da monarquia e estabelecimento de princ�pios de igualdade e liberdade.",
        "Conflito entre estados do Norte e do Sul sobre a quest�o da escravid�o e a preserva��o da Uni�o.",
        "Mudan�as significativas nas fronteiras europeias e o enfraquecimento de imp�rios tradicionais.",
        "Descobrir novas rotas e territ�rios para expandir o com�rcio e a influ�ncia europeia.",
        "Promover a paz e a coopera��o internacional ap�s a Segunda Guerra Mundial."
    };

    private string[][] incorrectAnswers = new string[][] {
        new string[] { "Decl�nio do interesse em ci�ncias e artes, com foco exclusivo em religi�o.", "Expans�o da monarquia absoluta na Europa." },
        new string[] { "Fortalecimento da monarquia na Fran�a.", "Cria��o de uma alian�a entre Fran�a e Inglaterra contra a �ustria." },
        new string[] { "Quest�es territoriais com o M�xico.", "Conflito religioso entre estados do Norte e do Sul dos EUA." },
        new string[] { "Unifica��o completa dos pa�ses europeus sob um governo �nico.", "Expans�o do imp�rio alem�o sobre toda a Europa Ocidental." },
        new string[] { "Explora��o de minas de ouro no continente africano.", "Conquista e coloniza��o da �sia exclusivamente." },
        new string[] { "Promover a divis�o dos territ�rios coloniais da Europa.", "Organizar o com�rcio internacional e a explora��o econ�mica global." }
    };

    private void Start()
    {
        InitializeFactIndexes();
        DisplayRandomFact();
    }

    private void InitializeFactIndexes()
    {
        factIndexes = new List<int> { 0, 1, 2, 3, 4, 5 }; // �ndices para selecionar perguntas n�o repetidas
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
        string correctAnswer = correctAnswers[currentFactIndex];
        string[] incorrectAnswersArray = incorrectAnswers[currentFactIndex];

        List<string> allAnswers = new List<string>(incorrectAnswersArray);
        int correctAnswerPosition = Random.Range(0, allAnswers.Count + 1);
        allAnswers.Insert(correctAnswerPosition, correctAnswer);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = allAnswers[i];
            answerButtons[i].onClick.RemoveAllListeners();

            bool isCorrect = (i == correctAnswerPosition);
            answerButtons[i].onClick.AddListener(() => AnswerSelected(isCorrect));
        }
    }

    private void AnswerSelected(bool isCorrect)
    {
        if (isCorrect)
        {
            Debug.Log("Resposta correta!");
            audioSource.clip = correctSound;
            DisableBookCollider(book1);
            Canva.SetActive(false);
            playerMovement.collidedStop = false;
        }
        else
        {
            ghostText.HandleMiss();
            Debug.Log("Resposta incorreta!");
            audioSource.clip = incorrectSound;
            playerMovement.collidedStop = false;
            Canva.SetActive(false);
            if (player != null)
            {
                player.transform.position = new Vector3(-39, 9.531775f, player.transform.position.z); // Reposicionar jogador
                Debug.Log("Jogador reposicionado para a posi��o inicial.");
            }
        }

        audioSource.Play();
        DisplayRandomFact();
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
