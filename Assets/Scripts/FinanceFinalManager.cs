using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class FinanceFinalManager : MonoBehaviour
{
    public TextMeshProUGUI questionText; // Texto da pergunta
    public Button[] answerButtons; // Botões de resposta
    public AudioSource audioSource; // AudioSource para sons de resposta
    public AudioClip correctSound; // Som de resposta correta
    public AudioClip incorrectSound; // Som de resposta incorreta
    public GameObject quizPanel; // Painel que exibe a pergunta e opções
    public GameObject victoryPanel; // Painel de vitória
    public TextMeshProUGUI FinancePointsText;

    public GameObject finalCanvas; // Canvas final
    public GameObject book1; // Referência ao objeto do livro ou similar
    public PlayerMovement playerMovement; // Referência ao script de movimento do jogador

    private int correctAnswersCount; // Contador de respostas corretas seguidas
    private int currentQuestionIndex;
    private List<int> questionIndexes;
    private List<string> answerOptions;

    // Perguntas e respostas em diferentes idiomas
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
        StartQuiz();
    }

    private void StartQuiz()
    {
        // Inicializa o quiz com todas as perguntas novamente
        questionIndexes = new List<int>();
        for (int i = 0; i < questionsPT.Length; i++)
        {
            questionIndexes.Add(i);
        }
        ShuffleQuestions();
        correctAnswersCount = 0; // Reseta contador de respostas corretas seguidas
        LoadQuestion();

        // Desativa o movimento do jogador enquanto o quiz está ativo
        if (playerMovement != null)
        {
            playerMovement.collidedStop = true;
        }
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

        string selectedLanguage = GameData.language; // Verifica o idioma atual

        if (selectedLanguage == "pt")
        {
            questionText.text = questionsPT[currentQuestionIndex];
            List<string> answerOptions = new List<string>(incorrectAnswersPT[currentQuestionIndex]);
            answerOptions.Add(correctAnswersPT[currentQuestionIndex]);
            ShuffleList(answerOptions); // Método para embaralhar respostas
        }
        else
        {
            questionText.text = questionsEN[currentQuestionIndex];
            List<string> answerOptions = new List<string>(incorrectAnswersEN[currentQuestionIndex]);
            answerOptions.Add(correctAnswersEN[currentQuestionIndex]);
            ShuffleList(answerOptions); // Método para embaralhar respostas
        }

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answerOptions[i];
            answerButtons[i].onClick.RemoveAllListeners();
            bool isCorrect = answerOptions[i] == (selectedLanguage == "pt" ? correctAnswersPT[currentQuestionIndex] : correctAnswersEN[currentQuestionIndex]);
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
                DisableBookCollider(book1); // Desativa o livro
                playerMovement.collidedStop = false; // Reativa o movimento do jogador
                finalCanvas.SetActive(false); // Fecha o canvas final
                GameData.financePoints += 3;
                FinancePointsText.text = GameData.financePoints.ToString();
                return;
            }
        }
        else
        {
            if (GameData.financePoints > 1)
            {
                GameData.financePoints -= 2;

            }
            else
            {
                GameData.financePoints = 0;
            }
            FinancePointsText.text = GameData.financePoints.ToString();
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
                bookRigidbody.bodyType = RigidbodyType2D.Dynamic; // Define o corpo como dinâmico
                bookRigidbody.gravityScale = 10;
                Debug.Log($"Rigidbody2D do livro {book.name} definido como dinâmico.");
            }
            else
            {
                Debug.LogWarning($"Rigidbody2D não encontrado no livro {book.name}.");
            }

            Destroy(book, 5); // Destroi o livro após 5 segundos
        }
        else
        {
            Debug.LogWarning($"Livro não encontrado.");
        }
    }
}