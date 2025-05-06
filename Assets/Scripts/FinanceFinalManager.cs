using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class FinanceFinalManager : MonoBehaviour
{
    public TextMeshProUGUI questionText; // Question text
    public Button[] answerButtons; // Answer buttons
    public AudioSource audioSource; // AudioSource for response sounds
    public AudioClip correctSound; // Correct answer sound
    public AudioClip incorrectSound; // Incorrect answer sound
    public GameObject quizPanel; // Panel that displays the question and options
    public GameObject victoryPanel; // Victory panel
    public TextMeshProUGUI FinancePointsText; // Text for finance points

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
        "Qual é a principal vantagem de poupar parte da renda mensal?",
        "Por que é importante planejar a aposentadoria desde cedo?",
        "Para que serve o IRS (Imposto sobre o Rendimento das Pessoas Singulares) em Portugal?",
        "Qual é uma das principais desvantagens do cartão de crédito?",
        "Qual é o propósito de um empréstimo estudantil?",
        "Como definir objetivos financeiros ajuda no planejamento pessoal?",
        "Por que é importante diversificar os investimentos?",
        "Qual é o objetivo principal de um fundo de emergência?",
        "Quais são os impactos do endividamento excessivo na saúde financeira?",
        "Quais são as vantagens e os riscos de investir em ações?",
        "Qual é o principal benefício de criar um orçamento mensal?"
    };

    private string[] correctAnswersPT = {
        "Cria uma base financeira para imprevistos e para realizar objetivos de longo prazo.",
        "Para garantir uma fonte de renda no futuro e manter o padrão de vida desejado.",
        "É um tributo obrigatório que financia serviços públicos e funciona conforme a faixa de renda anual da pessoa.",
        "Juros elevados em caso de atrasos, podendo gerar dívidas difíceis de quitar.",
        "Permite financiar a educação e pagar o valor emprestado com condições facilitadas.",
        "Ajuda a direcionar o planejamento financeiro e alcançar metas específicas.",
        "Para reduzir riscos financeiros e aumentar a estabilidade ao espalhar o capital entre diferentes ativos.",
        "Ter recursos suficientes para lidar com emergências sem recorrer a dívidas.",
        "Limita a capacidade de economizar e investir, além de gerar dificuldades financeiras a longo prazo.",
        "Permite lucrar com o crescimento de empresas, mas envolve maior risco de perda de capital.",
        "Ajuda a monitorar receitas e despesas, evitando dívidas e alcançando metas financeiras."
    };

    private string[][] incorrectAnswersPT = new string[][] {
        new string[] { "É um recurso opcional, sem grande impacto no futuro financeiro.", "Serve apenas para gastar em viagens e lazer." },
        new string[] { "Para aumentar a quantidade de gastos na juventude.", "Para garantir benefícios como férias prolongadas na aposentadoria." },
        new string[] { "Para diminuir os custos de bens e serviços pessoais.", "Para ajudar exclusivamente no financiamento de dívidas pessoais." },
        new string[] { "Possibilidade de acumular recompensas de fidelidade.", "Melhorar o crédito sem custos extras, mesmo com atrasos." },
        new string[] { "Servir como apoio de curto prazo para despesas pessoais.", "Cobrir apenas materiais e despesas de lazer dos estudantes." },
        new string[] { "Permite focar mais em compras de alto valor e bens de luxo.", "Faz com que o planejamento se torne mais restritivo e limitado." },
        new string[] { "Para garantir lucros elevados em todos os ativos.", "Para eliminar a necessidade de um fundo de emergência." },
        new string[] { "Aumentar o número de investimentos de risco.", "Garantir conforto financeiro apenas em curto prazo." },
        new string[] { "Melhora a capacidade de poupar para o futuro.", "Facilita a obtenção de crédito com juros baixos." },
        new string[] { "É totalmente seguro e garante lucros fixos.", "Não requer análise de mercado para tomar decisões." },
        new string[] { "Permite gastar sem restrições mensais.", "Facilita o endividamento ao longo do tempo." }
    };

    private string[] questionsEN = {
        "What is the main advantage of saving part of your monthly income?",
        "Why is it important to plan retirement early?",
        "What is the purpose of the IRS (Income Tax for Individuals) in Portugal?",
        "What is one of the main disadvantages of credit cards?",
        "What is the purpose of a student loan?",
        "How does setting financial goals help personal planning?",
        "Why is it important to diversify investments?",
        "What is the main goal of an emergency fund?",
        "What are the impacts of excessive debt on financial health?",
        "What are the advantages and risks of investing in stocks?",
        "What is the main benefit of creating a monthly budget?"
    };

    private string[] correctAnswersEN = {
        "Creates a financial base for unforeseen events and achieving long-term goals.",
        "To ensure a source of income in the future and maintain the desired standard of living.",
        "It is a mandatory tax that finances public services and operates according to an individual's annual income bracket.",
        "High interest rates in case of delays, which can generate hard-to-pay debts.",
        "It allows financing education and repaying the loan with easier conditions.",
        "Helps to guide financial planning and achieve specific goals.",
        "To reduce financial risks and increase stability by spreading capital across different assets.",
        "To have enough resources to deal with emergencies without resorting to debt.",
        "Limits the ability to save and invest, and causes long-term financial difficulties.",
        "Allows profiting from the growth of companies, but involves a higher risk of capital loss.",
        "Helps monitor income and expenses, avoiding debt and achieving financial goals."
    };

    private string[][] incorrectAnswersEN = new string[][] {
        new string[] { "It is an optional resource, with little impact on future finances.", "It is only for spending on trips and leisure." },
        new string[] { "To increase spending in youth.", "To guarantee benefits such as extended vacations in retirement." },
        new string[] { "To reduce the costs of goods and personal services.", "To help exclusively in financing personal debts." },
        new string[] { "Possibility of accumulating loyalty rewards.", "Improving credit without extra costs, even with delays." },
        new string[] { "To serve as short-term support for personal expenses.", "To cover only materials and leisure expenses for students." },
        new string[] { "Allows focusing more on high-value purchases and luxury goods.", "Makes planning more restrictive and limited." },
        new string[] { "To ensure high profits in all assets.", "To eliminate the need for an emergency fund." },
        new string[] { "Increase the number of risky investments.", "To guarantee financial comfort only in the short term." },
        new string[] { "Improves the ability to save for the future.", "Facilitates obtaining credit with low interest rates." },
        new string[] { "It is completely safe and guarantees fixed profits.", "Does not require market analysis to make decisions." },
        new string[] { "Allows spending without monthly restrictions.", "Facilitates long-term indebtedness." }
    };

    private void Start()
    {
        // Get current language from GameData
        language = GameData.language;

        // Update the finance points display
        FinancePointsText.text = GameData.financePoints.ToString();

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

        // Disable player movement while quiz is active
        if (playerMovement != null)
        {
            playerMovement.collidedStop = true;
        }
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

            GameData.financePoints++;
            FinancePointsText.text = GameData.financePoints.ToString();

            if (correctAnswersCount >= 3)
            {
                GameData.financePoints += 3;
                FinancePointsText.text = GameData.financePoints.ToString();
                playerMovement.collidedStop = false;
                DisableBookCollider(book1);
                finalCanvas.SetActive(false);
                return;
            }
        }
        else
        {
            audioSource.PlayOneShot(incorrectSound);

            if (GameData.financePoints > 1)
            {
                GameData.financePoints -= 2;
            }
            else
            {
                GameData.financePoints = 0;
            }
            FinancePointsText.text = GameData.financePoints.ToString();

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

            Destroy(book, 5); // Destroy the book after 5 seconds
        }
        else
        {
            Debug.LogWarning("Book not found.");
        }
    }
}