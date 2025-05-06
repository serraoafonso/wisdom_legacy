using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class HistoryQuizManager : MonoBehaviour
{
    public TextMeshProUGUI factText;
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip incorrectSound;
    public GameObject factPanel;
    public GameObject questionPanel;
    public TextMeshProUGUI themeText;
    public TextMeshProUGUI HistoryPointsText;
    public GameObject Canva;

    [SerializeField] private GhostText ghostText;
    public PlayerMovement playerMovement;
    public GameObject book1, book2, book3, book4;
    public GameObject player;

    private int currentFactIndex;
    private List<int> factIndexes;

    private string[] historyFactsPT = {
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

    private string[] historyQuestionsPT = {
        "Qual foi o impacto cultural do Renascimento na Europa?",
        "Quais foram algumas das principais consequ�ncias da Revolu��o Francesa?",
        "Qual foi a causa central da Guerra Civil Americana?",
        "Qual foi o principal resultado da Primeira Guerra Mundial...",
        "Qual era o principal objetivo da Era das Grandes Navega��es?",
        "Por que foi criada a ONU em 1945?",
        "Qual foi o impacto da Revolu��o Industrial no mundo?",
        "O que representou a assinatura da Declara��o de Independ�ncia dos EUA?",
        "Quais foram as principais consequ�ncias da Segunda Guerra Mundial?",
        "O que foi o Tratado de Versalhes e seu impacto na Alemanha?",
        "Qual foi o legado mais duradouro do Imp�rio Romano?"
    };

    private string[] correctAnswersPT = {
        "Ressurgimento das artes e ci�ncias...",
        "Derrubada da monarquia e estabelecimento de princ�pios...",
        "Conflito entre estados do Norte e do Sul...",
        "Mudan�as significativas nas fronteiras europeias...",
        "Descobrir novas rotas e territ�rios...",
        "Promover a paz e a coopera��o internacional...",
        "Transformou a economia mundial...",
        "A separa��o oficial das Treze Col�nias...",
        "A divis�o do mundo entre blocos ideol�gicos...",
        "Imposi��o de condi��es severas � Alemanha...",
        "Legado em �reas como direito, engenharia..."
    };

    private string[][] incorrectAnswersPT = {
        new string[] { "Decl�nio do interesse em ci�ncias...", "Expans�o da monarquia absoluta..." },
        new string[] { "Fortalecimento da monarquia...", "Cria��o de uma alian�a com Inglaterra..." },
        new string[] { "Quest�es territoriais com o M�xico.", "Conflito religioso entre estados..." },
        new string[] { "Unifica��o completa dos pa�ses...", "Expans�o do imp�rio alem�o..." },
        new string[] { "Explora��o de minas de ouro...", "Conquista e coloniza��o da �sia..." },
        new string[] { "Promover a divis�o dos territ�rios...", "Organizar o com�rcio internacional..." },
        new string[] { "Reduziu a inova��o tecnol�gica...", "Teve impacto apenas na Europa..." },
        new string[] { "A submiss�o das col�nias...", "Nova alian�a entre EUA e Inglaterra." },
        new string[] { "Unifica��o imediata de todas as na��es.", "Fortalecimento dos imp�rios europeus." },
        new string[] { "Restaura��o da monarquia alem�...", "Alian�a entre Fran�a e Alemanha..." },
        new string[] { "Impacto limitado � pen�nsula It�lica...", "Apenas conquistas militares..." }
    };

    // Dados em ingl�s
    private string[] historyFactsEN = {
        "The Renaissance was a cultural movement that occurred in Europe between the 14th and 17th centuries, characterized by the revival of arts, sciences, and humanist thinking.",
        "The French Revolution, which began in 1789, was a milestone that overthrew the absolutist monarchy in France, resulting in profound social and political changes.",
        "The American Civil War (1861-1865) was a conflict between the Northern and Southern states of the USA, largely due to the issue of slavery and the fight to preserve the Union.",
        "World War I (1914-1918) involved major world powers and resulted in millions of deaths, as well as significant changes to Europe's political map.",
        "The Age of Exploration, between the 15th and 17th centuries, was a period of European maritime expansion that led to the discovery of new routes and territories around the world.",
        "The United Nations (UN) was created in 1945 with the goal of promoting peace and international cooperation after the devastation of World War II.",
        "The Industrial Revolution, which began in the 18th century, transformed the world economy with the emergence of machines and factories, promoting urbanization and technological innovation.",
        "The U.S. Declaration of Independence was signed in 1776, marking the official separation of the Thirteen Colonies from British rule.",
        "World War II (1939-1945) was a global conflict that involved the world's major powers and led to the division of the world between ideological blocs.",
        "The Treaty of Versailles, signed in 1919, marked the official end of World War I, imposing severe conditions on Germany.",
        "The Roman Empire was one of the most influential civilizations in history, leaving a legacy in areas such as law, engineering, and culture."
    };

    private string[] historyQuestionsEN = {
        "What was the cultural impact of the Renaissance in Europe?",
        "What were some major consequences of the French Revolution?",
        "What was the central cause of the American Civil War?",
        "What was the main result of World War I for Europe?",
        "What was the main goal of the Age of Exploration?",
        "Why was the UN created in 1945?",
        "What was the impact of the Industrial Revolution?",
        "What did the signing of the U.S. Declaration of Independence represent?",
        "What were the consequences of World War II?",
        "What was the Treaty of Versailles and its impact on Germany?",
        "What was the Roman Empire's most lasting legacy?"
    };

    private string[] correctAnswersEN = {
        "Revival of arts and sciences and humanist thought.",
        "Overthrow of monarchy and establishment of equality and liberty.",
        "Conflict between North and South over slavery and Union preservation.",
        "Major border changes and weakening of traditional empires.",
        "To discover new routes and expand European influence.",
        "To promote peace and international cooperation after WWII.",
        "It transformed the global economy and encouraged innovation.",
        "The official separation of the 13 Colonies from Britain.",
        "Global division into ideological blocs and reconstruction.",
        "Harsh conditions imposed on Germany, fueling future tensions.",
        "Influence in law, engineering, and culture still seen today."
    };

    private string[][] incorrectAnswersEN = {
        new string[] { "Decline of interest in sciences and arts.", "Expansion of absolute monarchy." },
        new string[] { "Strengthening of the monarchy.", "Alliance with England against Austria." },
        new string[] { "Territorial disputes with Mexico.", "Religious conflict between states." },
        new string[] { "Full unification of Europe.", "German empire expansion across Europe." },
        new string[] { "Gold mining in Africa.", "Colonization of only Asia." },
        new string[] { "Divide Europe's colonial territories.", "Organize global economic exploitation." },
        new string[] { "Decreased innovation and rise of manual labor.", "Impacted only Europe." },
        new string[] { "Colonies submitted to Britain.", "New alliance between U.S. and England." },
        new string[] { "Immediate unification under one government.", "Empires strengthened globally." },
        new string[] { "Restoration of the German monarchy.", "Alliance for economic recovery." },
        new string[] { "Only impacted Italy.", "Only military conquests, no legacy." }
    };

    private void Start()
    {
        audioSource.clip = null;
        InitializeFactIndexes();
        DisplayRandomFact();
    }

    private void InitializeFactIndexes()
    {
        factIndexes = new List<int>();
        for (int i = 0; i < historyFactsPT.Length; i++)
        {
            factIndexes.Add(i);
        }
    }

    private void DisplayRandomFact()
    {
        if (factIndexes.Count == 0)
        {
            InitializeFactIndexes();
        }

        currentFactIndex = factIndexes[Random.Range(0, factIndexes.Count)];
        themeText.text = GameData.language == "en" ? "History" : "Hist�ria";
        factText.text = GameData.language == "en" ? historyFactsEN[currentFactIndex] : historyFactsPT[currentFactIndex];
        factIndexes.Remove(currentFactIndex);

        factPanel.SetActive(true);
        questionPanel.SetActive(false);
    }

    public void OnNextButtonClicked()
    {
        factPanel.SetActive(false);
        questionPanel.SetActive(true);

        questionText.text = GameData.language == "en" ? historyQuestionsEN[currentFactIndex] : historyQuestionsPT[currentFactIndex];
        SetupAnswers();
    }

    private void SetupAnswers()
    {
        string correctAnswer = GameData.language == "en" ? correctAnswersEN[currentFactIndex] : correctAnswersPT[currentFactIndex];
        string[] incorrectArray = GameData.language == "en" ? incorrectAnswersEN[currentFactIndex] : incorrectAnswersPT[currentFactIndex];

        List<string> allAnswers = new List<string>(incorrectArray);
        int correctAnswerPosition = Random.Range(0, allAnswers.Count + 1);
        allAnswers.Insert(correctAnswerPosition, correctAnswer);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < allAnswers.Count)
            {
                int buttonIndex = i;
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = allAnswers[i];
                answerButtons[i].onClick.RemoveAllListeners();

                bool isCorrect = (i == correctAnswerPosition);
                answerButtons[i].onClick.AddListener(() => AnswerSelected(isCorrect, answerButtons[buttonIndex].gameObject));
                answerButtons[i].gameObject.SetActive(true);
            }
            else
            {
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
            botao.GetComponent<Image>().color = Color.green;
            audioSource.clip = correctSound;

            if (targetBook != null)
                DisableBookCollider(targetBook);

            GameData.historyPoints++;
        }
        else
        {
            botao.GetComponent<Image>().color = Color.red;
            ghostText.HandleMiss();
            audioSource.clip = incorrectSound;

            GameData.historyPoints = Mathf.Max(0, GameData.historyPoints - 1);

            if (player != null)
            {
                if (playerPosition.y < 10)
                    player.transform.position = new Vector3(-39, 9.531775f, player.transform.position.z);
                else
                    player.transform.position = new Vector3(-130, 37, player.transform.position.z);
            }
        }

        HistoryPointsText.text = GameData.historyPoints.ToString();
        audioSource.Play();
        StartCoroutine(HandleAnswerFeedback(isCorrect, botao));
    }

    private IEnumerator HandleAnswerFeedback(bool isCorrect, GameObject botao)
    {
        yield return new WaitForSeconds(0.2f);
        botao.GetComponent<Image>().color = Color.white;
        Canva.SetActive(false);
        playerMovement.collidedStop = false;
        DisplayRandomFact();
    }

    private GameObject DetermineBookBasedOnPlayerPosition()
    {
        Vector3 playerPosition = player.transform.position;

        if (playerPosition.y < 10)
            return book1;
        else if (playerPosition.x <= -100)
            return book2;
        else if (playerPosition.x <= -80)
            return book3;
        else
            return book4;
    }

    private void DisableBookCollider(GameObject book)
    {
        if (book != null)
        {
            var col = book.GetComponent<BoxCollider2D>();
            var rb = book.GetComponent<Rigidbody2D>();

            if (col) col.enabled = false;
            if (rb)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.gravityScale = 10;
            }

            Destroy(book, 5);
        }
    }
}
