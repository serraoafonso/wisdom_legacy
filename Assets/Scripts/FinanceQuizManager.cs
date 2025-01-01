using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class FinanceQuizManager : MonoBehaviour
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
    public GameObject Canva; // Painel para controle adicional

    [SerializeField] private GhostText ghostText;
    public PlayerMovement playerMovement; // Referência ao script PlayerMovement
    public GameObject book1; // Referência ao objeto do livro
    public GameObject book2; // Referência ao objeto do livro
    public GameObject book3; // Referência ao objeto do livro
    public GameObject book4; // Referência ao objeto do livro
    public GameObject player; // Referência ao jogador

    private int currentFactIndex;
    private List<int> factIndexes;

    private string[] financeFacts = {
        "A poupança é o ato de guardar parte da renda para uso futuro, criando uma base financeira para imprevistos e objetivos de longo prazo.",
        "Planejar a aposentadoria é essencial para garantir uma fonte de renda no futuro, normalmente através de contribuições para planos de previdência pública e privada.",
        "O IRS (Imposto sobre o Rendimento das Pessoas Singulares) é um tributo cobrado em Portugal sobre os rendimentos de pessoas físicas, variando conforme a faixa de renda anual.",
        "O cartão de crédito permite compras e pagamentos a prazo, mas os juros cobrados por atrasos no pagamento podem ser muito elevados.",
        "O empréstimo estudantil permite que estudantes financiem sua educação e paguem o valor emprestado posteriormente, geralmente com juros baixos e prazos flexíveis.",
        "Definir objetivos financeiros claros, como comprar uma casa ou pagar dívidas, ajuda a direcionar o planejamento financeiro e a alcançar metas."
    };

    private string[] financeQuestions = {
        "Qual é a principal vantagem de poupar parte da renda mensal?",
        "Por que é importante planejar a aposentadoria desde cedo?",
        "Para que serve o IRS (Imposto sobre o Rendimento das Pessoas Singulares) em Portugal?",
        "Qual é uma das principais desvantagens do cartão de crédito?",
        "Qual é o propósito de um empréstimo estudantil?",
        "Como definir objetivos financeiros ajuda no planejamento pessoal?"
    };

    private string[] correctAnswers = {
        "Cria uma base financeira para imprevistos e para realizar objetivos de longo prazo.",
        "Para garantir uma fonte de renda no futuro e manter o padrão de vida desejado.",
        "É um tributo obrigatório que financia serviços públicos e funciona conforme a faixa de renda anual da pessoa.",
        "Juros elevados em caso de atrasos, podendo gerar dívidas difíceis de quitar.",
        "Permite financiar a educação e pagar o valor emprestado com condições facilitadas.",
        "Ajuda a direcionar o planejamento financeiro e alcançar metas específicas."
    };

    private string[][] incorrectAnswers = new string[][] {
        new string[] { "É um recurso opcional, sem grande impacto no futuro financeiro.", "Serve apenas para gastar em viagens e lazer." },
        new string[] { "Para aumentar a quantidade de gastos na juventude.", "Para garantir benefícios como férias prolongadas na aposentadoria." },
        new string[] { "Para diminuir os custos de bens e serviços pessoais.", "Para ajudar exclusivamente no financiamento de dívidas pessoais." },
        new string[] { "Possibilidade de acumular recompensas de fidelidade.", "Melhorar o crédito sem custos extras, mesmo com atrasos." },
        new string[] { "Servir como apoio de curto prazo para despesas pessoais.", "Cobrir apenas materiais e despesas de lazer dos estudantes." },
        new string[] { "Permite focar mais em compras de alto valor e bens de luxo.", "Faz com que o planejamento se torne mais restritivo e limitado." }
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
        themeText.text = "Literacia Financeira";
        factText.text = financeFacts[currentFactIndex]; // Atualiza o texto do fato
        factIndexes.Remove(currentFactIndex); // Remove o índice exibido

        factPanel.SetActive(true);
        questionPanel.SetActive(false);
    }

    public void OnNextButtonClicked()
    {
        factPanel.SetActive(false);
        questionPanel.SetActive(true);

        questionText.text = financeQuestions[currentFactIndex];
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

            // Mantém o Canvas ativo por um tempo para mostrar a cor/som
            StartCoroutine(HandleAnswerFeedback(true, botao));
        }
        else
        {
            botao.GetComponent<Image>().color = Color.red; // Indica resposta incorreta
            ghostText.HandleMiss();
            Debug.Log("Resposta incorreta!");
            audioSource.clip = incorrectSound;

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
                    player.transform.position = new Vector3(-50, 56.5f, player.transform.position.z); // Reposicionar jogador
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

        if (playerPosition.y < 10) // Exemplo de condição
        {
            return book1;
        }
        else if (playerPosition.x >= -60)
        {
            return book2;
        }
        else if (playerPosition.x >= -80)
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
            if (bookRigidbody != null)
            {
                bookRigidbody.bodyType = RigidbodyType2D.Dynamic;
                bookRigidbody.gravityScale = 10;
                Debug.Log($"Rigidbody2D do livro {book.name} alterado para Dynamic.");
            }
            Destroy(book, 5);
        }
        else
        {
            Debug.LogWarning("Livro não encontrado.");
        }
    }
}
