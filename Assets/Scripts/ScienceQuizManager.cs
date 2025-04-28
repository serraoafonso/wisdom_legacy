using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

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
    public TextMeshProUGUI sciencePointsText;
    public GameObject Canva;

    [SerializeField] private GhostText ghostText;
    public PlayerMovement playerMovement;
    public GameObject book1;
    public GameObject book2; // Referência ao objeto do livro
    public GameObject book3; // Referência ao objeto do livro
    public GameObject book4; // Referência ao objeto do livro
    public GameObject player;
    private int currentFactIndex;
    private List<int> factIndexes;

    private string[] scienceFacts = {
    "A gravidade é a força que atrai objetos uns aos outros e é responsável por manter planetas em órbita ao redor do Sol.",
    "A camada de ozônio na estratosfera protege a Terra dos raios ultravioletas prejudiciais, absorvendo a maior parte dessa radiação.",
    "A teoria das placas tectônicas sugere que a crosta terrestre é dividida em placas que se movem, causando fenômenos como terremotos e a formação de montanhas.",
    "As células são as unidades básicas de vida e compõem todos os organismos vivos, sejam eles unicelulares ou multicelulares.",
    "A evolução é o processo pelo qual diferentes espécies se desenvolvem e se diversificam ao longo do tempo, principalmente através da seleção natural.",
    "A fotossíntese é o processo pelo qual plantas e outros organismos convertem luz solar, água e dióxido de carbono em oxigênio e glicose, essenciais para a vida na Terra.",
    "O DNA é o material genético encontrado em células, que contém as instruções para o desenvolvimento e funcionamento de todos os organismos vivos.",
    "A água cobre cerca de 71% da superfície da Terra e é essencial para a sobrevivência de todos os seres vivos.",
    "Os buracos negros são regiões no espaço onde a gravidade é tão forte que nem mesmo a luz pode escapar.",
    "Os vírus são organismos microscópicos que dependem de hospedeiros vivos para se replicar, causando diversas doenças.",
    "O efeito estufa é um fenômeno natural que mantém a Terra aquecida, mas sua intensificação pode levar a mudanças climáticas."
};

    private string[] scienceQuestions = {
    "O que é a gravidade e qual sua função no sistema solar?",
    "Qual é a função da camada de ozônio na atmosfera terrestre?",
    "Qual é a teoria das placas tectônicas e quais fenômenos ela ajuda a explicar?",
    "Qual é a função das células nos organismos vivos?",
    "Como funciona o processo de evolução?",
    "Para que serve a fotossíntese nas plantas?",
    "O que é o DNA e qual a sua função nos organismos vivos?",
    "Por que a água é essencial para a vida na Terra?",
    "O que são buracos negros e quais são suas características?",
    "Como os vírus se replicam e qual seu impacto na saúde?",
    "O que é o efeito estufa e como ele influencia o clima da Terra?"
};

    private string[] correctAnswers = {
    "É a força que atrai os objetos uns aos outros e mantém os planetas em órbita ao redor do Sol.",
    "Absorver a maior parte da radiação ultravioleta prejudicial, protegendo a Terra.",
    "Que a crosta terrestre é dividida em placas que se movem, causando terremotos e a formação de montanhas.",
    "Servem como unidades básicas de vida, permitindo a formação e o funcionamento dos organismos.",
    "É o processo de diversificação das espécies através da seleção natural ao longo do tempo.",
    "Permite que as plantas produzam energia e oxigênio, essenciais para a vida na Terra.",
    "Contém as instruções genéticas que regulam o desenvolvimento e o funcionamento dos organismos vivos.",
    "É essencial para processos biológicos como hidratação celular e transporte de nutrientes.",
    "São regiões com gravidade tão forte que nem a luz pode escapar, deformando o espaço ao seu redor.",
    "Dependem de hospedeiros vivos para se replicar, causando doenças ao invadir células hospedeiras.",
    "É um fenômeno que mantém a Terra aquecida, mas sua intensificação pode levar a mudanças climáticas."
};

    private string[][] incorrectAnswers = new string[][] {
    new string[] { "É uma força que apenas afeta objetos grandes como planetas e estrelas.", "É uma força que repele objetos uns dos outros no espaço." },
    new string[] { "Reflete a radiação solar para o espaço, regulando o clima.", "Diminui a quantidade de oxigênio na atmosfera." },
    new string[] { "Que os continentes são imóveis e fixos na crosta terrestre.", "Que a superfície terrestre é completamente sólida e inquebrável." },
    new string[] { "Somente armazenam informações genéticas sem realizar nenhuma função.", "Atuam apenas na reprodução dos organismos." },
    new string[] { "É um processo que ocorre em dias de chuva e influencia o comportamento animal.", "Um ciclo biológico que apenas afeta os animais." },
    new string[] { "Produz apenas glicose, mas não tem efeito no oxigênio.", "É apenas um processo de absorção de luz solar sem outra função." },
    new string[] { "É uma molécula que serve apenas como fonte de energia para células.", "Atua exclusivamente na reprodução de organismos multicelulares." },
    new string[] { "É importante apenas para a formação de oceanos e rios.", "Apenas organismos aquáticos precisam dela para sobreviver." },
    new string[] { "São estrelas em colapso que brilham intensamente.", "São buracos que liberam luz e matéria no espaço." },
    new string[] { "Se replicam sozinhos sem precisar de células hospedeiras.", "Atuam apenas de forma benéfica para os organismos vivos." },
    new string[] { "Aumenta a quantidade de oxigênio na atmosfera.", "Não tem impacto direto na temperatura da Terra." }
};


    private void Start()
    {
        audioSource.clip = null;
        InitializeFactIndexes();
        DisplayRandomFact();

    }

    private void InitializeFactIndexes()
    {
        factIndexes = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; // Índices para controle de exibição de fatos/perguntas
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

        // Certifique-se de que temos o mesmo número de botões que respostas
        if (answerButtons.Length < allAnswers.Count)
        {
            Debug.LogError("O número de botões é menor que o número de respostas disponíveis!");
            return;
        }

        // Configurar botões de resposta
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < allAnswers.Count)
            {
                int buttonIndex = i; // Captura o índice localmente para evitar problemas com closures

                // Configurar o botão com o texto da resposta
                answerButtons[buttonIndex].GetComponentInChildren<TextMeshProUGUI>().text = allAnswers[buttonIndex];
                answerButtons[buttonIndex].onClick.RemoveAllListeners();

                bool isCorrect = (buttonIndex == correctAnswerPosition);
                answerButtons[buttonIndex].onClick.AddListener(() => AnswerSelected(isCorrect, answerButtons[buttonIndex].gameObject));

                answerButtons[buttonIndex].gameObject.SetActive(true); // Certifique-se de ativar o botão
            }
            else
            {
                // Desativar botões extras
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

            GameData.sciencePoints++;
            sciencePointsText.text = GameData.sciencePoints.ToString();
            // Mantém o Canvas ativo por um tempo para mostrar a cor/som
            StartCoroutine(HandleAnswerFeedback(true, botao));
        }
        else
        {
            botao.GetComponent<Image>().color = Color.red; // Indica resposta incorreta
            ghostText.HandleMiss();
            Debug.Log("Resposta incorreta!");
            audioSource.clip = incorrectSound;

            if (GameData.sciencePoints > 0)
            {
                GameData.sciencePoints--;

            }
            else
            {
                GameData.sciencePoints = 0;
            }

            sciencePointsText.text = GameData.sciencePoints.ToString();

            if (player != null)
            {
                // Reposiciona o jogador com base na posição Y
                if (playerPosition.y < 10)
                {
                    player.transform.position = new Vector3(-39, 9.531775f, player.transform.position.z);
                    Debug.Log("Jogador reposicionado para a posição inicial.");
                }
                else
                {
                    player.transform.position = new Vector3(-130, 79, player.transform.position.z);
                    Debug.Log("Jogador reposicionado para a posição inicial.");
                }
            }

            // Mantém o Canvas ativo por um tempo para mostrar a cor/som
            StartCoroutine(HandleAnswerFeedback(false, botao));
        }

        audioSource.Play();
    }

    // Corrotina para atrasar o fechamento do Canvas
    private IEnumerator HandleAnswerFeedback(bool isCorrect, GameObject botao)
    {
        // Aguarda 1 segundo para mostrar a cor e tocar o som
        yield return new WaitForSeconds(0.2f);

        // Reseta a cor do botão para a cor padrão
        botao.GetComponent<Image>().color = Color.white;

        // Fecha o Canvas após o feedback
        Canva.SetActive(false);
        playerMovement.collidedStop = false;

        // Exibe um novo fato apenas após o feedback
        DisplayRandomFact();
    }


    private GameObject DetermineBookBasedOnPlayerPosition()
    {
        Vector3 playerPosition = player.transform.position;

        if (playerPosition.y < 10)
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
            else
            {
                Debug.LogWarning($"Box Collider 2D não encontrado no livro {book.name}.");
            }

            if (bookRigidbody != null)
            {
                bookRigidbody.bodyType = RigidbodyType2D.Dynamic;
                bookRigidbody.gravityScale = 10;
                Debug.Log($"Rigidbody2D do livro {book.name} definido como Dinâmico.");
            }
            else
            {
                Debug.LogWarning($"Rigidbody2D não encontrado no livro {book.name}.");
            }

            Destroy(book, 5);
        }
        else
        {
            Debug.LogWarning($"Livro não encontrado.");
        }
    }
}
