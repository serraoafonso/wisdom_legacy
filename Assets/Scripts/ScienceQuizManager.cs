using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

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
    public TextMeshProUGUI sciencePointsText;
    public GameObject Canva;

    [SerializeField] private GhostText ghostText;
    public PlayerMovement playerMovement;
    public GameObject book1;
    public GameObject book2; // Refer�ncia ao objeto do livro
    public GameObject book3; // Refer�ncia ao objeto do livro
    public GameObject book4; // Refer�ncia ao objeto do livro
    public GameObject player;
    private int currentFactIndex;
    private List<int> factIndexes;

    private string[] scienceFacts = {
    "A gravidade � a for�a que atrai objetos uns aos outros e � respons�vel por manter planetas em �rbita ao redor do Sol.",
    "A camada de oz�nio na estratosfera protege a Terra dos raios ultravioletas prejudiciais, absorvendo a maior parte dessa radia��o.",
    "A teoria das placas tect�nicas sugere que a crosta terrestre � dividida em placas que se movem, causando fen�menos como terremotos e a forma��o de montanhas.",
    "As c�lulas s�o as unidades b�sicas de vida e comp�em todos os organismos vivos, sejam eles unicelulares ou multicelulares.",
    "A evolu��o � o processo pelo qual diferentes esp�cies se desenvolvem e se diversificam ao longo do tempo, principalmente atrav�s da sele��o natural.",
    "A fotoss�ntese � o processo pelo qual plantas e outros organismos convertem luz solar, �gua e di�xido de carbono em oxig�nio e glicose, essenciais para a vida na Terra.",
    "O DNA � o material gen�tico encontrado em c�lulas, que cont�m as instru��es para o desenvolvimento e funcionamento de todos os organismos vivos.",
    "A �gua cobre cerca de 71% da superf�cie da Terra e � essencial para a sobreviv�ncia de todos os seres vivos.",
    "Os buracos negros s�o regi�es no espa�o onde a gravidade � t�o forte que nem mesmo a luz pode escapar.",
    "Os v�rus s�o organismos microsc�picos que dependem de hospedeiros vivos para se replicar, causando diversas doen�as.",
    "O efeito estufa � um fen�meno natural que mant�m a Terra aquecida, mas sua intensifica��o pode levar a mudan�as clim�ticas."
};

    private string[] scienceQuestions = {
    "O que � a gravidade e qual sua fun��o no sistema solar?",
    "Qual � a fun��o da camada de oz�nio na atmosfera terrestre?",
    "Qual � a teoria das placas tect�nicas e quais fen�menos ela ajuda a explicar?",
    "Qual � a fun��o das c�lulas nos organismos vivos?",
    "Como funciona o processo de evolu��o?",
    "Para que serve a fotoss�ntese nas plantas?",
    "O que � o DNA e qual a sua fun��o nos organismos vivos?",
    "Por que a �gua � essencial para a vida na Terra?",
    "O que s�o buracos negros e quais s�o suas caracter�sticas?",
    "Como os v�rus se replicam e qual seu impacto na sa�de?",
    "O que � o efeito estufa e como ele influencia o clima da Terra?"
};

    private string[] correctAnswers = {
    "� a for�a que atrai os objetos uns aos outros e mant�m os planetas em �rbita ao redor do Sol.",
    "Absorver a maior parte da radia��o ultravioleta prejudicial, protegendo a Terra.",
    "Que a crosta terrestre � dividida em placas que se movem, causando terremotos e a forma��o de montanhas.",
    "Servem como unidades b�sicas de vida, permitindo a forma��o e o funcionamento dos organismos.",
    "� o processo de diversifica��o das esp�cies atrav�s da sele��o natural ao longo do tempo.",
    "Permite que as plantas produzam energia e oxig�nio, essenciais para a vida na Terra.",
    "Cont�m as instru��es gen�ticas que regulam o desenvolvimento e o funcionamento dos organismos vivos.",
    "� essencial para processos biol�gicos como hidrata��o celular e transporte de nutrientes.",
    "S�o regi�es com gravidade t�o forte que nem a luz pode escapar, deformando o espa�o ao seu redor.",
    "Dependem de hospedeiros vivos para se replicar, causando doen�as ao invadir c�lulas hospedeiras.",
    "� um fen�meno que mant�m a Terra aquecida, mas sua intensifica��o pode levar a mudan�as clim�ticas."
};

    private string[][] incorrectAnswers = new string[][] {
    new string[] { "� uma for�a que apenas afeta objetos grandes como planetas e estrelas.", "� uma for�a que repele objetos uns dos outros no espa�o." },
    new string[] { "Reflete a radia��o solar para o espa�o, regulando o clima.", "Diminui a quantidade de oxig�nio na atmosfera." },
    new string[] { "Que os continentes s�o im�veis e fixos na crosta terrestre.", "Que a superf�cie terrestre � completamente s�lida e inquebr�vel." },
    new string[] { "Somente armazenam informa��es gen�ticas sem realizar nenhuma fun��o.", "Atuam apenas na reprodu��o dos organismos." },
    new string[] { "� um processo que ocorre em dias de chuva e influencia o comportamento animal.", "Um ciclo biol�gico que apenas afeta os animais." },
    new string[] { "Produz apenas glicose, mas n�o tem efeito no oxig�nio.", "� apenas um processo de absor��o de luz solar sem outra fun��o." },
    new string[] { "� uma mol�cula que serve apenas como fonte de energia para c�lulas.", "Atua exclusivamente na reprodu��o de organismos multicelulares." },
    new string[] { "� importante apenas para a forma��o de oceanos e rios.", "Apenas organismos aqu�ticos precisam dela para sobreviver." },
    new string[] { "S�o estrelas em colapso que brilham intensamente.", "S�o buracos que liberam luz e mat�ria no espa�o." },
    new string[] { "Se replicam sozinhos sem precisar de c�lulas hospedeiras.", "Atuam apenas de forma ben�fica para os organismos vivos." },
    new string[] { "Aumenta a quantidade de oxig�nio na atmosfera.", "N�o tem impacto direto na temperatura da Terra." }
};


    private void Start()
    {
        audioSource.clip = null;
        InitializeFactIndexes();
        DisplayRandomFact();

    }

    private void InitializeFactIndexes()
    {
        factIndexes = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; // �ndices para controle de exibi��o de fatos/perguntas
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

            GameData.sciencePoints++;
            sciencePointsText.text = GameData.sciencePoints.ToString();
            // Mant�m o Canvas ativo por um tempo para mostrar a cor/som
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
                // Reposiciona o jogador com base na posi��o Y
                if (playerPosition.y < 10)
                {
                    player.transform.position = new Vector3(-39, 9.531775f, player.transform.position.z);
                    Debug.Log("Jogador reposicionado para a posi��o inicial.");
                }
                else
                {
                    player.transform.position = new Vector3(-130, 79, player.transform.position.z);
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
                Debug.LogWarning($"Box Collider 2D n�o encontrado no livro {book.name}.");
            }

            if (bookRigidbody != null)
            {
                bookRigidbody.bodyType = RigidbodyType2D.Dynamic;
                bookRigidbody.gravityScale = 10;
                Debug.Log($"Rigidbody2D do livro {book.name} definido como Din�mico.");
            }
            else
            {
                Debug.LogWarning($"Rigidbody2D n�o encontrado no livro {book.name}.");
            }

            Destroy(book, 5);
        }
        else
        {
            Debug.LogWarning($"Livro n�o encontrado.");
        }
    }
}
