using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ScienceQuizManager : MonoBehaviour
{
    public TextMeshProUGUI factText; // Texto do fato
    public TextMeshProUGUI questionText; // Texto da pergunta
    public Button[] answerButtons; // Bot�es de resposta
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
        "A gravidade � a for�a que atrai objetos uns aos outros e � respons�vel por manter planetas em �rbita ao redor do Sol.",
        "A camada de oz�nio na estratosfera protege a Terra dos raios ultravioletas prejudiciais, absorvendo a maior parte dessa radia��o.",
        "A teoria das placas tect�nicas sugere que a crosta terrestre � dividida em placas que se movem, causando fen�menos como terremotos e a forma��o de montanhas.",
        "As c�lulas s�o as unidades b�sicas de vida e comp�em todos os organismos vivos, sejam eles unicelulares ou multicelulares.",
        "A evolu��o � o processo pelo qual diferentes esp�cies se desenvolvem e se diversificam ao longo do tempo, principalmente atrav�s da sele��o natural.",
        "A fotoss�ntese � o processo pelo qual plantas e outros organismos convertem luz solar, �gua e di�xido de carbono em oxig�nio e glicose, essenciais para a vida na Terra."
    };

    private string[] scienceQuestions = {
        "O que � a gravidade e qual sua fun��o no sistema solar?",
        "Qual � a fun��o da camada de oz�nio na atmosfera terrestre?",
        "Qual � a teoria das placas tect�nicas e quais fen�menos ela ajuda a explicar?",
        "Qual � a fun��o das c�lulas nos organismos vivos?",
        "Como funciona o processo de evolu��o?",
        "Para que serve a fotoss�ntese nas plantas?"
    };

    private string[] correctAnswers = {
        "� a for�a que atrai os objetos uns aos outros e mant�m os planetas em �rbita ao redor do Sol.",
        "Absorver a maior parte da radia��o ultravioleta prejudicial, protegendo a Terra.",
        "Que a crosta terrestre � dividida em placas que se movem, causando terremotos e a forma��o de montanhas.",
        "Servem como unidades b�sicas de vida, permitindo a forma��o e o funcionamento dos organismos.",
        "� o processo de diversifica��o das esp�cies atrav�s da sele��o natural ao longo do tempo.",
        "Permite que as plantas produzam energia e oxig�nio, essenciais para a vida na Terra."
    };

    private string[][] incorrectAnswers = new string[][] {
        new string[] { "� uma for�a que apenas afeta objetos grandes como planetas e estrelas.", "� uma for�a que repele objetos uns dos outros no espa�o." },
        new string[] { "Reflete a radia��o solar para o espa�o, regulando o clima.", "Diminui a quantidade de oxig�nio na atmosfera." },
        new string[] { "Que os continentes s�o im�veis e fixos na crosta terrestre.", "Que a superf�cie terrestre � completamente s�lida e inquebr�vel." },
        new string[] { "Somente armazenam informa��es gen�ticas sem realizar nenhuma fun��o.", "Atuam apenas na reprodu��o dos organismos." },
        new string[] { "� um processo que ocorre em dias de chuva e influencia o comportamento animal.", "Um ciclo biol�gico que apenas afeta os animais." },
        new string[] { "Produz apenas glicose, mas n�o tem efeito no oxig�nio.", "� apenas um processo de absor��o de luz solar sem outra fun��o." }
    };

    private void Start()
    {
        InitializeFactIndexes();
        DisplayRandomFact();
    }

    private void InitializeFactIndexes()
    {
        factIndexes = new List<int> { 0, 1, 2, 3, 4, 5 }; // �ndices para controle de exibi��o de fatos/perguntas
    }

    private void DisplayRandomFact()
    {
        if (factIndexes.Count == 0)
        {
            InitializeFactIndexes(); // Reinicia a lista se todas as perguntas foram feitas
        }

        currentFactIndex = factIndexes[Random.Range(0, factIndexes.Count)];
        themeText.text = "Ci�ncia";
        factText.text = scienceFacts[currentFactIndex]; // Atualiza o texto do fato
        factIndexes.Remove(currentFactIndex); // Remove o �ndice exibido

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

        // Criar lista para todas as respostas e adicionar a correta em posi��o aleat�ria
        List<string> allAnswers = new List<string>(incorrectAnswersArray);
        int correctAnswerPosition = Random.Range(0, allAnswers.Count + 1);
        allAnswers.Insert(correctAnswerPosition, correctAnswer);

        // Configurar bot�es de resposta
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
                player.transform.position = new Vector3(-39, 9.531775f, player.transform.position.z); // Alterar posi��o
                Debug.Log("Jogador reposicionado para a posi��o (87.2, -682).");
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
                Debug.LogWarning($"Box Collider 2D n�o encontrado no livro {book.name}.");
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
            Debug.LogWarning($"Livro n�o encontrado.");
        }
    }

}