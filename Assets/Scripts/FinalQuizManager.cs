using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class FinalQuizManager : MonoBehaviour
{
    public TextMeshProUGUI questionText; // Question text
    public Button[] answerButtons; // Answer buttons
    public AudioSource audioSource; // AudioSource for response sounds
    public AudioClip correctSound; // Correct answer sound
    public AudioClip incorrectSound; // Incorrect answer sound
    public GameObject quizPanel; // Panel that displays the question and options
    public GameObject victoryPanel; // Victory panel

    public GameObject finalCanvas;
    public GameObject book1; // Reference to the book object
    public PlayerMovement playerMovement;
    private int correctAnswersCount; // Consecutive correct answers count
    private int currentQuestionIndex;
    private List<int> questionIndexes;

    // Language-dependent quiz data
    private string language;

    // Data for questions and answers in both languages
    private string[] questionsPT = {
        "O que � a gravidade e qual sua fun��o no sistema solar?",
        "Por que � importante planejar a aposentadoria desde cedo?",
        "Qual foi o impacto cultural do Renascimento na Europa?",
        "Qual � a fun��o das c�lulas nos organismos vivos?",
        "Qual � uma das principais desvantagens do cart�o de cr�dito?",
        "Qual foi a causa central da Guerra Civil Americana?",
        "Para que serve o IRS (Imposto sobre o Rendimento das Pessoas Singulares) em Portugal?",
        "Qual � o principal resultado da Primeira Guerra Mundial para o mapa pol�tico europeu?"
    };

    private string[] correctAnswersPT = {
        "� a for�a que atrai os objetos uns aos outros e mant�m os planetas em �rbita ao redor do Sol.",
        "Para garantir uma fonte de renda no futuro e manter o padr�o de vida desejado.",
        "Ressurgimento das artes e ci�ncias e fortalecimento do pensamento humanista.",
        "Servem como unidades b�sicas de vida, permitindo a forma��o e o funcionamento dos organismos.",
        "Juros elevados em caso de atrasos, podendo gerar d�vidas dif�ceis de quitar.",
        "Conflito entre estados do Norte e do Sul sobre a quest�o da escravid�o e a preserva��o da Uni�o.",
        "� um tributo obrigat�rio que financia servi�os p�blicos e funciona conforme a faixa de renda anual da pessoa.",
        "Mudan�as significativas nas fronteiras europeias e o enfraquecimento de imp�rios tradicionais."
    };

    private string[][] incorrectAnswersPT = new string[][] {
        new string[] { "� uma for�a que apenas afeta objetos grandes como planetas e estrelas.", "� uma for�a que repele objetos uns dos outros no espa�o." },
        new string[] { "Para aumentar a quantidade de gastos na juventude.", "Para garantir benef�cios como f�rias prolongadas na aposentadoria." },
        new string[] { "Decl�nio do interesse em ci�ncias e artes, com foco exclusivo em religi�o.", "Expans�o da monarquia absoluta na Europa." },
        new string[] { "Somente armazenam informa��es gen�ticas sem realizar nenhuma fun��o.", "Atuam apenas na reprodu��o dos organismos." },
        new string[] { "Possibilidade de acumular recompensas de fidelidade.", "Melhorar o cr�dito sem custos extras, mesmo com atrasos." },
        new string[] { "Quest�es territoriais com o M�xico.", "Conflito religioso entre estados do Norte e do Sul dos EUA." },
        new string[] { "Para diminuir os custos de bens e servi�os pessoais.", "Para ajudar exclusivamente no financiamento de d�vidas pessoais." },
        new string[] { "Unifica��o completa dos pa�ses europeus sob um governo �nico.", "Expans�o do imp�rio alem�o sobre toda a Europa Ocidental." }
    };

    private string[] questionsEN = {
        "What is gravity and what is its function in the solar system?",
        "Why is it important to plan retirement early?",
        "What was the cultural impact of the Renaissance in Europe?",
        "What is the function of cells in living organisms?",
        "What is one of the main disadvantages of credit cards?",
        "What was the central cause of the American Civil War?",
        "What is the IRS (Individual Income Tax) in Portugal for?",
        "What is the main result of World War I on the European political map?"
    };

    private string[] correctAnswersEN = {
        "It is the force that attracts objects to each other and keeps planets in orbit around the Sun.",
        "To ensure a source of income in the future and maintain the desired standard of living.",
        "A resurgence of the arts and sciences and the strengthening of humanist thinking.",
        "They serve as basic units of life, enabling the formation and functioning of organisms.",
        "High interest rates in case of delays, which can lead to difficult-to-repay debts.",
        "A conflict between the Northern and Southern states over the issue of slavery and the preservation of the Union.",
        "It is a mandatory tax that funds public services and operates according to a person's annual income bracket.",
        "Significant changes in European borders and the weakening of traditional empires."
    };

    private string[][] incorrectAnswersEN = new string[][] {
        new string[] { "It is a force that only affects large objects like planets and stars.", "It is a force that repels objects from each other in space." },
        new string[] { "To increase the amount of spending in youth.", "To ensure benefits like extended vacations in retirement." },
        new string[] { "Decline in interest in science and arts, with a focus solely on religion.", "Expansion of absolute monarchy in Europe." },
        new string[] { "They only store genetic information without performing any function.", "They act only in the reproduction of organisms." },
        new string[] { "Possibility of accumulating loyalty rewards.", "Improving credit without extra costs, even with delays." },
        new string[] { "Territorial issues with Mexico.", "Religious conflict between the Northern and Southern states of the USA." },
        new string[] { "To reduce the costs of personal goods and services.", "To help exclusively in financing personal debts." },
        new string[] { "Complete unification of European countries under a single government.", "Expansion of the German empire over all of Western Europe." }
    };

    private void Start()
    {
        // Assume gameData.language is set somewhere in the game
        language = GameData.language;

        StartQuiz();
    }

    private void StartQuiz()
    {
        // Initialize the quiz with all the questions again
        questionIndexes = new List<int>();
        for (int i = 0; i < GetQuestions().Length; i++)
        {
            questionIndexes.Add(i);
        }
        ShuffleQuestions();
        correctAnswersCount = 0; // Reset consecutive correct answers counter
        LoadQuestion();
    }

    private string[] GetQuestions()
    {
        return language == "en" ? questionsEN : questionsPT;
    }

    private string[] GetCorrectAnswers()
    {
        return language == "en" ? correctAnswersEN : correctAnswersPT;
    }

    private string[][] GetIncorrectAnswers()
    {
        return language == "en" ? incorrectAnswersEN : incorrectAnswersPT;
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

        questionText.text = GetQuestions()[currentQuestionIndex];
        List<string> answerOptions = new List<string>(GetIncorrectAnswers()[currentQuestionIndex]);
        answerOptions.Add(GetCorrectAnswers()[currentQuestionIndex]);
        ShuffleList(answerOptions); // Shuffle the answers

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answerOptions[i];
            answerButtons[i].onClick.RemoveAllListeners();
            bool isCorrect = answerOptions[i] == GetCorrectAnswers()[currentQuestionIndex];
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
            if (correctAnswersCount >= 3)
            {
                playerMovement.collidedStop = false;
                DisableBookCollider(book1);
                finalCanvas.SetActive(false);
                return;
            }
        }
        else
        {
            audioSource.PlayOneShot(incorrectSound);
            correctAnswersCount = 0; // Reset the count of consecutive correct answers
            StartQuiz(); // Restart the quiz
            return;
        }
        LoadQuestion();
    }

    private void Victory()
    {
        quizPanel.SetActive(false);
        victoryPanel.SetActive(true);
    }

    private void DisableBookCollider(GameObject book)
    {
        if (book != null)
        {
            BoxCollider2D bookCollider = book.GetComponent<BoxCollider2D>();

            Rigidbody2D bookRigidbody = book.GetComponent<Rigidbody2D>();

            if (bookCollider != null)
            {
                bookCollider.enabled = false; // Disable the Box Collider 2D
                Debug.Log($"Box Collider 2D of the book {book.name} disabled.");
            }
            else
            {
                Debug.LogWarning($"Box Collider 2D not found on the book {book.name}.");
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
            Debug.LogWarning($"Book not found.");
        }
    }
}
