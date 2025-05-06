using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class HistoryFinalManager : MonoBehaviour
{
    public TextMeshProUGUI questionText; // Question text
    public Button[] answerButtons; // Answer buttons
    public AudioSource audioSource; // AudioSource for response sounds
    public AudioClip correctSound; // Correct answer sound
    public AudioClip incorrectSound; // Incorrect answer sound
    public GameObject quizPanel; // Panel that displays the question and options
    public GameObject victoryPanel; // Victory panel
    public TextMeshProUGUI HistoryPointsText; // Text for history points

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

    private string[] correctAnswersPT = {
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

    private string[][] incorrectAnswersPT = new string[][] {
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

    private string[] questionsEN = {
        "What was the cultural impact of the Renaissance in Europe?",
        "What were some of the main consequences of the French Revolution?",
        "What was the central cause of the American Civil War?",
        "What was the main result of World War I for the European political map?",
        "What was the main goal of the Age of Exploration?",
        "Why was the United Nations created in 1945?",
        "What was the impact of the Industrial Revolution on the world?",
        "What did the signing of the Declaration of Independence represent?",
        "What were the main consequences of World War II?",
        "What was the Treaty of Versailles and what was its impact on Germany?",
        "What was the most enduring legacy of the Roman Empire?"
    };

    private string[] correctAnswersEN = {
        "Revival of the arts and sciences and strengthening of humanist thought.",
        "Overthrow of the monarchy and establishment of principles of equality and liberty.",
        "Conflict between Northern and Southern states over slavery and preserving the Union.",
        "Significant changes in European borders and the weakening of traditional empires.",
        "Discovering new routes and territories to expand trade and European influence.",
        "Promote peace and international cooperation after World War II.",
        "Transformed the global economy, promoting urbanization, innovation, and the rise of factories.",
        "Official separation of the Thirteen Colonies from British rule and the formation of an independent nation.",
        "Division of the world between ideological blocs and reconstruction of war-torn countries.",
        "Imposition of severe conditions on Germany, leading to future political tensions.",
        "Legacy in areas like law, engineering, architecture, and culture that still influence today."
    };

    private string[][] incorrectAnswersEN = new string[][] {
        new string[] { "Decline of interest in sciences and arts, with a sole focus on religion.", "Expansion of absolute monarchy in Europe." },
        new string[] { "Strengthening of monarchy in France.", "Creation of an alliance between France and England against Austria." },
        new string[] { "Territorial issues with Mexico.", "Religious conflict between Northern and Southern states in the US." },
        new string[] { "Complete unification of European countries under a single government.", "Expansion of the German Empire over all of Western Europe." },
        new string[] { "Exploration of gold mines in the African continent.", "Conquest and colonization of Asia exclusively." },
        new string[] { "Promote division of European colonial territories.", "Organize international trade and global economic exploitation." },
        new string[] { "Reduced technological innovation and increased reliance on manual labor.", "Only impacted Europe, without influencing other parts of the world." },
        new string[] { "Submission of colonies to British interests.", "Start of a new alliance between the Thirteen Colonies and England." },
        new string[] { "Immediate unification of all nations under a global government.", "Strengthening of European empires worldwide." },
        new string[] { "Restoration of the German monarchy after the war.", "Alliance between France and Germany for economic reconstruction." },
        new string[] { "Limited impact on the Italian peninsula, with no influence on other civilizations.", "Only military conquests, without cultural or legal legacy." }
    };

    private void Start()
    {
        // Get current language from GameData
        language = GameData.language;

        // Update the history points display
        HistoryPointsText.text = GameData.historyPoints.ToString();

        StartQuiz();
    }

    private void StartQuiz()
    {
        // Initialize the quiz with all the questions
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

            GameData.historyPoints++;
            HistoryPointsText.text = GameData.historyPoints.ToString();

            if (correctAnswersCount >= 3)
            {
                GameData.historyPoints += 3;
                HistoryPointsText.text = GameData.historyPoints.ToString();
                playerMovement.collidedStop = false;
                DisableBookCollider(book1);
                finalCanvas.SetActive(false);
                return;
            }
        }
        else
        {
            audioSource.PlayOneShot(incorrectSound);

            if (GameData.historyPoints > 1)
            {
                GameData.historyPoints -= 2;
            }
            else
            {
                GameData.historyPoints = 0;
            }
            HistoryPointsText.text = GameData.historyPoints.ToString();

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
            Debug.LogWarning("Book not found.");
        }
    }
}