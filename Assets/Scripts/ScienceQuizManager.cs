using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ScienceQuizManager : MonoBehaviour
{
    public TextMeshProUGUI factText; // Texto do fato
    public TextMeshProUGUI questionText; // Texto da pergunta
    public Button[] answerButtons; // Botões de resposta
    public AudioSource audioSource; // AudioSource para tocar os sons
    public AudioClip correctSound; // Som de acerto
    public AudioClip incorrectSound; // Som de erro
    public GameObject factPanel; // Painel do fato
    public GameObject questionPanel; // Painel da pergunta
    public TextMeshProUGUI themeText;
    public GameObject Canva;

    [SerializeField] private GhostText ghostText;
    public PlayerMovement playerMovement;
    public GameObject book1;
    public GameObject player;
    private int currentFactIndex;
    private List<int> factIndexes;

    private string[] scienceFacts = {
        "A gravidade é a força que atrai objetos uns aos outros e é responsável por manter planetas em órbita ao redor do Sol.",
        "A camada de ozônio na estratosfera protege a Terra dos raios ultravioletas prejudiciais, absorvendo a maior parte dessa radiação.",
        "A teoria das placas tectônicas sugere que a crosta terrestre é dividida em placas que se movem, causando fenômenos como terremotos e a formação de montanhas.",
        "As células são as unidades básicas de vida e compõem todos os organismos vivos, sejam eles unicelulares ou multicelulares.",
        "A evolução é o processo pelo qual diferentes espécies se desenvolvem e se diversificam ao longo do tempo, principalmente através da seleção natural.",
        "A fotossíntese é o processo pelo qual plantas e outros organismos convertem luz solar, água e dióxido de carbono em oxigênio e glicose, essenciais para a vida na Terra."
    };

    private string[] scienceQuestions = {
        "O que é a gravidade e qual sua função no sistema solar?",
        "Qual é a função da camada de ozônio na atmosfera terrestre?",
        "Qual é a teoria das placas tectônicas e quais fenômenos ela ajuda a explicar?",
        "Qual é a função das células nos organismos vivos?",
        "Como funciona o processo de evolução?",
        "Para que serve a fotossíntese nas plantas?"
    };

    private string[] correctAnswers = {
        "É a força que atrai os objetos uns aos outros e mantém os planetas em órbita ao redor do Sol.",
        "Absorver a maior parte da radiação ultravioleta prejudicial, protegendo a Terra.",
        "Que a crosta terrestre é dividida em placas que se movem, causando terremotos e a formação de montanhas.",
        "Servem como unidades básicas de vida, permitindo a formação e o funcionamento dos organismos.",
        "É o processo de diversificação das espécies através da seleção natural ao longo do tempo.",
        "Permite que as plantas produzam energia e oxigênio, essenciais para a vida na Terra."
    };

    private string[][] incorrectAnswers = new string[][] {
        new string[] { "É uma força que apenas afeta objetos grandes como planetas e estrelas.", "É uma força que repele objetos uns dos outros no espaço." },
        new string[] { "Reflete a radiação solar para o espaço, regulando o clima.", "Diminui a quantidade de oxigênio na atmosfera." },
        new string[] { "Que os continentes são imóveis e fixos na crosta terrestre.", "Que a superfície terrestre é completamente sólida e inquebrável." },
        new string[] { "Somente armazenam informações genéticas sem realizar nenhuma função.", "Atuam apenas na reprodução dos organismos." },
        new string[] { "É um processo que ocorre em dias de chuva e influencia o comportamento animal.", "Um ciclo biológico que apenas afeta os animais." },
        new string[] { "Produz apenas glicose, mas não tem efeito no oxigênio.", "É apenas um processo de absorção de luz solar sem outra função." }
    };

    private void Start()
    {
        InitializeFactIndexes();
        DisplayRandomFact();
    }

    private void InitializeFactIndexes()
    {
        factIndexes = new List<int> { 0, 1, 2, 3, 4, 5 }; // Índices para controle de exibição de fatos/perguntas
    }

    private void DisplayRandomFact()
    {
        if (factIndexes.Count == 0)
        {
            InitializeFactIndexes(); // Reinicia a lista se todas as perguntas foram feitas
        }

        currentFactIndex = factIndexes[Random.Range(0, factIndexes.Count)];
        themeText.text = "Ciência";
        factText.text = scienceFacts[currentFactIndex]; // Atualiza o texto do fato
        factIndexes.Remove(currentFactIndex); // Remove o índice exibido

        factPanel.SetActive(true);
        questionPanel.SetActive(false);
    }

    public void OnNextButtonClicked()
    {
        factPanel.SetActive(false);
        questionPanel.SetActive(true);

        questionText.text = scienceQuestions[currentFactIndex];
        SetupAnswers();
    }

    private void SetupAnswers()
    {
        // Obter a resposta correta
        string correctAnswer = correctAnswers[currentFactIndex];

        // Obter as respostas incorretas
        string[] incorrectAnswersArray = incorrectAnswers[currentFactIndex];

        // Criar lista para todas as respostas e adicionar a correta em posição aleatória
        List<string> allAnswers = new List<string>(incorrectAnswersArray);
        int correctAnswerPosition = Random.Range(0, allAnswers.Count + 1);
        allAnswers.Insert(correctAnswerPosition, correctAnswer);

        // Configurar botões de resposta
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
                player.transform.position = new Vector3(-39, 9.531775f, player.transform.position.z); // Alterar posição
                Debug.Log("Jogador reposicionado para a posição (87.2, -682).");
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
                bookCollider.enabled = false; // Desativa o Box Collider 2D
                Debug.Log($"Box Collider 2D do livro {book.name} desativado.");
            }
            else
            {
                Debug.LogWarning($"Box Collider 2D não encontrado no livro {book.name}.");
            }
            if (bookRigidbody != null)
            {
                bookRigidbody.bodyType = RigidbodyType2D.Dynamic; // Set body type to Dynamic
                bookRigidbody.gravityScale = 10;
                Debug.Log($"Rigidbody2D of the book {book.name} set to Dynamic.");
            }
            else
            {
                Debug.LogWarning($"Rigidbody2D not found on the book {book.name}.");
            }
            Destroy(book, 5);
        }
        else
        {
            Debug.LogWarning($"Livro não encontrado.");
        }
    }

}