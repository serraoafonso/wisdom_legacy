using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class HistoryFinalManager : MonoBehaviour
{
    public TextMeshProUGUI questionText; // Texto da pergunta
    public Button[] answerButtons; // Botões de resposta
    public AudioSource audioSource; // AudioSource para sons de resposta
    public AudioClip correctSound; // Som de resposta correta
    public AudioClip incorrectSound; // Som de resposta incorreta
    public GameObject quizPanel; // Painel que exibe a pergunta e opções
    public GameObject victoryPanel; // Painel de vitória
    public TextMeshProUGUI HistoryPointsText;

    public GameObject finalCanvas; // Canvas final
    public GameObject book1; // Referência ao objeto do livro
    public PlayerMovement playerMovement; // Controle do movimento do jogador

    private int correctAnswersCount; // Contador de respostas corretas seguidas
    private int currentQuestionIndex;
    private List<int> questionIndexes;

    // Dados de perguntas e respostas de história
    private string[] questions = {
        "Qual foi o impacto cultural do Renascimento na Europa?",
    "Quais foram algumas das principais consequências da Revolução Francesa?",
    "Qual foi a causa central da Guerra Civil Americana?",
    "Qual foi o principal resultado da Primeira Guerra Mundial para o mapa político europeu?",
    "Qual era o principal objetivo da Era das Grandes Navegações?",
    "Por que foi criada a ONU em 1945?",
    "Qual foi o impacto da Revolução Industrial no mundo?",
    "O que representou a assinatura da Declaração de Independência dos Estados Unidos?",
    "Quais foram as principais consequências da Segunda Guerra Mundial?",
    "O que foi o Tratado de Versalhes e qual foi seu impacto na Alemanha?",
    "Qual foi o legado mais duradouro do Império Romano?"
    };

    private string[] correctAnswers = {
        "Ressurgimento das artes e ciências e fortalecimento do pensamento humanista.",
    "Derrubada da monarquia e estabelecimento de princípios de igualdade e liberdade.",
    "Conflito entre estados do Norte e do Sul sobre a questão da escravidão e a preservação da União.",
    "Mudanças significativas nas fronteiras europeias e o enfraquecimento de impérios tradicionais.",
    "Descobrir novas rotas e territórios para expandir o comércio e a influência europeia.",
    "Promover a paz e a cooperação internacional após a Segunda Guerra Mundial.",
    "Transformou a economia mundial, promovendo urbanização, inovação e o surgimento de fábricas.",
    "A separação oficial das Treze Colônias do domínio britânico e a formação de uma nação independente.",
    "A divisão do mundo entre blocos ideológicos e a reconstrução de países devastados pelo conflito.",
    "Imposição de condições severas à Alemanha, que contribuiu para tensões políticas futuras.",
    "Legado em áreas como direito, engenharia, arquitetura e cultura que influenciam até hoje."
    };

    private string[][] incorrectAnswers = new string[][] {
            new string[] { "Declínio do interesse em ciências e artes, com foco exclusivo em religião.", "Expansão da monarquia absoluta na Europa." },
    new string[] { "Fortalecimento da monarquia na França.", "Criação de uma aliança entre França e Inglaterra contra a Áustria." },
    new string[] { "Questões territoriais com o México.", "Conflito religioso entre estados do Norte e do Sul dos EUA." },
    new string[] { "Unificação completa dos países europeus sob um governo único.", "Expansão do império alemão sobre toda a Europa Ocidental." },
    new string[] { "Exploração de minas de ouro no continente africano.", "Conquista e colonização da Ásia exclusivamente." },
    new string[] { "Promover a divisão dos territórios coloniais da Europa.", "Organizar o comércio internacional e a exploração econômica global." },
    new string[] { "Reduziu a inovação tecnológica e aumentou a dependência de trabalho manual.", "Teve impacto apenas na Europa, sem influenciar outras partes do mundo." },
    new string[] { "A submissão das colônias aos interesses britânicos.", "O início de uma nova aliança entre as Treze Colônias e a Inglaterra." },
    new string[] { "A unificação imediata de todas as nações sob um governo global.", "O fortalecimento dos impérios europeus em todo o mundo." },
    new string[] { "A restauração da monarquia alemã após a guerra.", "Uma aliança entre França e Alemanha para reconstrução econômica." },
    new string[] { "Impacto limitado à península Itálica, sem influenciar outras civilizações.", "Apenas conquistas militares, sem legado cultural ou jurídico." }
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
        ShuffleList(answerOptions); // Método para embaralhar respostas

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
                Debug.LogWarning($"Box Collider 2D não encontrado no livro {book.name}.");
            }

            if (bookRigidbody != null)
            {
                bookRigidbody.bodyType = RigidbodyType2D.Dynamic; // Define como Dynamic
                bookRigidbody.gravityScale = 10;
                Debug.Log($"Rigidbody2D do livro {book.name} alterado para Dynamic.");
            }
            else
            {
                Debug.LogWarning($"Rigidbody2D não encontrado no livro {book.name}.");
            }

            Destroy(book, 5);
        }
        else
        {
            Debug.LogWarning("Livro não encontrado.");
        }
    }
}
