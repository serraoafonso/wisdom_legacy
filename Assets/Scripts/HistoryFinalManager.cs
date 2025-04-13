using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class HistoryFinalManager : MonoBehaviour
{
    public TextMeshProUGUI questionText; // Texto da pergunta
    public Button[] answerButtons; // Bot�es de resposta
    public AudioSource audioSource; // AudioSource para sons de resposta
    public AudioClip correctSound; // Som de resposta correta
    public AudioClip incorrectSound; // Som de resposta incorreta
    public GameObject quizPanel; // Painel que exibe a pergunta e op��es
    public GameObject victoryPanel; // Painel de vit�ria
    public TextMeshProUGUI HistoryPointsText;

    public GameObject finalCanvas; // Canvas final
    public GameObject book1; // Refer�ncia ao objeto do livro
    public PlayerMovement playerMovement; // Controle do movimento do jogador

    private int correctAnswersCount; // Contador de respostas corretas seguidas
    private int currentQuestionIndex;
    private List<int> questionIndexes;

    // Dados de perguntas e respostas de hist�ria
    private string[] questions = {
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
        StartQuiz();
    }

    private void StartQuiz()
    {
        // Inicializa o quiz com todas as perguntas
        questionIndexes = new List<int>();
        for (int i = 0; i < questions.Length; i++)
        {
            questionIndexes.Add(i);
        }
        ShuffleQuestions();
        correctAnswersCount = 0; // Reseta contador de respostas corretas seguidas
        LoadQuestion();
    }

    private void ShuffleQuestions()
    {
        for (int i = 0; i < questionIndexes.Count; i++)
        {
            int temp = questionIndexes[i];
            int randomIndex = Random.Range(i, questionIndexes.Count);
            questionIndexes[i] = questionIndexes[randomIndex];
            questionIndexes[randomIndex] = temp;
        }
    }

    private void LoadQuestion()
    {
        if (questionIndexes.Count == 0)
        {
            Debug.Log("No more questions available.");
            return;
        }

        currentQuestionIndex = questionIndexes[0];
        questionIndexes.RemoveAt(0);

        questionText.text = questions[currentQuestionIndex];
        List<string> answerOptions = new List<string>(incorrectAnswers[currentQuestionIndex]);
        answerOptions.Add(correctAnswers[currentQuestionIndex]);
        ShuffleList(answerOptions); // M�todo para embaralhar respostas

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answerOptions[i];
            answerButtons[i].onClick.RemoveAllListeners();
            bool isCorrect = answerOptions[i] == correctAnswers[currentQuestionIndex];
            answerButtons[i].onClick.AddListener(() => Answer(isCorrect));
        }
    }

    private void ShuffleList(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            string temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void Answer(bool isCorrect)
    {
        if (isCorrect)
        {
            correctAnswersCount++;
            audioSource.PlayOneShot(correctSound);

            GameData.historyPoints++;
            HistoryPointsText.text = GameData.historyPoints.ToString();
            if (correctAnswersCount >= 3)
            {
                playerMovement.collidedStop = false;
                DisableBookCollider(book1);
                finalCanvas.SetActive(false);
                GameData.historyPoints += 3;
                HistoryPointsText.text = GameData.historyPoints.ToString();
                return;
            }
        }
        else
        {
            if (GameData.historyPoints > 1)
            {
                GameData.historyPoints -= 2;

            }
            else
            {
                GameData.historyPoints = 0;
            }
            HistoryPointsText.text = GameData.historyPoints.ToString();
            audioSource.PlayOneShot(incorrectSound);
            correctAnswersCount = 0; // Reinicia contagem de respostas corretas seguidas
            StartQuiz(); // Reinicia o quiz
            return;
        }
        LoadQuestion();
    }

    private void DisableBookCollider(GameObject book)
    {
        if (book != null)
        {
            BoxCollider2D bookCollider = book.GetComponent<BoxCollider2D>();
            Rigidbody2D bookRigidbody = book.GetComponent<Rigidbody2D>();

            if (bookCollider != null)
            {
                bookCollider.enabled = false; // Desativa o Box Collider 2D
                Debug.Log($"Box Collider 2D do livro {book.name} desativado.");
            }
            else
            {
                Debug.LogWarning($"Box Collider 2D n�o encontrado no livro {book.name}.");
            }

            if (bookRigidbody != null)
            {
                bookRigidbody.bodyType = RigidbodyType2D.Dynamic; // Define como Dynamic
                bookRigidbody.gravityScale = 10;
                Debug.Log($"Rigidbody2D do livro {book.name} alterado para Dynamic.");
            }
            else
            {
                Debug.LogWarning($"Rigidbody2D n�o encontrado no livro {book.name}.");
            }

            Destroy(book, 5);
        }
        else
        {
            Debug.LogWarning("Livro n�o encontrado.");
        }
    }
}
