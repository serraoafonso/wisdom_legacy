using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class FinanceQuizManager : MonoBehaviour
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
    public TextMeshProUGUI financePointsText;
    public GameObject Canva;

    [SerializeField] private GhostText ghostText;
    public PlayerMovement playerMovement;
    public GameObject book1;
    public GameObject book2;
    public GameObject book3;
    public GameObject book4;
    public GameObject player;
    private int currentFactIndex;
    private List<int> factIndexes;

    private string[] financeFactsPT = {
        "O orçamento nacional é um plano financeiro do governo que envolve as receitas e despesas do país.",
        "A inflação é o aumento generalizado dos preços de bens e serviços em uma economia ao longo do tempo.",
        "A taxa de juros é o custo do dinheiro emprestado, geralmente expresso como uma porcentagem.",
        "O mercado de ações é onde investidores compram e vendem ações de empresas públicas.",
        "A economia global é o sistema econômico internacional, onde países trocam bens, serviços e recursos financeiros.",
        "A dívida pública é o total de empréstimos que o governo tem, frequentemente usado para financiar déficits orçamentários.",
        "O PIB (Produto Interno Bruto) é a medida do valor total de bens e serviços produzidos em um país durante um determinado período.",
        "A balança comercial é a diferença entre o valor das exportações e importações de um país.",
        "A moeda fiduciária é uma moeda cujo valor não é respaldado por um ativo físico, como ouro ou prata.",
        "Os impostos são contribuições financeiras obrigatórias feitas pelos cidadãos ao governo para financiar serviços públicos."
    };

    private string[] financeFactsEN = {
        "The national budget is a financial plan of the government that involves the revenues and expenses of the country.",
        "Inflation is the general increase in the prices of goods and services in an economy over time.",
        "The interest rate is the cost of borrowed money, usually expressed as a percentage.",
        "The stock market is where investors buy and sell shares of publicly traded companies.",
        "The global economy is the international economic system where countries exchange goods, services, and financial resources.",
        "Public debt is the total amount of loans the government has, often used to finance budget deficits.",
        "GDP (Gross Domestic Product) is the measure of the total value of goods and services produced in a country during a specific period.",
        "The trade balance is the difference between the value of a country's exports and imports.",
        "Fiat currency is a currency that is not backed by a physical asset such as gold or silver.",
        "Taxes are mandatory financial contributions made by citizens to the government to fund public services."
    };

    private string[] financeQuestionsPT = {
        "O que é o orçamento nacional e qual sua função?",
        "O que é inflação e como ela afeta a economia?",
        "O que é a taxa de juros e como ela influencia o mercado?",
        "O que é o mercado de ações e como ele funciona?",
        "O que é a economia global e qual seu impacto nos países?",
        "O que é a dívida pública e como ela afeta o governo?",
        "O que é o PIB e como ele é calculado?",
        "O que é a balança comercial e por que ela é importante?",
        "O que é uma moeda fiduciária?",
        "Qual a importância dos impostos para a economia?"
    };

    private string[] financeQuestionsEN = {
        "What is the national budget and what is its function?",
        "What is inflation and how does it affect the economy?",
        "What is the interest rate and how does it influence the market?",
        "What is the stock market and how does it work?",
        "What is the global economy and what is its impact on countries?",
        "What is public debt and how does it affect the government?",
        "What is GDP and how is it calculated?",
        "What is the trade balance and why is it important?",
        "What is fiat currency?",
        "What is the importance of taxes for the economy?"
    };

    private string[] correctAnswersPT = {
        "É um plano financeiro do governo que envolve receitas e despesas.",
        "É o aumento generalizado dos preços de bens e serviços ao longo do tempo.",
        "É o custo do dinheiro emprestado, expresso como uma porcentagem.",
        "É o lugar onde investidores compram e vendem ações de empresas.",
        "É o sistema econômico internacional onde países trocam bens, serviços e recursos financeiros.",
        "É o total de empréstimos que o governo tem para financiar déficits.",
        "É a medida do valor total de bens e serviços produzidos em um país.",
        "É a diferença entre exportações e importações de um país.",
        "É uma moeda cujo valor não é respaldado por um ativo físico.",
        "São contribuições obrigatórias feitas pelos cidadãos ao governo."
    };

    private string[] correctAnswersEN = {
        "It is a financial plan of the government that involves revenues and expenses.",
        "It is the general increase in the prices of goods and services over time.",
        "It is the cost of borrowed money, expressed as a percentage.",
        "It is the place where investors buy and sell shares of companies.",
        "It is the international economic system where countries exchange goods, services, and financial resources.",
        "It is the total amount of loans the government has to finance deficits.",
        "It is the measure of the total value of goods and services produced in a country.",
        "It is the difference between exports and imports of a country.",
        "It is a currency whose value is not backed by a physical asset.",
        "They are mandatory contributions made by citizens to the government."
    };

    private string[][] incorrectAnswersPT = {
        new string[] { "É um plano financeiro para empresas privadas.", "É um documento feito apenas por bancos centrais." },
        new string[] { "É o aumento de salários no governo.", "É o aumento das vendas de uma empresa." },
        new string[] { "É o preço de um produto no mercado.", "É o valor da moeda de outro país." },
        new string[] { "É onde os governos compram e vendem produtos.", "É onde se trocam moedas entre países." },
        new string[] { "É uma economia onde os recursos são apenas naturais.", "É uma economia onde os países não trocam bens e serviços." },
        new string[] { "É o total de empréstimos privados.", "É o financiamento das empresas privadas." },
        new string[] { "É o valor de bens e serviços vendidos internacionalmente.", "É a quantidade de bens vendidos no mercado interno." },
        new string[] { "É a diferença entre o preço de um produto e seu custo de produção.", "É a quantidade de produtos vendidos dentro de um país." },
        new string[] { "É uma moeda com valor garantido por commodities.", "É uma moeda digital baseada em criptografia." },
        new string[] { "São contribuições voluntárias para ONGs.", "São contribuições feitas apenas para financiar a educação." }
    };

    private string[][] incorrectAnswersEN = {
        new string[] { "It is a financial plan for private companies.", "It is a document made only by central banks." },
        new string[] { "It is the increase of government salaries.", "It is the increase of a company's sales." },
        new string[] { "It is the price of a product in the market.", "It is the value of another country's currency." },
        new string[] { "It is where governments buy and sell products.", "It is where currencies are exchanged between countries." },
        new string[] { "It is an economy where resources are only natural.", "It is an economy where countries don't exchange goods and services." },
        new string[] { "It is the total of private loans.", "It is financing for private companies." },
        new string[] { "It is the value of goods and services sold internationally.", "It is the amount of goods sold in the domestic market." },
        new string[] { "It is the difference between the price of a product and its production cost.", "It is the amount of products sold within a country." },
        new string[] { "It is a currency backed by commodities.", "It is a digital currency based on cryptography." },
        new string[] { "They are voluntary contributions to NGOs.", "They are contributions made only to fund education." }
    };

    private void Start()
    {
        audioSource.clip = null;
        InitializeFactIndexes();
        DisplayRandomFact();
    }

    private void InitializeFactIndexes()
    {
        factIndexes = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    }

    private void DisplayRandomFact()
    {
        if (factIndexes.Count == 0)
        {
            InitializeFactIndexes();
        }

        currentFactIndex = factIndexes[Random.Range(0, factIndexes.Count)];

        themeText.text = GameData.language == "en" ? "Finance" : "Finanças";
        factText.text = GameData.language == "en" ? financeFactsEN[currentFactIndex] : financeFactsPT[currentFactIndex];

        factIndexes.Remove(currentFactIndex);

        factPanel.SetActive(true);
        questionPanel.SetActive(false);
    }

    public void OnNextButtonClicked()
    {
        factPanel.SetActive(false);
        questionPanel.SetActive(true);

        questionText.text = GameData.language == "en" ? financeQuestionsEN[currentFactIndex] : financeQuestionsPT[currentFactIndex];
        SetupAnswers();
    }

    private void SetupAnswers()
    {
        string correctAnswer = GameData.language == "en" ? correctAnswersEN[currentFactIndex] : correctAnswersPT[currentFactIndex];
        string[] incorrects = GameData.language == "en" ? incorrectAnswersEN[currentFactIndex] : incorrectAnswersPT[currentFactIndex];

        List<string> allAnswers = new List<string>(incorrects);
        int correctAnswerPosition = Random.Range(0, allAnswers.Count + 1);
        allAnswers.Insert(correctAnswerPosition, correctAnswer);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < allAnswers.Count)
            {
                int buttonIndex = i;
                answerButtons[buttonIndex].GetComponentInChildren<TextMeshProUGUI>().text = allAnswers[buttonIndex];
                answerButtons[buttonIndex].onClick.RemoveAllListeners();

                bool isCorrect = (buttonIndex == correctAnswerPosition);
                answerButtons[buttonIndex].onClick.AddListener(() => AnswerSelected(isCorrect, answerButtons[buttonIndex].gameObject));
                answerButtons[buttonIndex].gameObject.SetActive(true);
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
            Debug.Log("Resposta correta!");
            audioSource.clip = correctSound;

            if (targetBook != null) DisableBookCollider(targetBook);

            GameData.financePoints++;
            financePointsText.text = GameData.financePoints.ToString();

            StartCoroutine(HandleAnswerFeedback(true, botao));
        }
        else
        {
            botao.GetComponent<Image>().color = Color.red;
            ghostText.HandleMiss();
            Debug.Log("Resposta incorreta!");
            audioSource.clip = incorrectSound;

            GameData.financePoints = Mathf.Max(GameData.financePoints - 1, 0);
            financePointsText.text = GameData.financePoints.ToString();

            if (player != null)
            {
                player.transform.position = playerPosition.y < 10
                    ? new Vector3(-39, 9.531775f, player.transform.position.z)
                    : new Vector3(-130, 79, player.transform.position.z);
            }

            StartCoroutine(HandleAnswerFeedback(false, botao));
        }

        audioSource.Play();
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
        Vector3 pos = player.transform.position;

        if (pos.y < 10) return book1;
        if (pos.x <= -100) return book2;
        if (pos.x <= -80) return book3;
        return book4;
    }

    private void DisableBookCollider(GameObject book)
    {
        if (book == null) return;

        BoxCollider2D collider = book.GetComponent<BoxCollider2D>();
        Rigidbody2D rb = book.GetComponent<Rigidbody2D>();

        if (collider != null) collider.enabled = false;
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 10;
        }

        Destroy(book, 5);
    }
}
